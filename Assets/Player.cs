using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    //States
    private bool isAlive = true;

    //Caching
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private Collider2D myCollider2D;
    


	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run() {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2 (controlThrow*runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);     
    }

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder() {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; }

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow*climbSpeed);
            myRigidbody.velocity = climbVelocity;

        bool playerHasVerticalVelocity = Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalVelocity);

  
    }



    private void FlipSprite() {
        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }
}
