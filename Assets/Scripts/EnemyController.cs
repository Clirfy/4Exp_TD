using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class OnEnemyDestroyEvent : UnityEvent<EnemyController> { }

public class EnemyController : MonoBehaviour
{
    [field: SerializeField]
    public OnEnemyDestroyEvent OnEnemyDestroy { get; set; }
    private bool IsOnDestinationGround { get; set; }
    [field: SerializeField]
    private int HpMax { get; set; }
    [field: SerializeField]
    private int HpCurrent { get; set; }
    [field: SerializeField]
    private int MoneyDropAmmount { get; set; }
    [field: SerializeField]
    private int Damage { get; set; }
    [field: SerializeField]
    private Slider HpSlider { get; set; }
    [field: SerializeField]
    private NavMeshAgent MeshAgent { get; set; }
    //[field: SerializeField]
    //private Transform ModelTransform { get; set; }
    //[field: SerializeField]
    //private GameObject SliderTransform { get; set; }

    private void Awake()
    {
        HpCurrent = HpMax;
        HpSlider.maxValue = HpMax;
    }

    private void Update()
    {
        ShootedDown();
        HpSlider.value = HpCurrent;
        //SliderTransform.transform.position = ModelTransform.position;
        //SliderTransform.transform.LookAt(Camera.main.transform);
    }

    public void Initialize(Vector3 endPosition)
    {
        MeshAgent.SetDestination(endPosition);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hit");

        if (collider.CompareTag("Destination"))
        {
            GameManager.Instance.TakeDamage(Damage);
            Destroy(gameObject);
        }

        //if (SharedData.CastleLayerMask.CheckIfContainsLayer(other.gameObject.layer) == true)
        //{
        //    EnterCastle();
        //}
    }

    private void OnDestroy()
    {
        OnEnemyDestroy.Invoke(this);
    }

    public int TakeDamage(int damage)
    {
        return HpCurrent -= damage;
    }

    private void ShootedDown()
    {
        if (HpCurrent <= 0)
        {
            GameManager.Instance.AddMoney(MoneyDropAmmount);
            Destroy(gameObject);
        }
    }

    //private void EnterCastle()
    //{
    //    GameManager.Instance.TakeDamage(Damage);
    //    Destroy(gameObject);
    //}
}
