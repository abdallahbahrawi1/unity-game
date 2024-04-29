using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
 
public class ProjectileScript : MonoBehaviour{

  [SerializeField] GameObject prefab;


    void Start(){
    StartCoroutine(SelfDestruct());
}
private void OnTriggerEnter2D(Collider2D other) {
          var point = other.ClosestPoint(this.gameObject.transform.position);
          var normal = this.gameObject.transform.position - new Vector3(point.x,point.y,0);
          Instantiate(prefab,point ,Quaternion.Euler(normal.x,normal.y,normal.z));
          if(other.gameObject.layer == 6){
            Destroy(this.gameObject);
          }
}
   

IEnumerator SelfDestruct()
{
    yield return new WaitForSeconds(2f);
    Destroy(this.gameObject);
}


    private Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(float speed , Vector2 direction){
             rb.velocity = direction * speed;
    }
}
