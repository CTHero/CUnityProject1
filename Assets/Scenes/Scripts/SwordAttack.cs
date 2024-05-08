using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    //[SerializeField] private float knockbackStrength = 5f;
    [SerializeField] private float swingDelay = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioSource weaponSoundEffect;

    //private CapsuleCollider2D collider;
    private Animator anim;
    private float delayCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayCountdown <= 0f)
        {
            anim.SetBool("weapon", false);
            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("Fire1 is pressed.");
                anim.SetBool("weapon", true);
                if (weaponSoundEffect != null) weaponSoundEffect.Play();

                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
                for(int i = 0; i < enemiesHit.Length; i++)
                {
                    Destroy(enemiesHit[i].gameObject, 0.1f);
                }

                delayCountdown = swingDelay;
            }
        }
        else
        {
            delayCountdown = delayCountdown - Time.deltaTime;
        }
    }
}
