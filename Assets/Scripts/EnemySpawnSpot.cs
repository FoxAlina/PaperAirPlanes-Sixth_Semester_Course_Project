using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawnSpot : MonoBehaviour
{
    public int numberOfEnemies;
    List<GameObject> enemies;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public int amountOfEnemySet;

    public PathCreator pathCreatorScript;
    public GameObject player;

    [SerializeField] private GameObject prefab;
    public float enemySpeed;
    public float enemyRotationSpeed;

    int indexOfEnemy = 0;

    void Start()
    {
        enemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            tmp = Instantiate(prefab) as GameObject;
            tmp.SetActive(false);
            tmp.GetComponent<Enemy>().pathCreator = pathCreatorScript;
            tmp.GetComponent<Enemy>().player = player;
            tmp.GetComponent<Enemy>().speed = enemySpeed;
            tmp.GetComponent<Enemy>().rotationSpeed = enemyRotationSpeed;
            tmp.GetComponent<Enemy>().posOnPath = this.transform.position;
            enemies.Add(tmp);
        }
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < enemies[0].GetComponent<Enemy>().DistanceToPlayer)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                indexOfEnemy = 0;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (!enemies[i].activeInHierarchy)
                    {
                        indexOfEnemy++;

                        enemies[i].transform.position = this.transform.position;
                        if (indexOfEnemy % 2 == 0)
                        {
                            enemies[i].transform.rotation = new Quaternion();
                            enemies[i].transform.Rotate(0f, 90f, 0f);
                        }
                        if (indexOfEnemy % 3 == 0)
                        {
                            enemies[i].transform.rotation = new Quaternion();
                            enemies[i].transform.Rotate(0f, 180f, 0f);
                        }
                        if (indexOfEnemy % 4 == 0)
                        {
                            enemies[i].transform.rotation = new Quaternion();
                            enemies[i].transform.Rotate(0f, 270f, 0f);
                        }
                        enemies[i].GetComponent<Enemy>().distanceTravelled = 0f;
                        enemies[i].SetActive(true);
                        if(activeEnemiesInScene() == amountOfEnemySet) break;
                    }
                }
            }
        }
    }

    int activeEnemiesInScene()
    {
        int n = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].activeInHierarchy)
            {
                n++;
            }
        }
        return n;
    }
}
