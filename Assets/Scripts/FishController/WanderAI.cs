using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    Vector3 desiredMove = Vector3.zero;
    private float wanderRadius = 1000f;
    private float delayTime;
    private NavMeshAgent agent;
    private float timer;
    public AIstate state;
    public GameObject bobber;
    
    // Start is called before the first frame update
    void Start()
    {
        delayTime = Random.Range(3f,7f);
        agent = GetComponent<NavMeshAgent>();
        timer = delayTime;
        state = AIstate.wander;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case AIstate.wander:
                if (timer >= delayTime)
                {
                    Vector3 target = RandomTarget(agent.transform.position, wanderRadius);
                    agent.SetDestination(target);
                    timer = 0;
                }
                if (bobber.GetComponent<Renderer>().enabled) {
                    if (Vector3.Distance(agent.transform.position, bobber.transform.position) <= 100)
                    {
                        state = AIstate.chase;
                    }
                }
            break;
            case AIstate.chase:
                agent.SetDestination(bobber.transform.position);
                if (Vector3.Distance(agent.transform.position, bobber.transform.position) > 100)
                {
                    state = AIstate.wander;
                }
            break;
            default:
            break;
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
    public enum AIstate
    {
        wander,
        chase
    };
}
