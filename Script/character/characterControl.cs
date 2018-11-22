using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour {
    public GameObject player;

    public bool meetPlayer = false;
    public bool nearPlayer = false;
    Animator animator;
    int s;

    public GameObject bullet;
    public float lastShootTime = 0;//上一次开枪时间
    private float shootInterval = 5f;//子弹时间间隔

    public GameObject deadBody;
    void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        if (meetPlayer == false)
            OnMove();
        else
            OnAttack();
	}

    void OnMove()
    {
        animator.SetFloat("forward", 2f);
        transform.position += transform.forward * 0.1f;
        s++;
        if(s > 200)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            s = 0;
        }
    }

    void OnAttack()
    {
        if(nearPlayer == false)//怎么走过去？
        {
            transform.LookAt(player.transform);
            animator.SetFloat("forward", 2f);
            transform.position += transform.forward * 0.1f;

        }
        else
        {
            transform.LookAt(player.transform);
            animator.SetFloat("forward", 0);

            if (Time.time - lastShootTime < shootInterval) return;
            Vector3 pos = gameObject.transform.position + gameObject.transform.forward * 2;
            Instantiate(bullet, pos, transform.rotation);
            lastShootTime = Time.time;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "effect" || collision.gameObject.tag == "bullet")
        {
            Instantiate(deadBody, transform.position, transform.rotation);
            Destroy(gameObject);
        }
   
    }

}
