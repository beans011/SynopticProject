using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private int counter = 0;
    private Transform targetPoint;

    private void Start()
    {
        gameObject.GetComponentInChildren<Renderer>().material = NPCSpawner.instance.GetMaterial();
        targetPoint = NPCSpawner.instance.GetASpawnPoint();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(1.0f, 3.0f);
        agent.SetDestination(targetPoint.position);
    }

    private void Update()
    {
        if (counter == 60) 
        { 
            CheckLocation();
            counter = 0;
        }
        else 
        {
            counter++;
        }                
    }

    private void CheckLocation()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(gameObject);
        }
    }
}
