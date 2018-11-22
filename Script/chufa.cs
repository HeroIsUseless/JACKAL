using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chufa : MonoBehaviour {
    public GameObject heli;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            heliControl helic = heli.GetComponent<heliControl>();
            helic.speed = 2;
        }
    }
}
