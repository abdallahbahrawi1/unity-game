using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SelfDestruct());
    }

IEnumerator SelfDestruct()
{
    yield return new WaitForSeconds(0.1f);
    Destroy(this.gameObject);
}
    // Update is called once per frame
}
