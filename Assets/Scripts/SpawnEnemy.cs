using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnEnemy : MonoBehaviour
{
    public int numberOfEnemies;
    List<GameObject> enemies;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public PathCreator pathCreatorScript;
    public GameObject player;

    [SerializeField] private GameObject prefab;

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
            enemies.Add(tmp);
        }
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    enemies[i].transform.position = new Vector3(0, 0, 0);
                    enemies[i].GetComponent<Enemy>().distanceTravelled = 0f;
                    enemies[i].SetActive(true);
                    break;
                }
            }

        }

    }
}
