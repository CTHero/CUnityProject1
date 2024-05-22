using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpableEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource enemyDeathSound;
    [SerializeField] private AudioSource playerDeathSound;
    [SerializeField] private GameObject topLimitObject;
    [SerializeField] private float bounceStrength = 7.0F;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tempPlayer = collision.gameObject;
        Debug.Log("Inside JumpableEnemy");

        
        if (tempPlayer.CompareTag("Player")) // Checks to see that the object we collided with was the player
        {
            if (topLimitObject != null && (topLimitObject.transform.position.y < tempPlayer.transform.position.y))
            {
                //Enemy dies
                 if (enemyDeathSound != null) enemyDeathSound.Play(); 
                
                 //Bounch the player up in the air
                Rigidbody2D theRigidbody = tempPlayer.GetComponent<Rigidbody2D>();
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, bounceStrength);
                Destroy(gameObject);//Remove the enemy from the game
            }
            else
            {
                //Player dies
                if (playerDeathSound != null) playerDeathSound.Play();

                //Remove a life from the lifeBar
                if (DataManager.me.lifeBar != null)
                {
                    DataManager.me.lifeCount--;
                    if (DataManager.me.lifeCount <= 0)
                    {
                        Debug.Log("Game Over");
                    }
                }             

                tempPlayer.transform.position = DataManager.me.lastSavePoint;
            }
        }

    }
}
