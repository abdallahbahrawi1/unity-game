using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ProjectileScript : MonoBehaviour{

    [SerializeField] GameObject prefab;

    void Start(){
    StartCoroutine(SelfDestruct());
}

IEnumerator SelfDestruct()
{
    yield return new WaitForSeconds(1f);
    Destroy(prefab);
}


    private Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(float speed , Vector2 direction){
             rb.velocity = direction * speed;
    }
}
