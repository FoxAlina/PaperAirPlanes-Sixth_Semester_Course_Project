using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public GameObject player;
    Vector3 targetVector;
    public float height;

    void Start()
    {

    }

    void Update()
    {
        targetVector = player.transform.position;
        targetVector.y = height;
        transform.position = targetVector;
    }
}
