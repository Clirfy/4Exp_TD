using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController : MonoBehaviour
{
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    private LayerMask FloorLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask BuildGroundLayerMask { get; set; }
    private bool IsOnBuildGround { get; set; }

    private Camera MainCamera { get; set; }
    private bool IsPlaced { get; set; }
    private MeshRenderer[] ChildrenMeshRenderers { get; set; }

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
}
