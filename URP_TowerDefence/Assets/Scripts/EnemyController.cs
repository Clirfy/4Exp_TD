using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public void Initialize (Vector3 spawnPosition, Vector3 endPosition)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.Warp(spawnPosition);
        agent.destination = endPosition;
    }
}
