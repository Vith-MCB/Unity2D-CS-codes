using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private Collider2D swordColUpDown;

    private bool attackBool;

    public enum AttackDirection {Right, Left, Up, Down};

    public AttackDirection attackDirection;

    Vector2 rightAttackOffset;
    Vector2 downAttackOffset;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.localPosition;
        downAttackOffset = swordColUpDown.transform.localPosition;
    }

    private void Update() {
        attackBool = PlayerController.isAttacking;
        if(attackBool){Attack();}
        else{StopAttack();}
    }

    public void Attack(){
        switch(attackDirection){
            case AttackDirection.Right:
                AttackRight();
                break;
            case AttackDirection.Left:
                AttackLeft();
                break;
            case AttackDirection.Up:
                AttackUp();
                break;
            case AttackDirection.Down:
                AttackDown();
                break;
        }
    }
    public void AttackRight(){
        transform.localPosition = rightAttackOffset;
        swordColUpDown.enabled = false;//This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordCollider.enabled = true;
    }
    public void AttackLeft(){
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        swordColUpDown.enabled = false;//This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordCollider.enabled = true;
    }
    public void AttackUp(){
        swordColUpDown.transform.localPosition = new Vector2(downAttackOffset.x, -downAttackOffset.y);
        swordCollider.enabled = false; //This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordColUpDown.enabled = true;
    }
    public void AttackDown(){
        swordColUpDown.transform.localPosition = downAttackOffset;
        swordCollider.enabled = false; //This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordColUpDown.enabled = true;
    }
    public void StopAttack(){
        swordCollider.enabled = false;
        swordColUpDown.enabled = false;
    }
    
}
