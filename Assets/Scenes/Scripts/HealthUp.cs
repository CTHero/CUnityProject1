using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private int livesIncrease = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Inside HealthUp");
        if (collision.gameObject.CompareTag("Player"))
        {
            DataManager.me.lifeCount = DataManager.me.lifeCount + livesIncrease;
            if(DataManager.me.lifeCount > DataManager.me.maxLives)
            {
                DataManager.me.lifeCount = DataManager.me.maxLives;
            }
            Destroy(gameObject);
        }

    }
}
