using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundMask;
    
    private CapsuleCollider2D collider;
    private Rigidbody2D rigidbody;
    private Animator animation;
    
    private float horizontalInput;
    private bool isJumping;
    private bool isFalling;
    
    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }
    
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.1f)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
        
        //* Hold jump button to adjust jump height
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && rigidbody.linearVelocity.y > 0)
        // {
        //     rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, rigidbody.linearVelocity.y / 2);
        // }
        
        animation.SetBool("isRun", IsGrounded() && horizontalInput != 0);
        
        if (rigidbody.linearVelocity.y > 0 && !isJumping)
        {
            isJumping = true;
            isFalling = false;
            animation.SetBool("isJumping", true);
            animation.SetBool("isFalling", false);
        }
        else if (rigidbody.linearVelocity.y < 0 && !isFalling)
        {
            isJumping = false;
            isFalling = true;
            animation.SetBool("isJumping", false);
            animation.SetBool("isFalling", true);
        }
        else if (IsGrounded())
        {
            isJumping = false;
            isFalling = false;
            animation.SetBool("isJumping", false);
            animation.SetBool("isFalling", false);
        }
        
        rigidbody.linearVelocity = new Vector2(horizontalInput * speed, rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        rigidbody.linearVelocity = new Vector2(transform.localScale.x, jumpHeight);
    }
    
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + 0.1f, groundMask);
        return hit.collider != null;
    }
}
