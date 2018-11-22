using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger22 : MonoBehaviour {
    void Start()
    {

    }

    void Update()
    {
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<characterControl2>().nearPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<characterControl2>().nearPlayer = false;
        }
    }
}
