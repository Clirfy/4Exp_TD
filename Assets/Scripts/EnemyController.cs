using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool IsOnDestinationGround{get; set;}
    public OnEnemyDestroyEvent OnEnemyDestroyEvent{get; set;}

    [field: SerializeField]
    private int Hp{get; set;}

    public void Initialize (Vector3 spawnPosition, Vector3 endPosition)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.Warp(spawnPosition);
        agent.SetDestination(endPosition);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hit)");
        Destroy(gameObject);
        
    }

    private void OnDestroy()
    {
        OnEnemyDestroyEvent.Invoke(this);
    }

    public int TakeDamage(int damage)
    {
        return Hp -= damage;
    }
}
