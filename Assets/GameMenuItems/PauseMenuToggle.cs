using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  The script will be acting on the CanvasGroup so add a component requirement to the class:
[RequireComponent(typeof(CanvasGroup))]

public class PauseMenuToggle : MonoBehaviour
{
    /*Use GetComponent<CanvasGroup>() to grab a reference in Awake() and store in a private member variable named canvasGroup.
     * You should print a Debug.LogError() if GetComponent() doesn’t find the component you are looking for.
     */
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            Debug.Log("CanvasGroup could not be found");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //In Update() of the PauseMenuToggle script, add the following:
        /* Note that Input.GetKeyUp() should eventually be replaced with Input.GetButtonUp() with a virtual button created in the InputManager settings.
         * This will allow multiple game controllers to map to common input events(e.g.simultaneous keyboard, and handheld game controller support).
         */
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                // when game is being played
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                // when game is paused
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }
}

