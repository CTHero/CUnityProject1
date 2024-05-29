using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int sceneNumber = 0;
    [SerializeField] private AudioSource soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (soundEffect != null) soundEffect.Play();
            SceneManager.LoadScene(sceneNumber);
        }

    }



}
