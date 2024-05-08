using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedSound : MonoBehaviour, ActivatedObject
{

    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private bool playOnce = false;

    

    private int playCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This is the overridden method that will implement the specific sound activation
    public new void activate()
    {
        Debug.Log("Activating the Sound");
        if (!playOnce || playCount < 1)
        {
            if (soundEffect != null) soundEffect.Play();  //If the sound has been assigned, then play the sound.  Otherwise ignore so that we don't throw an error.
            playCount++;  //Increment playCount so that we know how many times the sound has been played.
        }

    }
}
