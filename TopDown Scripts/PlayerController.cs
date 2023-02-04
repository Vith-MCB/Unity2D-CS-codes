using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Rigidbody2D player;

    #region Move
    private Vector2 movementInput;
    public float moveSpeed = 1f;

    public static string directionLooked = "";

    public static string facing = "Right";

    public static bool isRunning = false;
    #endregion
    
    #region Attack
    public static bool isAttacking = false;

    #endregion

    #region Collisions
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

    #endregion

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionLooked = GetPlayerDirection();
        attack();
    }

    void FixedUpdate() {
        if(!isAttacking)
            MovePlayer();
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }


    public void MovePlayer(){
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);

            if(!success){
                success = TryMove(new Vector2(movementInput.x,0)); //The function is called again using a new vector2, that way, the player can move even if it can't move vertically

                if(!success){
                    success = TryMove(new Vector2(0 ,movementInput.y));//The function is called again using a new vector2, that way, the player can move even if it can't move horizontally
                }
            }
        }
    }


    /*
     * This function checks for collisions in every direction and returns if player can move.
    */
    private bool TryMove(Vector2 direction){
        int count = player.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.deltaTime + collisionOffset
            );
        if(count == 0){
            player.MovePosition(player.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else{return false;}
    }

    private string GetPlayerDirection(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            if (horizontalInput > 0)
            {
                isRunning = true;
                facing = "Right";
                return "Right";

            }
            else if(horizontalInput == 0)
            {
                isRunning = false;
                return "Idle";
            }
            else
            {
                isRunning = true;
                facing = "Left";
                return "Left";
            }
        }
        else
        {
            if (verticalInput > 0)
            {
                isRunning = true;
                facing = "Up";
                return "Up";
            }
            else if (verticalInput == 0)
            {
                isRunning = false;
                return "Idle";
            }
            else
            {
                isRunning = true;
                facing = "Down";
                return "Down";
            }
        }
        
    }

    private void attack(){
        if(Input.GetKeyDown(KeyCode.K)){
            isAttacking = true;
        }
    }


}
