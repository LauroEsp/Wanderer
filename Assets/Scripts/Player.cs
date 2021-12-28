using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbodyComponent;
    private Animator animatorComponent;
    private SpriteRenderer spriteComponent;
    private CapsuleCollider2D colliderComponent;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask solidGround;
    private enum MovementState {idle, running, jumping, falling};
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        animatorComponent = GetComponent<Animator>();
        spriteComponent = GetComponent<SpriteRenderer>();
        colliderComponent = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpKeyWasPressed = true;
        }

        UpdateAnimationStates();
    }

    private void FixedUpdate() 
    {
        rigidbodyComponent.velocity = new Vector2(horizontalInput * moveSpeed, rigidbodyComponent.velocity.y);
        
        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpKeyWasPressed = false;
        }
    }

    private void UpdateAnimationStates()
    {
        MovementState state;

        if (horizontalInput > 0f)
        {
            state = MovementState.running;
            spriteComponent.flipX = false;
        }
        else if (horizontalInput < 0f)
        {
            state = MovementState.running;
            spriteComponent.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigidbodyComponent.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rigidbodyComponent.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animatorComponent.SetInteger("animationIndex", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.CapsuleCast(colliderComponent.bounds.center, colliderComponent.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, .1f, solidGround);
    }
}
