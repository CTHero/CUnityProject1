using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointDeath : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tempPlayer = collision.gameObject;
       
        if (tempPlayer.CompareTag("Player"))
        {
            Debug.Log("Inside SavePointDeath");

                if (soundEffect != null) soundEffect.Play();
               
                tempPlayer.transform.position = DataManager.me.lastSavePoint;
        }

    }
}
