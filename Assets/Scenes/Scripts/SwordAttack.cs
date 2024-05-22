using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    //[SerializeField] private float knockbackStrength = 5f;
    [SerializeField] private float swingDelay = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float knockbackAmount = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioSource weaponSoundEffect;

    //private CapsuleCollider2D collider;
    private Animator anim = null;
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
            if(anim != null) anim.SetBool("weapon", false);
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire1 is pressed.");
                if (anim != null) anim.SetBool("weapon", true);
                if (weaponSoundEffect != null) weaponSoundEffect.Play();

                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
                for(int i = 0; i < enemiesHit.Length; i++)
                {
                    int tmpHP = 0;
                    int knockbackModifier = 0;

                    Enemy tmpEnemy = enemiesHit[i].gameObject.GetComponent<Enemy>();

                    tmpHP = tmpEnemy.changeHitPoints(-attackDamage);
                    if (tmpHP <= 0)
                    {
                        Destroy(enemiesHit[i].gameObject, 0.1f);
                    }
                    else
                    {
                        //Knockback
                        if(enemiesHit[i].gameObject.transform.position.x > gameObject.transform.position.x)
                        {
                            knockbackModifier = 1;
                        }
                        else
                        {
                            knockbackModifier = -1;
                        }
                        Rigidbody2D enemyRigidbody = enemiesHit[i].gameObject.GetComponent<Rigidbody2D>();
                        enemyRigidbody.velocity = new Vector2(knockbackModifier * knockbackAmount, enemyRigidbody.velocity.y);
                    }
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
