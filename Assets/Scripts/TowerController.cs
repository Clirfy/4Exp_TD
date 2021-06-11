using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController : MonoBehaviour
{   
    [field: SerializeField]
    public int TowerCost { get; set; }
    [field: SerializeField]
    private float AttackRatio{get; set;}
    [field: SerializeField]
    private int Damage{get; set;}
    [field: SerializeField]
    private float AttackRange{get; set;}
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    private LayerMask FloorLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask BuildGroundLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask EnemyLayerMask{get; set;}
    private bool IsOnBuildGround { get; set; }

    private Camera MainCamera { get; set; }
    private bool IsPlaced { get; set; }
    private MeshRenderer[] ChildrenMeshRenderers { get; set; }
    private int targetCount;
    private float attackTime;
    Collider[] enemyColliders = new Collider[1];

    private void Awake()
    {
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
        Attack();
        TryGetEnemyInSphere();
        if(IsPlaced == true)
        {
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

        if(IsOnBuildGround == true)
        {
            ChangeMaterialColor(Color.green);
        }
        if(IsOnBuildGround == false)
        {
            ChangeMaterialColor(Color.red);
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
        if(targetCount > 0 && Time.time > attackTime)
        {
            enemyColliders[targetCount -1].GetComponent<EnemyController>().TakeDamage(Damage);
            attackTime = Time.time + AttackRatio;
        }
    }
}
