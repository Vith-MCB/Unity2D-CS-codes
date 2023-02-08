using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour, IDamageable
{
    #region Animation Variables
    slimeAnimations slimeAnim;
    public bool isAware = false;
    public bool isDead = false;
    public bool isJumping = false;

    public bool gotDamaged = false;

    public bool isInvencible = false;

    private const string SLIME_IDLE = "slimeIdle";

    private const string SLIME_AWARE = "slimeAware";

    private const string SLIME_JUMP = "slimeJump";

    private const string SLIME_DEAD = "slimeDead";

    private const string SLIME_HIT = "slimeDamage";

    #endregion

    [SerializeField] private LayerMask collisionLayerMask;

    private float chargingSpeed = 10f;
    public float _health = 3f;

    public bool _targetable = true;
    public float Health {
        set{
            _health = value;
            if(_health <= 0){
                Targetable = false;
                Defeated();
            }
        }
        get{
            return _health;
        }
    }

    public bool Targetable { get {return _targetable;}
    set {
        _targetable = value;
        slimeRb.simulated = value;
    } }

    public Rigidbody2D slimeRb;
    private Collider2D slimeCollider;

    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();

        slimeAnim = GetComponent<slimeAnimations>();

        isDead = false;
        isInvencible = false;
    }

    void Update()
    {
        float distance = SlimeDistanceCalculator();
        BasicAI(distance);
    }

    private void Defeated(){
        isDead = true;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("Player hit");
        }
    }

    public void OnHit(float damage){
        gotDamaged = true;
        Health -= damage;
        if(Health > 0){
            gotDamaged = true;
            isInvencible = true;
        }
        
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        gotDamaged = true;
        if(Health > 0){
            Health -= damage;
            gotDamaged = true;
            isInvencible = true;
        }
        //Apply force to the slime
        slimeRb.AddForce(knockback, ForceMode2D.Impulse);
    }


    //slime basic ai that triggers deppending on the distance to the player
    private float SlimeDistanceCalculator(){
        Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        return Vector3.Distance(parentPosition, playerPosition);
    }

    private bool IsPlayerVisible()
    {
        chargingDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

        // Cast a ray downwards from the center of the player's collider
        RaycastHit2D hit = Physics2D.Raycast(slimeCollider.bounds.center, chargingDirection, 5f, collisionLayerMask);

        // Check if the ray hit any colliders on the collision layer
        if (hit.collider == null)
        {
            return true;
        }
        else { return false; }
        
    }

    private bool isCharging = false;
    private Vector2 chargingDirection;

    private void Charge()
    {
        chargingDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        isCharging = true;
    }

    private void FixedUpdate()
    {
        if (isCharging)
        {
            // Use the Lerp function to smooth the movement
            slimeRb.velocity = Vector2.Lerp(slimeRb.velocity, chargingDirection * chargingSpeed, Time.deltaTime * 5);
        }
    }

    private void BasicAI(float Distance){
        if(Distance <= 1f && IsPlayerVisible()){
            isAware = true;
            if(!isCharging ){
                StartCoroutine(waiter());
            }
        }
        else{
            isAware = false;
        }
    }

    IEnumerator waiter()
    {
        //Wait for some seconds (for example, 2 seconds)
        yield return new WaitForSecondsRealtime(2);
        //charge at player direction
        if(!gotDamaged){Charge();}
        

        if(IsPlayerVisible()){
            //Wait for some seconds (for example, 2 seconds)
            yield return new WaitForSecondsRealtime(0.5f);
            isCharging = false;
        }
        else{
            isCharging = false;
        }
    }


}
