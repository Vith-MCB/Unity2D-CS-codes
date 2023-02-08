using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private Collider2D swordColUpDown;
    [SerializeField] private Rigidbody2D playerRB;

    private bool attackBool;

    public enum AttackDirection {Right, Left, Up, Down};

    public AttackDirection attackDirection;

    Vector2 rightAttackOffset;
    Vector2 downAttackOffset;
    private float damage = 1;

    public float knockbackForce = 10f;

    private void Start() {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.localPosition;
        downAttackOffset = swordColUpDown.transform.localPosition;

        playerRB = GetComponentInParent<Rigidbody2D>();
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
        Vector2 opositeDirection = new Vector2(-rightAttackOffset.x, 0f);
        playerRB.AddForce(opositeDirection, ForceMode2D.Impulse);
        swordColUpDown.enabled = false;//This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordCollider.enabled = true;
    }
    public void AttackLeft(){
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        Vector2 opositeDirection = new Vector2(rightAttackOffset.x, 0f);
        playerRB.AddForce(opositeDirection, ForceMode2D.Impulse);
        swordColUpDown.enabled = false;//This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordCollider.enabled = true;
    }
    public void AttackUp(){
        swordColUpDown.transform.localPosition = new Vector2(downAttackOffset.x, -downAttackOffset.y + 0.05f);
        Vector2 opositeDirection = new Vector2(0f, downAttackOffset.y);
        playerRB.AddForce(opositeDirection, ForceMode2D.Impulse);
        swordCollider.enabled = false; //This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordColUpDown.enabled = true;
    }
    public void AttackDown(){
        swordColUpDown.transform.localPosition = downAttackOffset;
        Vector2 opositeDirection = new Vector2(0f, -downAttackOffset.y);
        playerRB.AddForce(opositeDirection, ForceMode2D.Impulse);
        swordCollider.enabled = false; //This is to prevent the player from attacking with the sword and the swordColUpDown at the same time
        swordColUpDown.enabled = true;
    }
    public void StopAttack(){
        swordCollider.enabled = false;
        swordColUpDown.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null){
            //Calculating direction between player and slime
            //Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition).normalized;
            //Adding force to the slime
            Vector2 knockback = direction * knockbackForce;

            damageable.OnHit(damage, knockback);
        }else{
            Debug.Log("Object is not damageable");
        }
        
    }

    
}
