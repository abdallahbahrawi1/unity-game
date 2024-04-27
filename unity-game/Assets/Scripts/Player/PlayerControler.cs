using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{


    [SerializeField] Transform groundCheck;
    bool isGrounded = false;
    int jumpCount = 1;
    float jumpforce = 11f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField]float dashSpeed = 20f;
    [SerializeField]bool isDashing = false;
    [SerializeField]float dashTime = 0f;
    float startDashTime = 0.2f;




    private Vector2 _moveInput;
    [SerializeField] Rigidbody2D _rb;
    Animator _animator;
    [SerializeField]
    private bool _isMoving = false; 
    [SerializeField]
    // private bool _isIdle = false; 
    public float walkSpeed = 5f;
    public bool IsMoving { 
        get {
            return _isMoving;
        }
        private set{
            _isMoving = value;
            _animator.SetBool("isMoving", value);   
        }
    }
    public bool isfacingRight = true;

    public bool IsFacingRight  { 
        get {
            return isfacingRight ;
        }
        private set{
            if(isfacingRight != value){ 
                transform.localScale *= new Vector2(-1, 1);
            }
            isfacingRight = value;
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Dashing on left-shift click
        if(Input.GetKeyDown("left shift")){
            if(!isDashing){
                walkSpeed += dashSpeed;
                isDashing = true;
                dashTime = startDashTime;
            }
        }
        //Dash timing
        if(dashTime<=0&&isDashing){
            isDashing = false;
            walkSpeed -= dashSpeed;
        }else if(dashTime>0){
            dashTime-= Time.deltaTime;
        }
        
        //Jumping logic
       isGrounded=Physics2D.BoxCast(groundCheck.position,new Vector2(2.5f,0.4f),0f,new Vector2(0f,0f),0.1f,groundLayer);
       if(isGrounded)jumpCount=1;
       
        if(Input.GetButtonDown("Jump")&&(isGrounded||jumpCount<2)){
              _rb.velocity= new Vector2(_rb.velocity.x,jumpforce);
              jumpCount++;    
        }
        ////////////////
        _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
    }
    

    private void FixedUpdate() {


        
    }

    public void OnMove(InputAction.CallbackContext context){
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput); 
    }
    public void OnJump(InputAction.CallbackContext context){
        
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
