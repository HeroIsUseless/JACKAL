using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houseControl : MonoBehaviour {

    public GameObject destroySmockEffect;
    public GameObject destroySteamEffect;
    public AudioClip boomClip;

    bool isDamage = false;

    public GameObject capture1;//红帽子的那种
    public GameObject capture2;//绿帽子的那种
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "effect" || collision.gameObject.tag == "bullet") && !isDamage)
        {
            isDamage = true;
            GameObject destroyObj1 = (GameObject)Instantiate(destroySmockEffect, gameObject.transform.position, gameObject.transform.rotation);
            GameObject destroyObj2 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
            AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0.7f;
            audioSource.PlayOneShot(boomClip);
            //现在是添加俘虏
            if(gameObject.tag == "house1")
            {
                GameObject cap1 = Instantiate(capture1, transform.position, transform.rotation);
                cap1.transform.Rotate(new Vector3(0, -90, 0));
            }
            if (gameObject.tag == "house2")
            {
                GameObject cap1 = Instantiate(capture2, transform.position, transform.rotation);
                cap1.transform.Rotate(new Vector3(0, -90, 0));
            }
        }
    }
}
