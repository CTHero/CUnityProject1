using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAnimation : MonoBehaviour, ActivatedObject
{
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private bool playOnce = false;

    private Animator theAnimator = null;
    private int playCount = 0;

    public new void activate()
    {
        Debug.Log("Activating the Animation");
        if (!playOnce || playCount < 1)
        {
            if (soundEffect != null) soundEffect.Play();  //If the sound has been assigned, then play the sound.  Otherwise ignore so that we don't throw an error.
            playCount++;  //Increment playCount so that we know how many times the sound has been played.

        }
        theAnimator = GetComponent<Animator>();
        if (theAnimator != null) theAnimator.SetBool("animation", true);
    }

    public void deactivate()
    {
        if (theAnimator != null) theAnimator.SetBool("animation", false);
    }
}
