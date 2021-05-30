using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamerController : MonoBehaviour
{
    private float XPosition { get; set; }
    private float ZPosition { get; set; }

    [field: SerializeField]
    private Transform CameraSlot { get; set; }

    [field: SerializeField]
    private float MoveSpeed { get; set; }
    [field: SerializeField]
    private float XMax { get; set; }
    [field: SerializeField]
    private float XMin { get; set; }
    [field: SerializeField]
    private float ZMin { get; set; }
    [field: SerializeField]
    private float ZMax { get; set; }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        MoveCamera();
    }
    private void Initialize()
    {
        XPosition = CameraSlot.position.x;
        ZPosition = CameraSlot.position.z;
    }
    private void MoveCamera()
    {
        XPosition += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        ZPosition += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

        XPosition = Mathf.Clamp(XPosition, XMin, XMax);
        ZPosition = Mathf.Clamp(ZPosition, ZMin, ZMax);

        CameraSlot.position = new Vector3(XPosition, CameraSlot.position.y, ZPosition);
    }

}
