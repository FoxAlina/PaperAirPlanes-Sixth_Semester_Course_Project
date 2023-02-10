using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController : MonoBehaviour
{
    public GameObject touch_marker;
    Vector3 target_vector;

    [HideInInspector]
    public bool touch;

    Vector3 start_pos;

    void Start()
    {
        touch = false;
        touch_marker.transform.position = transform.position;
        touch_marker.SetActive(false);
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (!touch)
        //    {

        //        touch = true;
        //        Vector3 touch_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //        touch_marker.transform.position = touch_pos;
        //        touch_marker.SetActive(true);

        //        start_pos = touch_pos;
        //    }
        //    else
        //    {
        //        Vector3 touch_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //        target_vector = touch_pos - start_pos;//touch_marker.transform.position;
        //        touch_marker.transform.position = touch_pos;
        //    }

        //}
        //else
        //{
        //    touch = false;
        //    touch_marker.SetActive(false);
        //    target_vector = new Vector3(0, 0, 0);
        //}

        if (Input.touchCount > 0)
        {
            Touch touchControl = Input.GetTouch(0);

            if (touchControl.phase == TouchPhase.Moved)
            {
                if (!touch)
                {

                    touch = true;
                    Vector3 touch_pos = new Vector3(touchControl.position.x, touchControl.position.y, 0.0f);
                    touch_marker.transform.position = touch_pos;
                    touch_marker.SetActive(true);

                    start_pos = touch_pos;
                }
                else
                {
                    Vector3 touch_pos = new Vector3(touchControl.position.x, touchControl.position.y, 0.0f);
                    target_vector = touch_pos - start_pos;
                    touch_marker.transform.position = touch_pos;
                }
            }
        }
        else
        {
            touch = false;
            touch_marker.SetActive(false);
            target_vector = new Vector3(0, 0, 0);
        }

    }

    public Vector3 getTargetVector()
    {
        return this.target_vector;
    }
}
