using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public JoysticController joysticScript;
    public SteeringController joysticScript;

    public float normalSpeed = 50.0f;
    public float maxSpeed = 100.0f;
    float currentSpeed = 0f;
    public float rotateSpeed = 60.0f;

    Vector3 target_vector;

    private Quaternion startRotation;
    private Vector3 startRot;

    public GameObject model;

    Vector3 prevPos;
    Vector3 curPos;

    [HideInInspector]
    public bool isGameOver;
    [HideInInspector]
    public bool getMainDamage;
    [HideInInspector]
    public bool getBigDamage;
    [HideInInspector]
    public int bigDamage;

    public Button fireButton;
    ObjectPool bulletPool;

    public Button speedUPButton;
    bool ifSpeedUpButtonDown;

    public GameObject speedParticleSystem;

    public int maxAmountBullets;
    [HideInInspector]
    public int bullets;

    [HideInInspector]
    public int hits;
    [HideInInspector]
    public int enemies;

    [HideInInspector]
    public bool ring;
    [HideInInspector]
    public int bulletIncrease;
    [HideInInspector]
    public int healthIncrease;

    void Start()
    {
        ifSpeedUpButtonDown = false;

        prevPos = transform.position;
        curPos = prevPos;

        startRotation = transform.rotation;
        startRot = startRotation.eulerAngles;

        Button btn = fireButton.GetComponent<Button>();
        btn.onClick.AddListener(fire);

        bulletPool = this.transform.GetComponent<ObjectPool>();

        speedParticleSystem.SetActive(false);

        bullets = maxAmountBullets;

        ring = false;
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (!ifSpeedUpButtonDown) currentSpeed = normalSpeed;

            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime); // скорость

            Quaternion currentRotation = transform.rotation;
            Vector3 curRot = currentRotation.eulerAngles;

            target_vector = joysticScript.getTargetVector(); // управление через джойстик
            target_vector /= 500;

            if (curRot.x > 85 && curRot.x < 275) // запрет мертвых петель - поворота по x
            {
                target_vector.y = 0f;
            }

            transform.Rotate(-target_vector.y * rotateSpeed * Time.deltaTime, target_vector.x * rotateSpeed * Time.deltaTime, 0f);

            Vector3 delta_rotation = new Vector3(0f, 0f, startRot.z - curRot.z); // возвращение - не даёт поворота по z
            transform.Rotate(delta_rotation);

            model.transform.rotation = new Quaternion(); // поворот модельки
            model.transform.rotation = transform.rotation;
            model.transform.rotation *= Quaternion.Euler(-20 * target_vector.y, 20 * target_vector.x, -35 * target_vector.x);


            //if (Input.GetButtonDown("Fire1"))
            if (Input.GetKeyDown(KeyCode.LeftControl))
                fire();


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //speedUp();
                speedUPButton.GetComponent<LongClickButton>().pointerDown = true;
                speedUPButton.GetComponent<LongClickButton>().sound = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
                //notSpeedUp();
                speedUPButton.GetComponent<LongClickButton>().Reset();
        }
    }

    void FixedUpdate()
    {
        prevPos = curPos;
        curPos = transform.position;
    }

    public float getMeters()
    {
        //return currentSpeed * Time.deltaTime;
        return Vector3.Distance(curPos, prevPos);
    }

    void fire()
    {
        if (bullets > 0 && !isGameOver)
        {
            GameObject bullet = bulletPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = model.transform.position + model.transform.forward;
                bullet.transform.rotation = model.transform.rotation;
                bullet.SetActive(true);
                bullets--;
            }
        }
    }

    public void speedUp()
    {
        ifSpeedUpButtonDown = true;
        currentSpeed = maxSpeed;
        speedParticleSystem.SetActive(true);
    }

    public void notSpeedUp()
    {
        ifSpeedUpButtonDown = false;
        speedParticleSystem.SetActive(false);
    }
}
