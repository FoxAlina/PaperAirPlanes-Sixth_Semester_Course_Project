using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSensor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet" && gameObject.tag == "Enemy")
        {
            transform.parent.GetComponent<Enemy>().playerBulletHit();
        }
        else if (other.gameObject.tag == "EnemyBullet" && gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Player>().getMainDamage = true;
        }
        else if (other.tag == "MainPlayer" && gameObject.tag == "Enemy")
        {
            transform.parent.GetComponent<Enemy>().die(other);
        }
    }

}
