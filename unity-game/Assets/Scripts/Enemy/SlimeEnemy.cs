using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class SlimeEnemy : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 5f;
    public DetectionZone attackZone;
    public DetectionZone sightingZone;
    bool hasTargetInSight;

    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;

    public enum WalkableDirection { Right, Left }
    WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection {
        get { return _walkDirection; }
        set {
            if(_walkDirection != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right){
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left){
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    bool _hasTarget = false;
    public float walkStopRate = 0.05f;

    public bool HasTarget { 
        get {
            return _hasTarget;
        }
        set {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }

    public bool CanMove {
        get {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        hasTargetInSight = sightingZone.detectedColliders.Count > 0 ;
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall){
            FlipDirection();
        }

        if(CanMove && !hasTargetInSight){
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }else if(CanMove && hasTargetInSight){
            rb.velocity = new Vector2(sprintSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right){
            WalkDirection = WalkableDirection.Left;
        } else if(WalkDirection == WalkableDirection.Left){
            WalkDirection = WalkableDirection.Right;
        }else{
            Debug.LogError("cureent walkable direction is not set to right or left.");
        }
    }
}
