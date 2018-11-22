using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryControl : MonoBehaviour {
    public GameObject target;
    public bool nearPlayer = false;
    public GameObject bullet;
    public float lastShootTime = 0;//上一次开枪时间
    private float shootInterval = 4f;//子弹时间间隔


    public GameObject destroyFireEffect;
    public GameObject destroySmockEffect;
    public GameObject destroySteamEffect;
    public AudioClip boomClip;

    void Start () {
		
	}
	
	void Update () {
        transform.LookAt(target.transform);
        if (nearPlayer == true)
        {
            if (Time.time - lastShootTime < shootInterval) return;
            Vector3 pos = gameObject.transform.position + gameObject.transform.forward * 8;
            Instantiate(bullet, pos, transform.rotation);
            lastShootTime = Time.time;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Collider co = gameObject.GetComponent<Collider>();
            co.enabled = false;
            foreach (Transform child in transform)
            {
                GameObject destroyObj1 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj2 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj3 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.7f;
                audioSource.PlayOneShot(boomClip);
                child.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
