using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    private Player playerScript;

    public GameObject boomParticleSystem;

    void Awake()
    {
        boomParticleSystem.SetActive(false);

        playerScript = transform.parent.GetComponent<Player>();

        playerScript.isGameOver = false;
        Time.timeScale = 1.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();

            playerScript.isGameOver = true;
        }
        else if (other.tag == "Enemy")//"MainEnemy")
        {
            boomParticleSystem.transform.position = transform.position;
            boomParticleSystem.SetActive(true);
            boomParticleSystem.GetComponent<ParticleSystem>().Play();

            playerScript.getBigDamage = true;
        }
        else if (other.tag == "Ring")
        {
            playerScript.ring = true;

            playerScript.bulletIncrease = other.GetComponent<Ring>().bulletIncrease;
            playerScript.healthIncrease = other.GetComponent<Ring>().healthIncrease;

            other.GetComponent<Ring>().success();
            //other.gameObject.SetActive(false);
        }
    }
}
