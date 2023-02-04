using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour, IDamageable
{
    #region Animation Variables
    public static bool isAware = false;
    public static bool isDead = false;
    public static bool isJumping = false;

    public static bool gotDamaged = false;

    public static bool isInvencible = false;

    #endregion


    public float _health = 3f;
    public float Health {
        set{
            _health = value;
            if(_health <= 0){
                Defeated();
            }
        }
        get{
            return _health;
        }
    }

    public Rigidbody2D slimeRb;
    private Collider2D slimeCollider;

    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Debug.Log(Health);
    }

    private void Defeated(){
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            Debug.Log("Player hit");
        }
    }

    public void OnHit(float damage){
        Debug.Log(isInvencible);
        Health -= damage;
        if(Health > 0){
            gotDamaged = true;
            isInvencible = true;
        }
        
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        if(Health > 0){
            gotDamaged = true;
            isInvencible = true;
        }
        //Apply force to the slime
        slimeRb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
