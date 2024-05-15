using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointDeath : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tempPlayer = collision.gameObject;

        if(DataManager.me.lifeBar != null)
        {
            DataManager.me.lifeCount--;
            if(DataManager.me.lifeCount <= 0)
            {
                Debug.Log("Game Over");
            }
        }
       
        if (tempPlayer.CompareTag("Player"))
        {
            Debug.Log("Inside SavePointDeath");

                if (soundEffect != null) soundEffect.Play();
               
                tempPlayer.transform.position = DataManager.me.lastSavePoint;
        }

    }
}
