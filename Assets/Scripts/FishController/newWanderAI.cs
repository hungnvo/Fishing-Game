using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class newWanderAI : MonoBehaviour
{
    Vector3 desiredMove = Vector3.zero;
    private float wanderRadius = 100f;
    private float delayTime = 5f;
    private NavMeshAgent agent;
    private float timer;
    public GameObject targetCircle;
    private bool caught;
    
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
        	if (caught == false) {
	        	float distToTarget = Vector3.Distance(targetCircle.transform.position, transform.position);
	        	Vector3 target;
	        	if (distToTarget < 300.0f && targetCircle.GetComponent<Renderer>().enabled) {
	        		target = targetCircle.transform.position;
	        		Debug.Log("Headed Towards Target: " + target);
	        	} else {
	        		Debug.Log("Headed Away Dist: " + distToTarget);
	        		target = RandomTarget(agent.transform.position, wanderRadius);
	            }
	            agent.SetDestination(target);
	            timer = 0;
	        } else {
	        	agent.SetDestination(transform.position);
	        }
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

    void OnTriggerEnter(Collider collider) {
    	if (collider.gameObject.tag == "target") {
        		caught = true;

     	}
    }
}
