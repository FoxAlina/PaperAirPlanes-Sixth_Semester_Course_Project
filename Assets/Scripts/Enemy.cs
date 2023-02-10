using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class Enemy : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    public float rotationSpeed;
    [HideInInspector]
    public float distanceTravelled;

    public GameObject player;

    bool ifIFollowThePath;

    [HideInInspector]
    public Vector3 posOnPath;

    public Slider healthBar;
    public int maxHealth = 5;
    int health;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    ObjectPool bulletPool;

    public float DistanceToPlayer;

    public AudioSource bulletSound;

    public GameObject boomParticleSystem;
    public AudioSource boomSound;

    bool justEnabled;

    void Awake()
    {
        healthBar.maxValue = maxHealth;
        bulletPool = this.transform.GetComponent<ObjectPool>();
    }

    void OnEnable()
    {
        boomParticleSystem.SetActive(false);

        health = maxHealth;
        ifIFollowThePath = true;
        healthBar.value = health;

        justEnabled = true;
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < DistanceToPlayer)
        {
            if (ifIFollowThePath)
            {
                ifIFollowThePath = false;
                posOnPath = transform.position;
            }

            Vector3 dir = player.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir /* * Random.Range(-1.0f, 1.0f)*/);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * 2f * speed * Time.deltaTime);

            if(Vector3.Distance(this.transform.position, player.transform.position) < DistanceToPlayer / 2.0f)
            {
                Vector3 targetDir = player.transform.position - transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);
                if (angle < 10f)
                {
                    if (Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        shoot();
                    }
                }
            }
        }
        else
        {
            if (!ifIFollowThePath)
            {
                Vector3 dir = posOnPath - this.transform.position;
                Quaternion rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
                transform.Translate(Vector3.forward * 2f * speed * Time.deltaTime);
                if (Vector3.Distance(posOnPath, transform.position) <= 10)
                {
                    ifIFollowThePath = true;
                }
            }
            else
            {
                distanceTravelled += 2f * speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
        }

    }

    public void die(Collider other)
    {
        if (justEnabled && other.tag == "Ground") justEnabled = false;
        else
        {
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();

            if (other.tag == "MainPlayer")
            {
                player.GetComponent<Player>().enemies++;
                player.GetComponent<Player>().getBigDamage = true;
                player.GetComponent<Player>().bigDamage = health;
            }

            StartCoroutine(waitAndDie());
        }
    }

    IEnumerator waitAndDie()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }

    public void playerBulletHit()
    {
        boomParticleSystem.transform.position = transform.position;
        boomParticleSystem.SetActive(true);
        boomParticleSystem.GetComponent<ParticleSystem>().Play();

        boomSound.Play();

        damage();

        player.GetComponent<Player>().hits++;
    }

    public void damage()
    {
        health--;
        if (health >= 0)
        {
            healthBar.value = health;
        }
        if (health == 0)
        {
            player.GetComponent<Player>().enemies++;

            this.gameObject.SetActive(false);
        }
    }

    void shoot()
    {
        GameObject bullet = bulletPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);

            bulletSound.Play();
        }

    }
}
