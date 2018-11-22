using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heliControl : MonoBehaviour {
    public int speed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= transform.forward * speed;
	}
}
