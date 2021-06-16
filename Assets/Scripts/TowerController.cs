using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController : MonoBehaviour
{
    [field: SerializeField]
    public int TowerCost { get; set; }
    [field: SerializeField]
    private float AttackRatio { get; set; }
    [field: SerializeField]
    private int Damage { get; set; }
    [field: SerializeField]
    private float AttackRange { get; set; }
    [field: SerializeField]
    public GameObject AttackRangeDisplay { get; set; }
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    private LayerMask FloorLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask BuildGroundLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask EnemyLayerMask { get; set; }
    private bool IsOnBuildGround { get; set; }

    private Camera MainCamera { get; set; }
    private bool IsPlaced { get; set; }
    private MeshRenderer[] ChildrenMeshRenderers { get; set; }
    private int targetCount;
    private float attackTime;
    [field: SerializeField]
    private Collider[] enemyColliders { get; set; } = new Collider[1];
    [field: SerializeField]
    private Projectile ProjectilePrefab { get; set; }
    [field: SerializeField]
    private Transform ShootingPosition { get; set; }


    private void Awake()
    {
        AttackRangeDisplay.transform.localScale = new Vector3(AttackRange * 2, 0, AttackRange * 2);
        MainCamera = Camera.main;
        ChildrenMeshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
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
        if (IsPlaced == true)
        {
            TryGetEnemyInSphere();
            Attack();
            return;
        }

        Ray vRay = MainCamera.ScreenPointToRay(Input.mousePosition);

        FollowMousePosition(vRay);
        DebugCheckIfCanBePlaced(vRay);

    }

    private void FollowMousePosition(Ray vRay)
    {
        if (Physics.Raycast(vRay, out RaycastHit vHit, MaxRaycastDistance, FloorLayerMask) == true)
        {
            transform.position = new Vector3(vHit.point.x, 1.5f, vHit.point.z);
        }
    }
    private void DebugCheckIfCanBePlaced(Ray vRay)
    {
        IsOnBuildGround = Physics.Raycast(vRay, MaxRaycastDistance, BuildGroundLayerMask);
        //Debug.Log(IsOnBuildGround == true ? "can be placed" : "can't be placed");

        if (IsOnBuildGround == true)
        {
            ChangeMaterialColor(Color.green);
        }
        else
        {
            ChangeMaterialColor(Color.red);
        }
        if(GameManager.Instance.Money < TowerCost)
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

    private void TryGetEnemyInSphere()
    {
        targetCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, AttackRange, enemyColliders, EnemyLayerMask);
        Debug.Log(targetCount);
    }
    private void Attack()
    {
        if (targetCount > 0 && Time.time > attackTime)
        {
            Projectile projectile = Instantiate(ProjectilePrefab, ShootingPosition.position, ShootingPosition.rotation);
            projectile.LaunchAtTarget(enemyColliders[0].gameObject, Damage);

            //enemyColliders[targetCount -1].GetComponent<EnemyController>().TakeDamage(Damage);
            attackTime = Time.time + AttackRatio;
        }
    }
}
