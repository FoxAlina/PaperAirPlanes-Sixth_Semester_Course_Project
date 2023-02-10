using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            transform.parent.GetComponent<Enemy>().die(other);
        }
        else if (other.tag == "MainPlayer")
        {
            transform.parent.GetComponent<Enemy>().die(other);
        }
    }
}
