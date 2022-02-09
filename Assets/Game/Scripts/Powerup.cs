using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private int powerupID; //0=tripleShot, 1=speedBoost, 3=shield

    [SerializeField]
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            try
            {
                Player p = other.GetComponent<Player>();
                //enable triple shot
                if (powerupID == 0)
                {
                    p.TriggerTripleShot();
                }
                if (powerupID == 1)
                {
                    p.TriggerSpeedBoost();
                }
                if (powerupID == 2)
                {
                    p.TriggerShield();
                }

                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);



            } catch (Exception e)
            {
                Debug.Log("could not get player object: " + e);
            }

            Destroy(this.gameObject);
        }

    }
}
