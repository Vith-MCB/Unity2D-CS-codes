using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int bananaCount = 0;

    [SerializeField] private Text bananasText;

    [SerializeField] private AudioSource collectableSnd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColecItem"))
        {
            collectableSnd.Play();
            Destroy(collision.gameObject);
            bananaCount++;
            bananasText.text = "Bananas: " + bananaCount.ToString();
        }
    }
}
