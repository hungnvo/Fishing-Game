using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControlScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject aimPrompt;
    public GameObject castPrompt;
    public GameObject resetPrompt;
    public GameObject target;
    public GameObject zPrompt;
    public GameObject cPrompt;
    public GameObject caughtFish;
    private int fishDifficulty = -1;
    public bool casting = false;
    public GameObject boat;

    private Rigidbody rb;
    private int speed = 10;
    GameObject collObj;

	public Text fishCountText;
	public int fishCount = 0;

	public AudioClip FishCaughtSound;
	public AudioSource FishCaughtSource;

    public AudioClip BobberCastSound;
	public AudioSource BobberCastSource;

	public enum fishState
	{
	 Rest,
	 Aim,
	 Cast,
	 Reel
	};

	public fishState fs;

    void Start()
    {
    	rb = target.GetComponent<Rigidbody>();
    	fs = fishState.Rest;
    	GetComponent<Renderer>().enabled = false;
		fishCountText.text = fishCount.ToString() + " Fish Caught";
		FishCaughtSource.clip = FishCaughtSound;
        BobberCastSource.clip = BobberCastSound;
		//fishCaughtSound = GetComponent<AudioSource>();
	}

    void Update()
    {
    	// if (Input.GetKeyDown("x")) {
    	// 	Debug.Log("rip");
    	// }
    	switch(fs) {
			case fishState.Rest:
			 	setRest();
                if (Input.GetKeyDown("x")) {
                    fs = fishState.Aim;
                }
				break;
			case fishState.Aim:
				target.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                aim();
                if (Input.GetKeyUp("x")) {
                    fs = fishState.Cast;
                    BobberCastSource.Play();
                }
			    break;
            case fishState.Cast:
                cast();
                if (Input.GetKeyDown("f")) {
                    fs = fishState.Rest;
                }
                break;
            case fishState.Reel:
            	if (Input.GetKeyDown("f")) {
                    fs = fishState.Rest;
                }
                reel();

                break;
			default:
				Debug.Log("Badness");
			    break;
		}
              
    }

    void FixedUpdate() {
    	if (fs == fishState.Aim) {
            // resource: https://forum.unity.com/threads/moving-character-relative-to-camera.383086/
  			
            // float horiz = Input.GetAxis("Horizontal");
            // float vert = Input.GetAxis("Vertical");
            // var camera = Camera.main;

            // var forward = transform.forward;
            // var right = transform.right;

            // forward.y = 0f;
            // right.y = 0f;
            // // forward = Vector3.Normalize(forward);
            // // right = Vector3.Normalize(right);

            // var moveDir = forward * vert + right * horiz;
 
            // // Debug.Log(moveDir);
            // transform.Translate(moveDir * 10f * Time.deltaTime);

            transform.position += Input.GetAxis("Vertical") * transform.forward * 20.0f * Time.deltaTime;
            transform.position += Input.GetAxis("Horizontal") * transform.right * 20.0f * Time.deltaTime;
    	} else {
            // resource: https://answers.unity.com/questions/1373810/how-to-move-the-character-using-wasd.html
            boat.transform.position += Input.GetAxis("Vertical") * transform.forward * 20.0f * Time.deltaTime;
            boat.transform.Rotate(0, Input.GetAxis("Horizontal") * .3f , 0);
        }
    }

    void reel() {
        if (fishDifficulty == 1) {
            if (Input.GetKeyDown("z")) {
                zPrompt.GetComponent<TextMesh>().fontSize -=1;
            }
            if (Input.GetKeyUp("z")) {
                zPrompt.GetComponent<TextMesh>().fontSize +=2;
                if (zPrompt.GetComponent<TextMesh>().fontSize >= 25) {
                    fishDifficulty -=1;
                    zPrompt.SetActive(false);
                }
            }
        } else if (fishDifficulty == 2) {
            if (Input.GetKeyDown("c")) {
                cPrompt.GetComponent<TextMesh>().fontSize -=1;
            }
            if (Input.GetKeyUp("c")) {
                cPrompt.GetComponent<TextMesh>().fontSize +=2;
                if (cPrompt.GetComponent<TextMesh>().fontSize >= 25) {
                    fishDifficulty -=1;
                    zPrompt.SetActive(true);
                    cPrompt.SetActive(false);
                }
            }
        } else if (fishDifficulty == 0) {
            // fs = fishState.Rest;
            caughtFish.SetActive(true);
            if (Input.GetKeyDown("x")) {
				updateFishCount();
				FishCaughtSource.Play();
				//caughtFish.GetComponent<AudioSource>().Play();
				caughtFish.SetActive(false);
                fs = fishState.Rest;
                aimPrompt.SetActive(true);
            }
        }
    }


    void setRest() {
    	aimPrompt.SetActive(true);
		GetComponent<Renderer>().enabled = false;
        resetPrompt.SetActive(false);
    	castPrompt.SetActive(false);
        caughtFish.SetActive(false);
        casting = false;
        zPrompt.GetComponent<TextMesh>().fontSize = 15;
        cPrompt.GetComponent<TextMesh>().fontSize = 15;
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 100;
        pos.y = 1.4f;
        target.transform.position = pos;
        // target.transform.position = new Vector3(0.0f, 0.1f, 86.1f);

    }

    void aim() {
    	GetComponent<Renderer>().enabled = true;
    	target.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        fs = fishState.Aim;
       	aimPrompt.SetActive(false);
       	target.SetActive(true);
       	castPrompt.SetActive(true);
    }

    void cast() {
        casting = true;
       	aimPrompt.SetActive(false);
       	castPrompt.SetActive(false);
        resetPrompt.SetActive(true);
       	target.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
       	checkForFish();
	    
    }



    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.gameObject.tag);
    	if (collider.gameObject.tag == "fish") {
        		Debug.Log("Collided with Fish!");
        		collObj = collider.gameObject;
        		fishDifficulty = 2;
        		resetPrompt.SetActive(false);

     	}
    }

    void OnTriggerExit(Collider collider) {
    	zPrompt.SetActive(false);
    	collObj = null;
    }

    void checkForFish() {
    	if (collObj != null) {
	       	fs = fishState.Reel;
	        cPrompt.SetActive(true);
	        Debug.Log("Found a Fish!");
	    }
    }

	void updateFishCount()
	{
		fishCount+= 1;
		fishCountText.text = fishCount.ToString() + " Fish Caught";
	}
}
