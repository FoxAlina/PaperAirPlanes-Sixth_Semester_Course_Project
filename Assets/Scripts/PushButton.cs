using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// Пока кнопка нажата
public class PushButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent ifButtonDown;
    public UnityEvent ifButtonUp;

    private bool isDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
    }

    void Update()
    {
        if (!this.isDown)
        {
            if (ifButtonUp != null)
                ifButtonUp.Invoke();
        }
        else
        {
            if (ifButtonDown != null)
                ifButtonDown.Invoke();
        }

    }
}
