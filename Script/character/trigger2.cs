using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<characterControl>().nearPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<characterControl>().nearPlayer = false;
        }
    }
}
