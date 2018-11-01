// Date   : 13.10.2018 01:12
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{


    private Rigidbody2D rb2d;

    private bool debugMode = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
#if UNITY_EDITOR
        debugMode = true;
#endif
    }

    [SerializeField]
    private List<Transform> groundCheckPositions;
    private bool grounded = false;
    private bool allowJumping = false;
    private bool facingRight = false;
    [SerializeField]
    private float moveForce = 365f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float jumpForce = 1000f;
    [SerializeField]
    private float maxFallSpeed = 20f;

    [SerializeField]
    private float startingSpeed = 0.4f;

    [SerializeField]
    [Range(0.001f, 0.2f)]
    private float minHAxis = 0.05f;

    [SerializeField]
    [Range(0f, 0.8f)]
    private float groundedLingerTime = 0.1f;
    private float lingerTimer = 0f;

    [SerializeField]
    private Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (debugMode)
        {
            GameManager.main.UpdateLingerTimer(lingerTimer);
        }
        grounded = lingerTimer > 0f;
        if (!grounded)
        {
            foreach (Transform groundCheck in groundCheckPositions)
            {
                if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
                {

                    lingerTimer = groundedLingerTime;
                    grounded = true;
                    break;
                }
            }
        }
        else
        {
            lingerTimer -= Time.deltaTime;
        }

        if (debugMode)
        {
            GameManager.main.UpdateGrounded(grounded);
        }

        if (grounded && KeyManager.main.GetKeyDown(Action.Jump))
        {
            allowJumping = true;
            lingerTimer = -1f;
            grounded = false;
            animator.SetTrigger("Jump");
        }
        else if (KeyManager.main.GetKeyDown(Action.Jump))
        {
            Debug.Log("Wasn't grounded");
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        //anim.SetFloat("Speed", Mathf.Abs(h));
        bool walking = Mathf.Abs(h) > 0.01f;
        animator.SetBool("Walk", walking);
        if (h * rb2d.velocity.x < maxSpeed)
        {
            float speed = h;
            if (Mathf.Abs(h) > minHAxis)
            {
                float factor = h > 0 ? 1 : -1;
                speed = Mathf.Abs(h) > startingSpeed ? h : startingSpeed * factor;
                Debug.Log("speed: " + speed);
            }
            rb2d.AddForce(Vector2.right * speed * moveForce, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.y > maxFallSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxFallSpeed);
        }

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (allowJumping)
        {
            //anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            allowJumping = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
