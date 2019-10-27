using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    Vector3 desiredMove = Vector3.zero;
    private float wanderRadius = 100f;
    private float delayTime = 5f;
    private NavMeshAgent agent;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayTime) {
            Vector3 target = RandomTarget(agent.transform.position, wanderRadius);
            agent.SetDestination(target);
            timer = 0;
        }
    }
    Vector3 RandomTarget(Vector3 origin, float dist) 
    {
        Vector3 random = Random.insideUnitSphere * dist;
        random += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(random, out hit, dist, NavMesh.AllAreas);
        return hit.position;
    }
}
