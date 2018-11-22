using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour {
    public float speed = 1f;
    public float maxLiftTime = 2f;
    public float instantiateTime = 0f;
    // Use this for initialization
    void Start()
    {
        instantiateTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        //if (Time.time - instantiateTime > maxLiftTime)
        //Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
