using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 100f;
    public float maxLiftTime = 2f;
    public float instantiateTime = 0f;
    public bool canDestroy = false;
    int s = 0;
	// Use this for initialization
	void Start () {
        instantiateTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= transform.forward * speed * Time.deltaTime;
       if(canDestroy)
       {
           s++;
           if(s > 20)
           {
               Destroy(gameObject);
           }
       }
	}
}
