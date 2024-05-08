using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;

    

private void OnTriggerEnter2D(Collider2D collision)
{
        Debug.Log("Inside OnTriggerEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inside Die");
            Die();
        }

    }

    private void Die()
    {
        restartLevel();
    }

    private void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
