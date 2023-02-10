using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    private float speed = 500.0f;
    private float rotateSpeed = 200.0f;

    public Vector3 target_vector;

    private Quaternion startRotation;
    private Vector3 startRot;

    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startRot = startRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        //transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime * Input.GetAxis("Vertical")); // Обычный поворот
        //transform.Rotate(0f, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0f);

        //if (Input.GetKey(KeyCode.UpArrow)) // Обычный поворот
        //{
        //    transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        //    //target_vector += new Vector3(0f, 0f, -rotateSpeed * Time.deltaTime);
        //    //flag = true;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        //    //target_vector += new Vector3(0f, 0f, rotateSpeed * Time.deltaTime);
        //    //flag = true;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        //    //target_vector += new Vector3(0f, rotateSpeed * Time.deltaTime, 0f);
        //    //flag = true;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0f, -rotateSpeed * Time.deltaTime, 0f);
        //    //target_vector += new Vector3(0f, -rotateSpeed * Time.deltaTime, 0f);
        //    //flag = true;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(-rotateSpeed * Time.deltaTime, 0f, 0f);
        //    //target_vector += new Vector3(-rotateSpeed * Time.deltaTime, 0f, 0f);
        //    //flag = true;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
        //    //target_vector += new Vector3(rotateSpeed * Time.deltaTime, 0f, 0f);
        //    //flag = true;
        //}

        //if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        //{
        //    flag = false;
        //}

        //if (flag)
        //{
              target_vector /= 500; // для джойстика
              transform.Rotate(0f, target_vector.x * rotateSpeed * Time.deltaTime, -target_vector.y * rotateSpeed * Time.deltaTime);


        //    //target_vector /= 5; //  для клавиш
        //    //transform.Rotate(target_vector);
        //}



        //else
        //{

            Quaternion currentRotation = transform.rotation; // можно добавить в повороты клавишами, тогда будут блокироваться повороты при двух клавишах
            Vector3 curRot = currentRotation.eulerAngles;

            Vector3 delta_rotation = new Vector3(startRot.x - curRot.x, 0f, 0f);
            transform.Rotate(delta_rotation);
        //}

        //transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * speed * Time.deltaTime, Space.World); // Наклон и возврат
        //transform.rotation = Quaternion.AngleAxis(20 * Input.GetAxis("Horizontal"), transform.right);
        //transform.rotation *= Quaternion.AngleAxis(-20 * Input.GetAxis("Vertical"), transform.forward);

    }

}
