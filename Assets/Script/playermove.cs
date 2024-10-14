using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public Transform groundCheckCollider;
    public LayerMask groundLayer;
    public Transform wallCheckCollider;
    public LayerMask wallLayer;
    
    const float groundCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;
    [SerializeField] float speed;
    [SerializeField] float jumpPower = 500;
    [SerializeField] float slideFactor = 0.2f;
    [SerializeField] int totalJumps;
    int availableJumps;
    float horizontalValue;

    [SerializeField] bool isGrounded;
    bool facingRight = true; 
    bool isSliding;
    bool wallJumping;
    bool isDead = false;
    bool multipleJump;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    void Awake()
    {
        availableJumps = totalJumps;
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (CanMove()==false)
            return;
        
        animator.SetFloat("yVelocity", rb.velocity.y);
        
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();

        }
        
        WallCheck();
    }
    
    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }
    bool CanMove()
    {
        bool can = true;

        if(isDead)
            can = false;

        return can;
    }
    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        
        if(colliders.Length > 0)
        {
            isGrounded = true;
            if(!wasGrounded)
                availableJumps = totalJumps;
        }

        animator.SetBool("Jump", !isGrounded);


    }

    #region Wall Slide & Jump


    void WallCheck()
    {
        if(Physics2D.OverlapCircle(wallCheckCollider.position,wallCheckRadius,wallLayer)
            && Mathf.Abs(horizontalValue) > 0
            && rb.velocity.y<0
            && !isGrounded)
            {
                Vector2 v = rb.velocity;
                v.y = -slideFactor;
                rb.velocity = v;
                isSliding = true;
            }
            else
            {
                isSliding = false;
            }
            if (Input.GetButtonDown("Jump") && isSliding == true)
            {
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }
            if (wallJumping == true)
            {
                rb.velocity = new Vector2(xWallForce * -horizontalValue, yWallForce);
            }
    }
    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    #endregion
    void Jump()
    {
        if(isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        } else
        {
            if(multipleJump && availableJumps>0)
            {
                availableJumps--;
            
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }
    void Move(float dir)
    {
        
        
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;

        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        } else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
    }

    public void Die()
    {
        isDead = true;
        FindObjectOfType<levelmanager>().Restart();
    }
}
