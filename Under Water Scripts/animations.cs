using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    #region Animations
    private Animator playerAnimations;
    private SpriteRenderer spriteRenderer;
    private string currentState;

    private const string PLAYER_IDLE = "playerIdle";
    private const string PLAYER_SWIMMING = "playerSwimming";
    #endregion

    private void Start()
    {
        playerAnimations = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AnimationState();
    }

    private void ChangeAnimationState(string newState)
    {
        //Stop the same animation from interrupting itself
        if (currentState == newState) return;

        //Play animations
        playerAnimations.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    private void AnimationState()
    {
        float xInput = playerMoviment.dirX;
        float yInput = playerMoviment.dirY;

        Debug.Log("X Input: "+xInput+ "|| Y Input: " + yInput);

        if (xInput != 0 || yInput != 0) //Running animation
        {
            ChangeAnimationState(PLAYER_SWIMMING);
        }
        else //Idle animation
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        #region flip sprite
        if(xInput > 0){spriteRenderer.flipX = false;}
        else if(xInput < 0){ spriteRenderer.flipX = true; }
        #endregion
    }

}
