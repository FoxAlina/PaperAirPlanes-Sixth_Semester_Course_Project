using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    [HideInInspector]
    public float distanceTravelled;

    public GameObject player;

    bool ifIFollowThePath;

    Vector3 posOnPath;

    public Slider healthBar;
    public int maxHealth = 5;
    int health;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    ObjectPool bulletPool;

    public float DistanceToPlayer;

    [SerializeField] private GameObject prefab;
    GameObject boomParticleSystem;

    void Awake()
    {
        healthBar.maxValue = maxHealth;
        bulletPool = this.transform.GetComponent<ObjectPool>();
    }

    void OnEnable()
    {
        boomParticleSystem = Instantiate(prefab) as GameObject;
        boomParticleSystem.SetActive(false);

        health = maxHealth;
        ifIFollowThePath = true;
        healthBar.value = health;
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
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime / 50f);
            transform.Translate(Vector3.forward * 2f * speed * Time.deltaTime);

            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (angle < 20f)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    shoot();
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

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "PlayerBullet")
        //{
        //    boomParticleSystem.transform.position = transform.position;
        //    boomParticleSystem.SetActive(true);

        //    damage();
        //    other.gameObject.SetActive(false);

        //    player.GetComponent<Player>().hits++;
        //}
        //else
        if(other.tag == "Ground")
        {
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);

            this.gameObject.SetActive(false);
        }
        else if (other.tag == "Player")
        {
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);

            player.GetComponent<Player>().enemies++;

            this.gameObject.SetActive(false);
        }
    }

    public void playerBulletHit(Collider other)
    {
        boomParticleSystem.transform.position = transform.position;
        boomParticleSystem.SetActive(true);

        damage();
        other.gameObject.SetActive(false);

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
        }

    }
}
