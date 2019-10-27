using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobberController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    private Rigidbody rb;
    public playerControlScript pcScript;
    private bool atTarget;
    public Transform ballHoldSpot;



    void Start()
    {
       rb = GetComponent<Rigidbody>();
       // rb.useGravity = false;
       pcScript = target.GetComponent<playerControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (pcScript.casting == true && atTarget == false) {
    		//rb.useGravity = true;
            Vector3 tarPos = target.transform.position;
            Vector3 bobPos = transform.position;
    		// rb.AddForce(80.0f, 0.0f, 0.0f);
            rb.AddForce(tarPos.x-bobPos.x, tarPos.y - bobPos.y, tarPos.z-bobPos.z);
            // Plot();
    	}

        if (pcScript.casting == false) {
            atTarget = false;
        }
        
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag == "target") {
                Debug.Log("Collided with Target!");
                atTarget = true;

        }
    }


    // void OnEnable() {
    // 	EventManager.onCast += throwBobber;
    // }

    // void OnDisable() {
    // 	EventManager.onCast -= throwBobber;
    // }

    // void throwBobber() {
    // 	Debug.Log("Throwing Bobber");
    // }
    // Vector3 GetQuadraticCoordinates(t : float) 
    // {
    //     var mousePos = target.mousePosition;
    //     middle.x = mousePos.x;
    //     middle.y = mousePos.y;
    //     middle.z = end.position.z - start.position.z;
    //     return Mathf.Pow(1-t,2)*start.position + 2*t*(1-t)*middle + Mathf.Pow(t,2)*end.position;
    // }
     
    // void Plot() {
    //     var t : float ;    
    //     for (var i : int = 0 ; i < 10 ; i++ ){
    //        t = i/(10-1) ;
    //        lineRenderer.SetPosition (i ,GetQuadraticCoordinates(t));
    //     }
    // }

}

