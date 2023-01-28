using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    private const string PLAYER_DAMAGE = "playerDamage";
    private const string PLAYER_DEATH = "death";
    public static bool animationEnded = true;

    private string currentState;

    private Animator animations;

    //Sound
    [SerializeField] private AudioSource damageSnd;

    //Player variables
    private int playerLife = 555555;
    private Rigidbody2D player;
    private bool damageble = true;

    //Knock back
    private float knockbackForce = 25f;

    private void Start()
    {
        animations = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(animationEnded)
        {
            currentState = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        Vector2 direction = (collider.transform.position - transform.position).normalized;

        if (collision.gameObject.CompareTag("Spikes") && damageble)
        {
            playerLife -= 1;
            ChangeAnimationState(PLAYER_DAMAGE);
            ApplyRecoil(direction * knockbackForce);
            StartCoroutine(CheckIfCanTakeDamage());

            damageSnd.Play();
        }

        if (playerLife == 0)
        {
            animations.SetTrigger(PLAYER_DEATH);
            player.bodyType = RigidbodyType2D.Static;
        }
    }

    private void ChangeAnimationState(string newState)
    {
        
        //Stop the same animation from interrupting itself
        if (currentState == newState) return;

        //Play animations
        animations.Play(newState);

        //reassign the current state
        currentState = newState;

        animationEnded = false;
    }

    private void AnimationEnded()
    {
        animationEnded = true;
    }

    private IEnumerator CheckIfCanTakeDamage()
    {
        damageble = false;
        yield return new WaitForSeconds(1f);
        currentState = null;
        animationEnded = true;
        damageble = true;
    }

    private void Die()
    {
        RestartLvl();
    }

    private void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ApplyRecoil(Vector2 force)
    {
        player.AddForce(new Vector2(-force.x, -force.y), ForceMode2D.Impulse);
    }
}
