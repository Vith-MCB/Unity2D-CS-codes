using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oxygenHandler : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D playerCollider;

    private float playerDepth;
    [SerializeField] private Text depthText;

    [SerializeField] private LayerMask oxygenLayerMask;

    #region Oxygen variables
    private float oxygenMax = 100f;
    public static float playerOxygen;
    public OxygenBar oxygenBar;
    private float depthUsage;
    #endregion

    void Start()
    {
        playerOxygen = oxygenMax;
        oxygenBar.SetMaxOxBar(oxygenMax);
        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        PlayerOxigenCalculator();

        CheckOxygenLayer();

        AddOxygen();

        CalculatePlayerDepth();
    }

    /// <summary>
    /// This function is used to calculate player depth based in its position
    /// </summary>
    private void CalculatePlayerDepth()
    {
        playerDepth = -(player.position.y)/5;

        depthText.text = "Depth: " + ((int)playerDepth).ToString();
    }

    /// <summary>
    /// This function calculates the player oxygen
    /// </summary>
    private void PlayerOxigenCalculator()
    {
        #region Definig oxygen use rate based on depth
        if (playerDepth < 2f){ depthUsage = 1f; }
        else if(playerDepth >= 2f && depthUsage < 5f) { depthUsage = 1.7f; }
        else { depthUsage = 2f; }
        #endregion

        //Player Oxygen will be gone faster if player moves
        float oxygenUseRate = depthUsage * Time.deltaTime;

        playerOxygen -= oxygenUseRate;

        oxygenBar.SetOxBar(playerOxygen);
    }

    /// <summary>
    /// Checks if player touched Oxygen layer
    /// </summary>
    public bool CheckOxygenLayer()
    {
        // Cast a ray downwards from the center of the player's collider
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.up, playerCollider.bounds.extents.y + 0.4f, oxygenLayerMask);

        // Check if the ray hit any colliders on the ground layer
        if (hit.collider == null)
        {
            return false;
        }
        else { return true; }
    }


    /// <summary>
    /// This function checks if player took a breath on Oxygen layer
    /// </summary>
    private void AddOxygen()
    {
        if (CheckOxygenLayer())
        {
            if(playerOxygen < oxygenMax)
            {
                playerOxygen += 7f * Time.deltaTime;
            }
        }
    }
}
