using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed=1;

    //States
    private bool isAlive = true;

    //Caching
    private Rigidbody2D myRigidbody;
    private Animator myAnimator; 


	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        FlipSprite();
    }

    private void Run() {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2 (controlThrow*runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
          
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }
}
