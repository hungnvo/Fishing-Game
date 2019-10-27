using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backupPlayerControlScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject aimPrompt;
    public GameObject castPrompt;
    public GameObject target;
    public GameObject zPrompt;
    public GameObject cPrompt;
    public GameObject caughtFish;
    private int fishDifficulty;
    public bool casting = false;

    private Rigidbody rb;
    private int speed = 10;
    GameObject collObj;

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

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            aim();
        }
        if (Input.GetKeyUp("x"))
        {
            cast();
        }
        if (fs == fishState.Cast) {
            checkForFish();
        }
        if (fs == fishState.Reel) {
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
                    caughtFish.SetActive(false);
                    fs = fishState.Rest;
                    aimPrompt.SetActive(true);
                }
            }

        }
        
    }

    void FixedUpdate() {
        if (fs == fishState.Aim) {
            // resource: https://answers.unity.com/questions/1373810/how-to-move-the-character-using-wasd.html
            Vector3 Movement = new Vector3 (Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1);
            target.transform.position += Movement * speed * Time.deltaTime;

        }
    }

    void aim() {
        GetComponent<Renderer>().enabled = true;
        fs = fishState.Aim;
        aimPrompt.SetActive(false);
        target.SetActive(true);
        castPrompt.SetActive(true);
    }

    void cast() {
        fs = fishState.Cast;
        casting = true;
        aimPrompt.SetActive(false);
        target.SetActive(true);
        castPrompt.SetActive(false);
        target.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        checkForFish();
        
    }


    void OnTriggerEnter(Collider collider) {
        Debug.Log("!");
        if (collider.gameObject.tag == "fish") {
                Debug.Log("Collided with Fish!");
                collObj = collider.gameObject;
                fishDifficulty = 2;
                

        }
    }

    void OnTriggerExit(Collider collider) {
        Debug.Log("X");
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
}
