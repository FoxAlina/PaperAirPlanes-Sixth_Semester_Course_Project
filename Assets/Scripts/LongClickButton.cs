using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

// долгое нажатие за время
public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool pointerDown;
    private float pointerDownTime;

    public float requiredHoldTime;

    public UnityEvent onLongClick;
    public UnityEvent ifButtonUp;

    [SerializeField]
    private Image fillImage;

    public AudioSource speedUpSound;
    public bool sound;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        sound = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        sound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointerDown)
        {
            if (sound)
            {
                sound = false;
                speedUpSound.Play();
            }
            pointerDownTime += Time.deltaTime;
            if (onLongClick != null)
                onLongClick.Invoke();
            if (pointerDownTime > requiredHoldTime)
            {
                Reset();
            }
            fillImage.fillAmount = pointerDownTime / requiredHoldTime;
        }

        if (Time.timeScale == 0) Reset();
    }

    public void Reset()
    {
        pointerDown = false;
        pointerDownTime = 0;
        fillImage.fillAmount = pointerDownTime / requiredHoldTime;

        if (ifButtonUp != null)
            ifButtonUp.Invoke();

        speedUpSound.Stop();
    }
}
