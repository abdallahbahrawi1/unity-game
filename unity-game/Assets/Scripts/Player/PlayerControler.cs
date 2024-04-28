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
    float jumpforce = 9f;
    float doubleJumpForce = 7f;
    [SerializeField] LayerMask groundLayer;
    float dashSpeed = 20f;
    bool isDashing = false;
    float dashTime = 0f;
    float startDashTime = 0.2f;
    float dashCDTime = 0f;
    float dashCD = 2.5f;




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
        //Dash timing
        if(dashTime<=0&&isDashing){
            isDashing = false;
            walkSpeed -= dashSpeed;
        }else if(dashTime>0){
            dashTime-= Time.deltaTime;
        }

        //dash cd timing
        if(dashCDTime>0){
             dashCDTime -= Time.deltaTime;
        }
        

        _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
    }
    

    private void FixedUpdate() {
       //ground check
       isGrounded=Physics2D.BoxCast(groundCheck.position,new Vector2(2.5f,0.4f),0f,new Vector2(0f,0f),0.1f,groundLayer);
       if(isGrounded)jumpCount=1;
        
    }

    public void OnMove(InputAction.CallbackContext context){
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput); 
    }
    public void OnDash(InputAction.CallbackContext context){
         if(!isDashing&&dashCDTime<=0){
                walkSpeed += dashSpeed;
                isDashing = true;
                dashTime = startDashTime;
                dashCDTime = dashCD;
            }

    }


    public void OnJump(InputAction.CallbackContext context){
        if(isGrounded||jumpCount<=2){
            if(!isGrounded&&jumpCount==2)_rb.velocity= new Vector2(_rb.velocity.x,doubleJumpForce);
            else  _rb.velocity= new Vector2(_rb.velocity.x,jumpforce);
              jumpCount++;    
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
