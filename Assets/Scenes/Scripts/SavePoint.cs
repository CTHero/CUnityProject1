using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tempPlayer = collision.gameObject;
        Debug.Log("Inside SavePoint's OnTriggerEnter");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inside SavePoint");

            DataManager.me.lastSavePoint = tempPlayer.transform.position;

            if (soundEffect != null) soundEffect.Play(); // if there is an attached sound, play it upon triggering the save point
        }

    }
}
