using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnimator : MonoBehaviour
{
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private bool isPlaying = false;
    private const string RISE_RINGS_NAME = "RiseRings";

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
       // rendererComponent = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E Pressed");
            animatorComponent.enabled = true;
            animatorComponent.Play(RISE_RINGS_NAME);
        }
    }

    public void Disable() {
        isPlaying = false;
        animatorComponent.enabled = false;
    }

    public void Enable() {
        isPlaying = true;
    }

    
}
