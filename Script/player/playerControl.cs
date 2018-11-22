using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    Animator animator;
    Rigidbody rigidbody;
    public GameObject gun;
    float speed = 20f;


    public GameObject shell;//炮弹
    public GameObject missile;//火箭弹
    public float lastShootTimeShell = 0;//上一次开炮时间
    private float shootIntervalShell = 1f;//炮弹时间间隔

    public GameObject deadbody;

    public GameObject destroyFireEffect;
    public GameObject destroySmockEffect;
    public GameObject destroySteamEffect;
    public AudioClip boomClip;

    public bool canDamage = true;
    int capture1Count = 0;
    int capture2Count = 0;
    public GameObject capture1;
    public GameObject capture2;
    bool canMisson = false;
    
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	void Update () {
        float steer = 100;
        float x = Input.GetAxis("Horizontal");
        transform.Rotate(0, x * steer * Time.deltaTime, 0);
        float y = Input.GetAxis("Vertical");
        Vector3 s = y * transform.forward * speed * Time.deltaTime;
        transform.position -= s;
        rigidbody.velocity = new Vector3(0, 0, 0);
        gun.transform.rotation = Quaternion.Euler(0,-90,0);



        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K))
        {
            if (Time.time - lastShootTimeShell < shootIntervalShell) return;
            Vector3 pos2 = transform.position - transform.forward * 5 + new Vector3(0, 1, 0)*5;
            if (canMisson)
            {
                GameObject shellcpy = Instantiate(missile, pos2 - transform.forward*2, gameObject.transform.rotation);//huojiandan
            }
            else
            {
                GameObject shellcpy = Instantiate(shell, pos2, gameObject.transform.rotation);
                shell shelljb = shellcpy.GetComponent<shell>();
                shelljb.speed = 20 + speed;
            }
                
            lastShootTimeShell = Time.time;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(canDamage)
        {
          if(collision.gameObject.tag == "tank" || collision.gameObject.tag == "enbullet")
            {
                GameObject destroyObj1 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj2 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj3 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj11 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj21 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj31 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj12 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj22 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj32 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj13 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj23 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj33 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.7f;
                audioSource.PlayOneShot(boomClip);
                GameObject deadb = Instantiate(deadbody, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else//不用毁坏时
        {
            if(collision.gameObject.tag == "capture1")
            {
                capture1Count++;
                canMisson = true;
            }
            if(collision.gameObject.tag == "capture2")
            {
                capture2Count++;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canDamage)
        {
            if (other.gameObject.tag == "tank" || other.gameObject.tag == "enbullet")
            {
                GameObject destroyObj1 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj2 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj3 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj11 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj21 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj31 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj12 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj22 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj32 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj13 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj23 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
                GameObject destroyObj33 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.7f;
                audioSource.PlayOneShot(boomClip);
                GameObject deadb = Instantiate(deadbody, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chufa2")
        {
            
            if (capture2Count > 0)
            {
                GameObject cap2 = Instantiate(capture2, transform.position - transform.forward + transform.right + transform.right + transform.right, transform.rotation);
                captureControl capC = cap2.GetComponentInChildren<captureControl>();
                capC.isBackHome = true;
                capture2Count--;
            }
            if(capture1Count > 0)
            {
                GameObject cap1 = Instantiate(capture1, transform.position - transform.forward + transform.right + transform.right + transform.right + transform.right, transform.rotation);
                captureControl capC = cap1.GetComponentInChildren<captureControl>();
                capC.isBackHome = true;
                capture1Count--;
            }

        }
    }
    
}
