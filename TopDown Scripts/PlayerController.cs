using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;

    #region Move
    private Vector2 movementInput;
    public float moveSpeed = 1f;
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

    void FixedUpdate() {
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

}
