using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winston : MonoBehaviour
{

    public Animator anim;
    public float InputX;
    public float InputY;
    public bool hasFishingRod;
    public Transform bobberHoldSpot;
    public Rigidbody bobber;
    public Rigidbody currBobber;
    public Rigidbody target;



    // Start is called before the first frame update
    void Start()
    {
        // Get animator
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("HasFishingRod", true);
        // Debug.Log("anim set");
    }

    // Update is called once per frame
    void Update()
    {
        //InputX = Input.GetAxis("Horizontal");
        //InputY = Input.GetAxis("Vertical");
        anim.SetFloat("InputX", InputX);
        anim.SetFloat("InputY", InputY);

        //if (Input.GetKeyDown(KeyCode.N))
            hasFishingRod = !hasFishingRod;
        

        //anim.SetBool("SitSad", Input.GetKeyDown(KeyCode.C) && !hasFishingRod);

        //anim.SetBool("SitPatient", Input.GetKeyDown(KeyCode.V) && !hasFishingRod);

       // anim.SetBool("Stand", Input.GetKeyDown(KeyCode.B) && !hasFishingRod);

        //anim.SetBool("HasFishingRod", hasFishingRod);

        anim.SetBool("Cast", Input.GetKeyUp(KeyCode.X) && hasFishingRod /*&& !anim.GetCurrentAnimatorStateInfo(1).IsName("Cast")*/);

        anim.SetBool("Reel", Input.GetKeyDown(KeyCode.R) && hasFishingRod);

        if (anim.GetBool("Cast"))
        {
            currBobber = Instantiate(bobber, bobberHoldSpot);
            currBobber.isKinematic = false;
            currBobber.useGravity = false;
        }
    }

    public void Cast()
    {
        // currBobber = Instantiate(bobber, ballHoldSpot);
        // currBobber.isKinematic = false;
        // currBobber.useGravity = false;
        currBobber.transform.parent = null;
        currBobber.velocity = Vector3.zero;
        currBobber.angularVelocity = Vector3.zero;
        Debug.Log("target position:" + target.transform.position);
        Debug.Log("bobber position:" + currBobber.transform.position);


        Vector3 castDirection = (target.transform.position - currBobber.transform.position).normalized;
        Debug.Log("cast direction:" + castDirection);
        currBobber.AddForce(80.0f * castDirection, ForceMode.VelocityChange);
        currBobber = null;

    }
}
