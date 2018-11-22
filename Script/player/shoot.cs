using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    public GameObject bullet;
    public float lastShootTime = 0;//上一次开枪时间
    private float shootInterval = 0.1f;//子弹时间间隔
                                       // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
        {
            if (Time.time - lastShootTime < shootInterval) return;
            Vector3 pos = gameObject.transform.position + Vector3.right * 4 - gameObject.transform.forward * 2;
            GameObject bu = Instantiate(bullet, pos, Quaternion.Euler(0,-90,0));
            Bullet bucp = bu.GetComponent<Bullet>();
            bucp.canDestroy = true;
            lastShootTime = Time.time;
        }

    }
}
