using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int timeToDisable = 3;
    public float globalSpeed = 120f;
    private float speed;

    public GameObject boomParticleSystem;

    //public GameObject 

    void OnEnable()
    {
        speed = globalSpeed;

        boomParticleSystem.SetActive(false);
        StartCoroutine(lifeTime());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(timeToDisable);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "PlayerBullet")
        //{
        //    speed = 0;
        //    boomParticleSystem.transform.position = transform.position;
        //    boomParticleSystem.SetActive(true);
        //    boomParticleSystem.GetComponent<ParticleSystem>().Play();
        //    //StartCoroutine(waitAndDie());
        //    this.gameObject.SetActive(false);
            
        //}
        //else
        if (other.tag == "Ground")
        {
            speed = 0;
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();
            StartCoroutine(waitAndDie());
            //this.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy" && gameObject.tag == "PlayerBullet")
        {
            speed = 0;
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();
            StartCoroutine(waitAndDie());
            //this.gameObject.SetActive(false);
        }
        else if (other.tag == "Player" && gameObject.tag == "EnemyBullet")
        {
            speed = 0;
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();
            StartCoroutine(waitAndDie());
            //this.gameObject.SetActive(false);
        }
    }

    private void getTrigger()
    {
        
    }

    IEnumerator waitAndDie()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}
