using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    private Camera MainCamera { get; set; }

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 cachedThisPosition = transform.position;
        Vector3 cachedCameraPosition = MainCamera.transform.position;

        Vector3 directionToLookAt = (cachedCameraPosition - cachedThisPosition).normalized;
        transform.rotation = Quaternion.LookRotation(directionToLookAt);
    }
}
