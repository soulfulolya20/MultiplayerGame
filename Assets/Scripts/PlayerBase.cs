using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class PlayerBase : NetworkBehaviour
{
    private Rigidbody2D rb;
    public GameObject playerObject;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private MovementState _state;
    [SerializeField] private bool flipX;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Camera _camera;
    
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
        if (!IsOwner || !IsSpawned)
        {
            return;
        }
        float dirX = Input.GetAxisRaw(HorizontalInputAxis);

        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        if (Input.GetButtonDown(JumpInputButton) && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
        HandleAnimationServerRpc(_state);
        ProvideFlipStateToServerRpc(flipX);
    }

    private void UpdateAnimationState()
    {
        float dirX = Input.GetAxisRaw(HorizontalInputAxis);
        switch (dirX)
        {
            case > 0f:
                _state = MovementState.running;
                flipX = false;
                break;
            case < 0f:
                _state = MovementState.running;
                flipX = true;
                break;
            default:
                _state = MovementState.idle;
                break;
        }

        switch (rb.velocity.y)
        {
            case > .1f:
                _state = MovementState.jumping;
                break;
            case < -.1f:
                _state = MovementState.falling;
                break;
        }

        anim.SetInteger("state", (int)_state);
        sprite.flipX = flipX;
    }

    private bool IsGrounded()
    {
        var bounds = coll.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 
            0f, Vector2.down, .1f, jumpableGround);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.LogWarning("OnNetworkSpawn");
        Debug.LogWarning(IsLocalPlayer);
        Debug.LogWarning(IsOwner);
        Debug.LogWarning(IsHost);
        GameManager.RegisterPlayer(this);
        if(!IsLocalPlayer)
        {
            _camera.enabled = false;
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        GameManager.RemovePlayer(this);
    }
    
    [ServerRpc]
    private void HandleAnimationServerRpc(MovementState state)
    {
        anim.SetInteger("state", (int)state);
    }
    
    [ServerRpc]
    void ProvideFlipStateToServerRpc(bool state)
    {
        // make the change local on the server
        sprite.flipX = state;

        // forward the change also to all clients
        SendFlipStateClientRpc(state);
    }

// invoked by the server only but executed on ALL clients
    [ClientRpc]
    void SendFlipStateClientRpc(bool state)
    {
        // skip this function on the LocalPlayer 
        // because he is the one who originally invoked this
        if(IsLocalPlayer) return;

        //make the change local on all clients
        sprite.flipX = state;
    }
}