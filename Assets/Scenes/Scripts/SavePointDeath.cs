using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointDeath : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tempPlayer = collision.gameObject;

       
        if (tempPlayer.CompareTag("Player"))
        {
            //Remove a life from the lifeBar
            if (DataManager.me.lifeBar != null)
            {
                DataManager.me.lifeCount--;
                if (DataManager.me.lifeCount <= 0)
                {
                    Debug.Log("Game Over");
                }
            }



            Debug.Log("Inside SavePointDeath");

                if (deathSound != null) deathSound.Play();
               
                tempPlayer.transform.position = DataManager.me.lastSavePoint;
        }

    }
}
