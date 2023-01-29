using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private string currentState;

    private string playerDirection;

    #region Animations States constants
    private const string IDLE_UP = "player_IdleUp";
    private const string IDLE_DOWN = "player_IdleDown";
    private const string IDLE_SIDES = "player_IdleSides";

    private const string RUN_UP = "player_UpRun";
    private const string RUN_DOWN = "player_DownRun";
    private const string RUN_SIDES = "player_SideRun";
    #endregion

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = PlayerController.directionLooked;
        AnimationsState();
        Debug.Log(playerDirection);
    }

    private void ChangeAnimationState(string newState)
    {
        //Stop the same animation from interrupting itself
        if (currentState == newState) return;

        //Play animations
        playerAnimator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    private void AnimationsState(){
        if(PlayerController.isRunning){
            if(playerDirection == "Up"){
            ChangeAnimationState(RUN_UP);
            }
            else if(playerDirection == "Down"){
                ChangeAnimationState(RUN_DOWN);
            }
            else if(playerDirection == "Left"){
                ChangeAnimationState(RUN_SIDES);
                spriteRenderer.flipX = true;
            } 
            else if(playerDirection == "Right"){
                spriteRenderer.flipX = false;
                ChangeAnimationState(RUN_SIDES);
            }
        }
        else{
            if(PlayerController.facing == "Up"){
            ChangeAnimationState(IDLE_UP);
            }
            else if(PlayerController.facing == "Down"){
                ChangeAnimationState(IDLE_DOWN);
            }
            else if(PlayerController.facing == "Left"){
                ChangeAnimationState(IDLE_SIDES);
                spriteRenderer.flipX = true;
            } 
            else if(PlayerController.facing == "Right"){
                spriteRenderer.flipX = false;
                ChangeAnimationState(IDLE_SIDES);
            }
        }
        
    }
}