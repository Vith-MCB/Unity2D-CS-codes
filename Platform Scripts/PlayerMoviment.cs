using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


public class PlayerMoviment : MonoBehaviour
{
    //Creating variables

    #region Jumping
    private float jumpForce = 14f;
    private float jumpMAXvelocity = 20f;
    private float jumpAcceleration = 20f;
    private int doubleJumpCount = 1;
    [SerializeField] private LayerMask platformLayerMask;
    #endregion

    #region Horizontal Moviment
    private float moveVelocity = 2f;
    private float movAcceleration = .3f;
    private float actualSpeed = 0f;
    private float maxSpeed = 10f;
    private float dirX = 0f;
    #endregion

    #region Animations
    private Animator animations;
    private SpriteRenderer spriteRenderer;
    private string currentState;

    private const string PLAYER_IDLE = "playerIdle";
    private const string PLAYER_RUNNING = "playerRunning";
    private const string PLAYER_FALLING = "playerFalling";
    private const string PLAYER_JUMPING = "playerJumping";
    private const string PLAYER_DOUBLEJUMPING = "playerDoubleJumping";

    #endregion

    private Rigidbody2D player;
    private BoxCollider2D playerCollider;

    //Audio
    [SerializeField] private AudioSource jumpSnd;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        animations = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Moviment(); //Horizontal Moviment

        #region JumpMoviment
        if (IsTouchingGround()) //Restoring the double jump
        {
            doubleJumpCount = 1;
        }

        if (doubleJumpCount > 0 && Input.GetKeyDown(KeyCode.Space)) //Basic jump
        {
            player.AddForce(new Vector2(player.velocity.x, jumpForce), ForceMode2D.Impulse);
            jumpSnd.Play();
            doubleJumpCount -= 1;
        }
        #endregion

        AnimationState(DamageSystem.animationEnded);


    }
    private void Moviment() //The function already treats running animation
    {
        dirX = Input.GetAxisRaw("Horizontal");
        float accRate = 0.1f;


        if (dirX < 0) //Move Left
        {
            spriteRenderer.flipX = true;
                
            accRate -= 0.01f;
            if (actualSpeed < maxSpeed)
            {
                actualSpeed += (movAcceleration * accRate) * moveVelocity;
                player.velocity = new Vector2(-actualSpeed, player.velocity.y);
            }
            else { player.velocity = new Vector2(-maxSpeed, player.velocity.y); accRate = 0.1f; }
        }

        else if (dirX > 0) //Move Right
        {
            spriteRenderer.flipX = false;

            accRate -= 0.01f;
            if (actualSpeed < maxSpeed)
            {

                actualSpeed += (movAcceleration * accRate) * moveVelocity;
                player.velocity = new Vector2(actualSpeed, player.velocity.y);
            }
            else { player.velocity = new Vector2(maxSpeed, player.velocity.y); accRate = 0.1f; }
        }

        else //No move input
        {

            accRate = 0.1f;
            actualSpeed = 0f;
            if (DamageSystem.animationEnded) { player.velocity = new Vector2(0f, player.velocity.y); }
            else { player.velocity = new Vector2(player.velocity.x, player.velocity.y); }

        }

    }

    public void FixedUpdate()
    {
        #region JumpFallAcceleration
        if (player.velocity.y < 0 )
        {
            player.AddForce(new Vector2(0f, -jumpAcceleration), ForceMode2D.Force);
            if (!IsTouchingGround()) { ChangeAnimationState(PLAYER_FALLING); }
            
        }
        #endregion

        if(player.velocity.y > jumpMAXvelocity)
        {
            player.velocity = new Vector2(player.velocity.x,jumpMAXvelocity);
        }
    }

    private bool IsTouchingGround()
    {
        // Cast a ray downwards from the center of the player's collider
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + 0.3f, platformLayerMask);


        // Check if the ray hit any colliders on the ground layer
        if (hit.collider == null)
        {
            return false;
        }
        else { return true; }
        
    }

    private void ChangeAnimationState(string newState)
    {
        //Stop the same animation from interrupting itself
        if (currentState == newState) return;

        //Play animations
        animations.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    private void AnimationState(bool canPerformAnimation)
    {
        if (canPerformAnimation)
        {
            if (IsTouchingGround())
            {
                if (dirX != 0) //Running animation
                {
                    ChangeAnimationState(PLAYER_RUNNING);
                }
                else //Idle animation
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
            }
            else if (player.velocity.y > 0 && doubleJumpCount == 1)//Jump Animation
            {
                ChangeAnimationState(PLAYER_JUMPING);
            }
            else if (doubleJumpCount < 1 && player.velocity.y > 0) //Double Jump animation
            {
                ChangeAnimationState(PLAYER_DOUBLEJUMPING);
            }
            else if (player.velocity.y < 0) //Falling animation
            {
                ChangeAnimationState(PLAYER_FALLING);
            }
        }
    }

}
