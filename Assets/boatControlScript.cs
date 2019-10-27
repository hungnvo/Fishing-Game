using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	// GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
// 
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
    	// Vector3 Movement = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * -1);
     //    transform.position += Movement * 10 * Time.deltaTime;
    }
}
