using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankControl : MonoBehaviour {
    public GameObject player;

    public bool meetPlayer = false;
    public bool nearPlayer = false;
    int s;

    public GameObject bullet;
    public float lastShootTime = 0;//上一次开枪时间
    private float shootInterval = 5f;//子弹时间间隔

    public GameObject deadBody;

    public GameObject destroyFireEffect;
    public GameObject destroySmockEffect;
    public GameObject destroySteamEffect;
    public AudioClip boomClip;

    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.velocity = Vector3.zero;
        if (meetPlayer == false)
            OnMove();
        else
            OnAttack();
    }

    void OnMove()
    {
        transform.position += transform.forward * 0.1f;
        s++;
        if (s > 200)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            s = 0;
        }
    }

    void OnAttack()
    {
        if (nearPlayer == false)
        {
            transform.LookAt(player.transform);
            transform.position += transform.forward * 0.1f;

        }
        else
        {
            transform.LookAt(player.transform);
            if (Time.time - lastShootTime < shootInterval) return;
            Vector3 pos = gameObject.transform.position + gameObject.transform.forward * 5;
            Instantiate(bullet, pos, transform.rotation);
            lastShootTime = Time.time;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = Vector3.zero;
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
            foreach (Transform child in transform)
            {
                GameObject destroyObj1 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj2 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj3 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.7f;
                audioSource.PlayOneShot(boomClip);
                
            }
            GameObject deadb = Instantiate(deadBody, transform.position, transform.rotation);
            deadb.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
