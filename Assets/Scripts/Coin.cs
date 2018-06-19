using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pickupAmount;
    private bool coinPicked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!coinPicked)
        {
            coinPicked = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToScore(pickupAmount);
        }

    }
}
