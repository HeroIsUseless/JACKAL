using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryTrigger : MonoBehaviour {

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
            GetComponentInParent<BatteryControl>().nearPlayer = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<BatteryControl>().nearPlayer = false;
        }
    }
}
