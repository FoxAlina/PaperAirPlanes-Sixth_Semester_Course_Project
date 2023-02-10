using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MainMenuFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    [HideInInspector]
    public float distanceTravelled;

    void Start()
    {
        
    }

    void Update()
    {
        distanceTravelled += 2f * speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
