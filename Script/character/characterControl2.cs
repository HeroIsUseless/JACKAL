using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl2 : MonoBehaviour {
    public GameObject player;

    public bool meetPlayer = false;
    public bool nearPlayer = false;
    Animator animator;
    int s;

    public GameObject bullet;
    public float lastShootTime = 0;//上一次开枪时间
    private float shootInterval = 5f;//子弹时间间隔

    public GameObject deadBody;
    void Start()
    {
        //try {animator = GetComponentInChildren<Animator>(); } catch { }
        
    }

    void Update()
    {
        if (meetPlayer == true)
            OnAttack();
    }

    void OnAttack()
    {
        if (nearPlayer == false)
        {
            transform.LookAt(player.transform);
            //try {animator.SetFloat("forward", 0); } catch { }
        }
        else
        {
            transform.LookAt(player.transform);
            //try { animator.SetFloat("forward", 0); } catch { }

            if (Time.time - lastShootTime < shootInterval) return;
            Vector3 pos = gameObject.transform.position + gameObject.transform.forward * 2;
            Instantiate(bullet, pos, transform.rotation);
            lastShootTime = Time.time;

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "effect" || collision.gameObject.tag == "bullet")
        {
            Instantiate(deadBody, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


}
