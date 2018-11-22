using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//huojaindan
public class shell2 : MonoBehaviour {
    public float speed = 100f;
    public float maxLiftTime = 2f;
    public float instantiateTime = 0f;

    public GameObject destroyFireEffect;
    public GameObject destroySteamEffect;
    public AudioClip boomClip;

    Rigidbody rigidbody;
    int jishu = 0;
    // Use this for initialization
    void Start()
    {
        instantiateTime = Time.time;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        transform.Rotate(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "airwall")
        {
            GameObject destroyObj1 = (GameObject)Instantiate(destroyFireEffect, gameObject.transform.position, gameObject.transform.rotation);
            GameObject destroyObj2 = (GameObject)Instantiate(destroySteamEffect, gameObject.transform.position, gameObject.transform.rotation);
            AudioSource audioSource = destroyObj2.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0.7f;
            audioSource.PlayOneShot(boomClip);
            Destroy(gameObject);
        }

    }
}
