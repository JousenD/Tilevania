using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (25f,25f);

    //States
    private bool isAlive = true;
    //private bool isJumping = false;

    //Caching
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private float gravityScaleAtStart;


	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;

    }
	
	// Update is called once per frame
	void Update () {

        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
        //Debug.Log("Jumping: " + isJumping);
        //Debug.Log("Touching Ground: "+ myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")));
        //Debug.Log("Touching Climbing: "+ myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")));
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
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }


        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {

            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");

            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
        }
    }

    private void ClimbLadder() {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return; }

            myRigidbody.gravityScale = 0;
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
