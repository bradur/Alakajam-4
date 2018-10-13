// Date   : 13.10.2018 01:12
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


    private Rigidbody2D rb2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    [SerializeField]
    private Transform groundCheck;
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
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (grounded && KeyManager.main.GetKeyDown(Action.Jump))
        {
            allowJumping = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        //anim.SetFloat("Speed", Mathf.Abs(h));


        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

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