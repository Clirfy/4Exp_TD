using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSpellController : MonoBehaviour
{
    public event Action OnSpellCasted = delegate { };

    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    public float Cooldown { get; set; }
    [field: SerializeField]
    public float Radius { get; set; }
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    protected TowerLayerData LayerData { get; set; }
    protected Collider[] EnemyColliders { get; set; }
    private Camera MainCamera { get; set; }

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    public virtual void Update()
    {
        Ray vRay = MainCamera.ScreenPointToRay(Input.mousePosition);

        FollowMousePosition(vRay);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnSpellCasted.Invoke();
            CastSpell();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CancelSpell();
        }
    }

    public virtual void FollowMousePosition(Ray vRay)
    {
        if (Physics.Raycast(vRay, out RaycastHit vHit, MaxRaycastDistance, LayerData.FloorLayerMask) == true)
        {
            transform.position = new Vector3(vHit.point.x, 1.5f, vHit.point.z);
        }
    }

    public virtual void CastSpell()
    {
        Debug.Log("Spell casted");
        Destroy(gameObject);
    }

    public virtual void CancelSpell()
    {
        Destroy(gameObject);
    }
}
