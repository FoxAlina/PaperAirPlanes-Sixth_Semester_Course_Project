using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    ObjectPool ringPool;

    public List<GameObject> positions;

    // Start is called before the first frame update
    void Start()
    {
        ringPool = this.transform.GetComponent<ObjectPool>();

        for (int i = 0; i < positions.Count; i++)
        {
            GameObject ring = ringPool.SharedInstance.GetPooledObject();
            if (ring != null)
            {
                Ring ringScript = ring.transform.GetComponent<Ring>();

                ring.transform.position = positions[i].transform.position;
                ring.transform.rotation = positions[i].transform.rotation;
                ring.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
