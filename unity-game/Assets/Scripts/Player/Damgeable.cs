using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damgable : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100;
    private Animator animator;
    private bool _isInvinciable = false;
    private float _timeSinceHit = 0;
    public float MaxHealth
    {
        get {
            return _maxHealth;
        }
        set {
            _maxHealth = value;
        }
    }

    [SerializeField] private float _health = 100;
    public float Health {
        get {
            return _health;
        }
        set {
            _health = value;

            if(_health < 0){
                IsAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;

    public bool IsAlive { 
        get { return _isAlive; } 
        set { 
            _isAlive  = value; 
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }
    public float invinciablityTime = 0.25f;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(_isInvinciable){
            if(_timeSinceHit > invinciablityTime){
                _isInvinciable = false;
                _timeSinceHit = 0;
            }
            _timeSinceHit += Time.deltaTime;
        }

        // apply hit to the object
        // Hit(10);
    }

    public void Hit(int damage){
        if(IsAlive && !_isInvinciable){
            Health -= damage;
            _isInvinciable = true;
        }
    }

}
