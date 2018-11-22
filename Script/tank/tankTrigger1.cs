using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankTrigger1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<tankControl>().meetPlayer = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<tankControl>().meetPlayer = false;
        }
    }
}
