using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private string currentState;

    private string playerDirection;
    private string playerFacing;

    private bool runningBool;
    private bool attackingBool;

    #region Animations States constants
    private const string IDLE_UP = "player_IdleUp";
    private const string IDLE_DOWN = "player_IdleDown";
    private const string IDLE_SIDES = "player_IdleSides";

    private const string RUN_UP = "player_UpRun";
    private const string RUN_DOWN = "player_DownRun";
    private const string RUN_SIDES = "player_SideRun";


    private const string ATTACK_UP = "player_UAttack";
    private const string ATTACK_DOWN = "player_DAtt";
    private const string ATTACK_SIDES = "player_Att";

    public SwordAttack swordAttack;
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
        playerFacing = PlayerController.facing;
        runningBool = PlayerController.isRunning;
        attackingBool = PlayerController.isAttacking;

        ActionAnimations();
        if(!PlayerController.isAttacking){
            AnimationsState();
        }
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
        if(runningBool){
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
            if(playerFacing == "Up"){
            ChangeAnimationState(IDLE_UP);
            }
            else if(playerFacing == "Down"){
                ChangeAnimationState(IDLE_DOWN);
            }
            else if(playerFacing == "Left"){
                ChangeAnimationState(IDLE_SIDES);
                spriteRenderer.flipX = true;
            } 
            else if(playerFacing == "Right"){
                spriteRenderer.flipX = false;
                ChangeAnimationState(IDLE_SIDES);
            }
        }
        
    }

    private void ActionAnimations(){
        if(attackingBool){
            if(playerFacing == "Up"){
                ChangeAnimationState(ATTACK_UP);
                swordAttack.attackDirection = SwordAttack.AttackDirection.Up;
            }
            else if(playerFacing == "Down"){
                ChangeAnimationState(ATTACK_DOWN);
                swordAttack.attackDirection = SwordAttack.AttackDirection.Down;
            }
            else if(playerFacing == "Left"){
                ChangeAnimationState(ATTACK_SIDES);
                spriteRenderer.flipX = true;
                swordAttack.attackDirection = SwordAttack.AttackDirection.Left;
            } 
            else if(playerFacing == "Right"){
                spriteRenderer.flipX = false;
                ChangeAnimationState(ATTACK_SIDES);
                swordAttack.attackDirection = SwordAttack.AttackDirection.Right;
            }
        } 
    }

    private void AnimationFinished(){
        PlayerController.isAttacking = false;
        swordAttack.StopAttack();
    }

}