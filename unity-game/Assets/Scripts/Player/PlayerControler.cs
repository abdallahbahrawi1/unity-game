using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    private Vector2 _moveInput;
    Rigidbody2D _rb;
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
            _animator.SetBool(AnimationStrings.isMoving, value);   
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
        
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput); 
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
