using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//首先往前走几步，然后开始循环
public class captureControl : MonoBehaviour {
    int jibuqi = 0;
    public bool nearPlayer = false;
    Animator animator;
    public GameObject player;
    public bool isBackHome = false;
    public GameObject heli;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isBackHome == false)
        {
		    if(jibuqi < 50)
            {
                animator.SetFloat("forward", 2);
                transform.position += transform.forward * 0.1f;
                jibuqi++;
            }
            else
            {
                if(nearPlayer == false)//不在旁边，停住，敬礼
                {
                    transform.LookAt(player.transform);
                    animator.SetFloat("forward", 0);
                }
                else//在旁边，走过去就可以了
                {
                    transform.LookAt(player.transform);
                    animator.SetFloat("forward", 2);
                    transform.position += transform.forward * 0.1f;
                }
            }
        }
        else
        {
            transform.LookAt(heli.transform);
            animator.SetFloat("forward", 2);
            transform.position += transform.forward * 0.1f;
        }

	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
        if (collision.gameObject.tag == "heli")
            Destroy(gameObject);
    }
}
