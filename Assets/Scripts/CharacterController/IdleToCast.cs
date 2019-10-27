using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToCast : MonoBehaviour
{

    public Animator anim;
    public float InputX;
    public float InputY;
    // public bool inputCast;

    // Start is called before the first frame update
    void Start()
    {
        // Get animator
        anim = this.gameObject.GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");
        anim.SetFloat("InputX", InputX);
        anim.SetFloat("InputY", InputY);


        if (Input.GetKeyDown("space"))
        {
            Debug.Log("CastButton set to true.");
            anim.SetBool("CastButton", true);
        }
        else
        {
            anim.SetBool("CastButton", false);
        }
    }
}
