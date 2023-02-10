using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysticController : MonoBehaviour
{
    public GameObject touch_marker;
    Vector3 target_vector;

    public bool touch;

    void Start()
    {
        touch = false;
        touch_marker.transform.position = transform.position;
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 touch_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //    target_vector = touch_pos - transform.position;
        //    if (target_vector.magnitude < 250)
        //    {
        //        touch_marker.transform.position = touch_pos;
        //    }
        //    else
        //    {
        //        target_vector = new Vector3(0, 0, 0);
        //        touch_marker.transform.position = transform.position;
        //    }
        //}
        //else
        //{
        //    touch_marker.transform.position = transform.position;
        //    target_vector = new Vector3(0, 0, 0);
        //}



        //if (Input.GetMouseButton(0))
        //{
        //    if (!touch)
        //    {

        //        touch = true;
        //        Vector3 touch_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //        target_vector = touch_pos - transform.position;
        //        if (target_vector.magnitude < 250)
        //        {
        //            touch_marker.transform.position = touch_pos;
        //        }
        //        else
        //        {
        //            touch = false;
        //            target_vector = new Vector3(0, 0, 0);
        //            touch_marker.transform.position = transform.position;
        //        }
        //    }
        //    else
        //    {
        //        Vector3 touch_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //        target_vector = touch_pos - transform.position;
        //        if (target_vector.magnitude < 250)
        //        {
        //            touch_marker.transform.position = touch_pos;
        //        }
        //        else
        //        {
        //            touch_marker.transform.position = transform.position;
        //        }
        //    }

        //}
        //else
        //{
        //    touch = false;
        //    touch_marker.transform.position = transform.position;
        //    target_vector = new Vector3(0, 0, 0);
        //}
    }

    public Vector3 getTargetVector()
    {
        return this.target_vector;
    }
}
