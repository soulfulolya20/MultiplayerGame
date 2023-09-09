using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject playerObject;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;
    
    public float movementSpeed = 7f;
    public float jumpForce = 14f;

    protected abstract string HorizontalInputAxis { get; }
    protected abstract string JumpInputButton { get; }

    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    private void Start()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float dirX = Input.GetAxisRaw(HorizontalInputAxis);

        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown(JumpInputButton) && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        float dirX = Input.GetAxisRaw(HorizontalInputAxis);
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}