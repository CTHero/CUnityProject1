using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;
 


    [SerializeField] private float jumpStrength = 5.0F;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private bool limitJumps = false;

    [SerializeField] private bool canWallJump = false;
    [SerializeField] private float wallJumpStrength = 15.0f;
    [SerializeField] private float wallJumpingDuration = 0.5f;

    private Rigidbody2D rigidbody;
    //private BoxCollider2D collider;
    private CapsuleCollider2D collider;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool startPointSet = false;

    private bool isWallJumping = false;
    

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
//        collider = GetComponent<BoxCollider2D>();
        collider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if(DataManager.me == null)
        {
            Debug.Log("You must create an Empty Game Object in your Hierarchy and add the DataManager component to it.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPointSet) // Doing this in update in case this gets created before the DataManager.
        {
            DataManager.me.lastSavePoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            startPointSet = true;
        }

        dirX = Input.GetAxisRaw("Horizontal");

        if (!isWallJumping)
        {
            rigidbody.velocity = new Vector2(dirX * moveSpeed, rigidbody.velocity.y);
            CancelInvoke(nameof(stopWallJumping));
        }

        if (Input.GetButtonDown("Jump") )
        {
            if (IsGrounded())  //If we are standing on the ground
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpStrength);
                if (jumpSoundEffect != null) jumpSoundEffect.Play();
            }
            else
            {
                bool rightWall = isTouchingWallRight();
                bool leftWall = isTouchingWallLeft();
                bool bothWalls = rightWall & leftWall;

                if (!bothWalls)  //If bothWalls is true, then we are probably falling down a pit and jump shouldn't work
                {
                   
                    if(rightWall)
                    {
                        Debug.Log("Walljumping!");
                        rigidbody.velocity = new Vector2(-wallJumpStrength, jumpStrength);
                        if (jumpSoundEffect != null) jumpSoundEffect.Play();
                        isWallJumping = true;
                        Invoke(nameof(stopWallJumping), wallJumpingDuration);
                    }
                    else if(leftWall)
                    {
                        Debug.Log("Walljumping!");
                        rigidbody.velocity = new Vector2(wallJumpStrength, jumpStrength);
                        if (jumpSoundEffect != null) jumpSoundEffect.Play();
                        isWallJumping = true;
                        Invoke(nameof(stopWallJumping), wallJumpingDuration);
                    }
                }
            }
        }
        if (Input.GetButton("Fire2"))
        {
            DataManager.me.hiding = true;
        }
        else
        {
            DataManager.me.hiding = false;
        }

        UpdateAnimationState();
    }

    private void stopWallJumping ()
    {
        isWallJumping = false;
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0F)
        {
            state = MovementState.running;
            //sprite.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("running", true);
        }
        else if (dirX < 0F)
        {
            state = MovementState.running;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("running", true);
        }
        else
        {
            state = MovementState.idle;
            anim.SetBool("running", false);
        }
        if (rigidbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        if(rigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        //Detects if the player's touching the ground with the bottom of it's box collider by creating a similar box offset by .1f
        //on the "jumpableGround" layer that we created in the Unity IDE
        if (limitJumps)
        {
            Debug.Log("testing to see if ground is jumpable");
            //return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
            return Physics2D.Raycast(collider.bounds.center, Vector2.down, (collider.bounds.extents.y + 0.2f), jumpableGround);
        }
        else return true;
    }

    private bool isTouchingWallRight()
    {
        //Detects if the player's touching the wall (must be "jumpableGround" layer)
        return Physics2D.Raycast(collider.bounds.center, Vector2.right, (collider.bounds.extents.x + 0.2f), jumpableGround);
    }
    private bool isTouchingWallLeft()
    {
        //Detects if the player's touching the wall (must be "jumpableGround" layer)
        return Physics2D.Raycast(collider.bounds.center, Vector2.left, (collider.bounds.extents.x + 0.2f), jumpableGround);
    }
}
