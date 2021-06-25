using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController : MonoBehaviour
{
    [field: SerializeField]
    public int TowerCost { get; set; }
    [field: SerializeField]
    private TowerAttackData TowerAttackData { get; set; }
    [field: SerializeField]
    private TowerLayerData TowerLayerData { get; set; }
    [field: SerializeField]
    private GameObject AttackRangePrefab { get; set; }
    private GameObject AttackRangeDisplay { get; set; }
    [field: SerializeField]
    private Transform AttackRangeSlot { get; set; }
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    private bool IsOnBuildGround { get; set; }
    private Camera MainCamera { get; set; }
    private bool IsPlaced { get; set; }
    private MeshRenderer[] ChildrenMeshRenderers { get; set; }
    [field: SerializeField]
    private Collider[] EnemyColliders { get; set; } = new Collider[1];
    [field: SerializeField]
    private Projectile ProjectilePrefab { get; set; }
    [field: SerializeField]
    private Transform ShootingPosition { get; set; }
    private int TargetCount { get; set; }
    private float TimeSinceLastShot { get; set; }

    private void Awake()
    {
        ChildrenMeshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        MainCamera = Camera.main;
        TimeSinceLastShot = TowerAttackData.AttackRatio;
    }

    private void Start()
    {
        AttackRangeDisplay = Instantiate(AttackRangePrefab, AttackRangeSlot);
        AttackRangeDisplay.transform.localScale = new Vector3(TowerAttackData.AttackRange * 2, 2f, TowerAttackData.AttackRange * 2);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }

    public void Place()
    {
        IsPlaced = true;
        ChangeMaterialColor(Color.white);
    }

    private void Update()
    {
        TimeSinceLastShot += Time.deltaTime;

        if(IsPlaced == false)
        {
            Ray vRay = MainCamera.ScreenPointToRay(Input.mousePosition);

            FollowMousePosition(vRay);
            FeedbackCheckIfCanBePlaced(vRay);
        }
        else if(TimeSinceLastShot > TowerAttackData.AttackRatio)
        {
            AttackRangeSlot.gameObject.SetActive(false);
            Attack();
        }
    }

    private void FollowMousePosition(Ray vRay)
    {
        if (Physics.Raycast(vRay, out RaycastHit vHit, MaxRaycastDistance, TowerLayerData.FloorLayerMask) == true)
        {
            transform.position = new Vector3(vHit.point.x, 1.5f, vHit.point.z);
        }
    }

    private void FeedbackCheckIfCanBePlaced(Ray vRay)
    {
        IsOnBuildGround = Physics.Raycast(vRay, MaxRaycastDistance, TowerLayerData.BuildGroundLayerMask);

        if (IsOnBuildGround == true)
        {
            ChangeMaterialColor(Color.green);
        }
        else
        {
            ChangeMaterialColor(Color.red);
        }

        if (GameManager.Instance.Money < TowerCost)
        {
            ChangeMaterialColor(Color.black);
        }
    }

    public bool CheckIfCanBePlaced()
    {
        return IsOnBuildGround == true;
    }

    private void ChangeMaterialColor(Color color)
    {
        foreach (var item in ChildrenMeshRenderers)
        {
            item.material.color = color;
        }
    }

    private void Attack()
    {
        TargetCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, TowerAttackData.AttackRange, EnemyColliders, TowerLayerData.EnemyLayerMask);

        if (TargetCount > 0)
        {
            Projectile projectile = Instantiate(ProjectilePrefab, ShootingPosition.position, ShootingPosition.rotation);
            projectile.LaunchAtTarget(EnemyColliders[0].GetComponent<EnemyController>(), TowerAttackData.AttackDamage);

            TimeSinceLastShot = 0.0f;
        }
    }
}