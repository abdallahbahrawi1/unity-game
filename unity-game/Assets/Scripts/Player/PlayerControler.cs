using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerControler : MonoBehaviour
{
    int jumpCount = 1;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashTime = 0f;
    float startDashTime = 0.2f;
    private Vector2 _moveInput;
    [SerializeField] Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    [SerializeField]
    private bool _isMoving = false; 
    [SerializeField] 
    private bool _isDashing = false;
    // [SerializeField]
    // private bool _isIdle = false; 
    public float jumpforce = 11f;
    public float walkSpeed = 5f;


    public float CurrentMoveSpeed {
        get {
            if(IsMoving && !touchingDirections.IsOnWall){
                if(IsDashing){
                    return dashSpeed;
                }else{
                    return walkSpeed;
                }
            }else {
                return 0;
            }
        }

    }
    public bool IsMoving { 
        get {
            return _isMoving;
        }
        private set{
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);   
        }
    }
    public bool IsDashing { 
        get {
            return _isDashing;
        }
        private set{
            _isDashing = value;
            animator.SetBool(AnimationStrings.isMoving, value);   
        }
    }
    private bool _isfacingRight = true;

    public bool IsFacingRight  { 
        get {
            return _isfacingRight ;
        }
        private set{
            if(_isfacingRight != value){ 
                transform.localScale *= new Vector2(-1, 1);
            }
            _isfacingRight = value;
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    void Start()
    {
        
    }

    void Update()
    {        
        if(dashTime <= 0 && _isDashing){
            _isDashing = false;
        }else if(dashTime > 0){
            dashTime-= Time.deltaTime;
        }
        rb.velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    private void FixedUpdate() {
        
    }

    public void OnMove(InputAction.CallbackContext context){
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput); 
    }

    public void OnDash(InputAction.CallbackContext context){
        if(context.started && !_isDashing){
            _isDashing = true;
            dashTime = startDashTime;
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        //TODO CHECK IF ALIVE AS WELL
        if(context.started && touchingDirections.IsGrounded){
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity= new Vector2(rb.velocity.x, jumpforce);
            jumpCount = 2;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight){
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}
