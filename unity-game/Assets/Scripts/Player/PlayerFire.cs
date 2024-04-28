using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEngine.InputSystem;
using Unity.Mathematics;


public class PlayerFire : MonoBehaviour
{
    [SerializeField]float projectileSpeed = 20f;
    [SerializeField] Transform projectileSpawn;
    Vector2 value;
    Vector3 rotations;
    [SerializeField] ProjectileScript projectilePrefab;


    void Update(){
        
    }
    
    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
        var projectile = Instantiate(projectilePrefab,projectileSpawn.position+projectileSpawn.up,projectileSpawn.rotation);
        projectile.Fire(projectileSpeed,projectileSpawn.up);
       }
    }
    
    /*public void OnLook(InputAction.CallbackContext context){
            value = context.ReadValue<Vector2>();
            projectileSpawn.Rotate(0,0,value.x*0.1f);
            rotations = projectileSpawn.eulerAngles;
            if(rotations.z > 180) rotations.z -= 360;
            rotations.z = Mathf.Clamp(rotations.z , -90 , 90);
            projectileSpawn.eulerAngles = rotations;
            
    }*/
}
