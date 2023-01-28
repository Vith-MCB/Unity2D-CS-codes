using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoviment : MonoBehaviour
{
    public static Rigidbody2D player;
    private BoxCollider2D playerCollider;

    #region Swimming Variables
    private float swimVelocity = 0.9f;
    private float maxSwimSpeed = 3f;
    private bool isVelocityCappingOn = true;

    public static float dirX = 0f;
    public static float dirY = 0f;
    #endregion

    #region Mechanics
    private static float gravityFloating = 2f;
    #endregion

    


    void Start()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        PlayerMoviment();

        CapVelocity();

        waterGravity();

    }

    /// <summary>
    /// Function used to handle player input
    /// </summary>
    private void PlayerMoviment()
    {
        #region Swim Variables
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(dirX, dirY).normalized;
        Vector2 swim = direction * swimVelocity;
        #endregion

        #region Checking User Input
        if (dirX != 0 || dirY != 0)
        {
            player.AddForce(swim, ForceMode2D.Impulse);
        }
        else
        {
            player.velocity = new Vector2(0f, 0f);
        }
        #endregion

    }

    /// <summary>
    /// This function caps player speed to the max speed
    /// </summary>
    private void CapVelocity()
    {
        if (isVelocityCappingOn)
        {
            if (player.velocity.magnitude > maxSwimSpeed)
            {
                player.velocity = Vector3.ClampMagnitude(player.velocity, maxSwimSpeed);
            }
        }
        else return;
    }


    /// <summary>
    /// This function calculates gravity of player based on oxygen
    /// </summary>
    private void waterGravity()
    {
        if (oxygenHandler.playerOxygen > 75) { player.gravityScale = -gravityFloating; }
        else { player.gravityScale = gravityFloating; }
    }
}
