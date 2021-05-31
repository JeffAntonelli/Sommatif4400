﻿using UnityEngine;
using System;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    private enum State
    {
        None,
        PlayerIdle,
        PlayerRun,
        Jump,
        Bouncing
    }

    private State _currentState = State.None;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerFoot foot;

    private bool _isJumping = false;
    private bool _facingRight = true;
    private bool _jumpButtonDown = false;
    private bool _jumpButton = false;
    private bool _isSwitching = false;
    private bool isbouncing_ = false;

    private bool top; // Pour la rotation.


    private float jumpTimeCounter_;
    [SerializeField] private const float JumpTime = 0.5f;
    [SerializeField] private const float DeadZone = 0.1f; 
    [SerializeField] private float jumpSpeed = 5.0f;
    [SerializeField] private float bouncingHeight = 2.0f;
    const float MoveSpeed = 15.0f;


    void Start()
    {
        ChangeState(State.Jump);
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpButtonDown = true;
        }

        if (Input.GetButton("Jump"))
        {
            _jumpButton = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _isJumping = false;
        }

    }

    void FixedUpdate()
    {

        if (foot.FootContact_ > 0 && _jumpButtonDown)
        {
            Jump();
        }
        _jumpButtonDown = false;

        if (_jumpButton && _isJumping) // Jump en fonction de la durée de l'input
        {
            JumpVariation();
        }
        _jumpButton = false;

        if (foot.FootContact_ > 0 && CompareTag("enemy"))
        {
            Bouncing();
        }


        var vel = body.velocity;
        body.velocity = new Vector2(MoveSpeed * Input.GetAxis("Horizontal"), vel.y);
        //We flip the characters when not facing in the right direction
        if (Input.GetAxis("Horizontal") > DeadZone && !_facingRight)
        {
            FlipX();
        }

        if (Input.GetAxis("Horizontal") < -DeadZone && _facingRight)
        {
            FlipX();
        }
        //We manage the state machine of the character
        switch (_currentState)
        {
            case State.PlayerIdle:
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > DeadZone)
                {
                    ChangeState(State.PlayerRun);
                }

                if (foot.FootContact_ == 0)
                {
                    ChangeState(State.Jump);
                }
                break;
            case State.PlayerRun:
                if (Mathf.Abs(Input.GetAxis("Horizontal")) < DeadZone)
                {
                    ChangeState(State.PlayerIdle);
                }

                if (foot.FootContact_ == 0)
                {
                    ChangeState(State.Jump);
                }
                break;
            case State.Jump:
                if (foot.FootContact_ > 0)
                {
                    ChangeState(State.PlayerIdle);
                }
                
                if (foot.FootContact_ > 0 && CompareTag("enemy"))
                {
                    ChangeState(State.Bouncing);
                }
                break;
            case State.Bouncing:
                if (foot.FootContact_ == 0)
                {
                    ChangeState(State.Jump);
                }
                
                if (foot.FootContact_ > 0)
                {
                    ChangeState(State.PlayerIdle);
                }
                break;
                
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void Jump()
    {
        Debug.Log("Work");
        _isJumping = true;
        jumpTimeCounter_ = JumpTime;
        var vel = body.velocity;
        body.velocity = new Vector2(vel.x, jumpSpeed);
    }

    private void Bouncing()
    {
        isbouncing_ = true;
        transform.position = new Vector3(body.velocity.x, bouncingHeight, 0);
    }


    private void JumpVariation()
    {
        if (jumpTimeCounter_ > 0)
        {
            var vel = body.velocity;
            body.velocity = new Vector2(vel.x, jumpSpeed);
            jumpTimeCounter_ -= Time.deltaTime;
        }
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.PlayerIdle:
                anim.Play("Player_Idle");
                break;
            case State.PlayerRun:
                anim.Play("Player_Walk");
                break;
            case State.Jump:
                anim.Play("Player_Jump");
                break;
            case State.Bouncing:
                anim.Play("Player_Jump");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        _currentState = state;
    }

    void FlipX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        _facingRight = !_facingRight;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
