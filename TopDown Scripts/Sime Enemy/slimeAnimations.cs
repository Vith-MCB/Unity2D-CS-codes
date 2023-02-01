using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAnimations : MonoBehaviour
{
    private Animator slimeAnimator;
    private SpriteRenderer spriteRenderer;

    private string currentState;

    #region Animations States constants
    private const string SLIME_IDLE = "slimeIdle";

    private const string SLIME_AWARE = "slimeAware";

    private const string SLIME_JUMP = "slimeJump";

    private const string SLIME_DEAD = "slimeDead";

    #endregion

    void Start()
    {
        slimeAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void ChangeAnimationState(string newState)
    {
        //Stop the same animation from interrupting itself
        if (currentState == newState) return;

        //Play animations
        slimeAnimator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

/*
    public void AnimationsState()
    {
        if (slimeAI.isAware)
        {
            ChangeAnimationState(SLIME_AWARE);
        }
        else if (slimeAI.isDead)
        {
            ChangeAnimationState(SLIME_DEAD);
        }
        else if (slimeAI.isJumping)
        {
            ChangeAnimationState(SLIME_JUMP);
        }
        else
        {
            ChangeAnimationState(SLIME_IDLE);
        }
    }
    */
}
