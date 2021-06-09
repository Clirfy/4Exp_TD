using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool IsOnDestinationGround{get; set;}

    public void Initialize (Vector3 spawnPosition, Vector3 endPosition)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.Warp(spawnPosition);
        agent.destination = endPosition;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hit)");
        Destroy(gameObject);
    }
}
