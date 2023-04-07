using System;
using Characters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace UI
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public float Radius;
        private RectTransform RectTransform => (RectTransform)transform;
        public RectTransform Stick;
        private Vector2 centerPos;

        public Character ControllingCharacter;

        private void Awake()
        {
            centerPos = RectTransform.anchoredPosition + (RectTransform.sizeDelta* 0.5f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = (Vector2)Input.mousePosition - centerPos;
            position = position.normalized * Radius; 
            Stick.anchoredPosition = position;
            ControllingCharacter.OnMove?.Invoke(position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Stick.anchoredPosition = Vector2.zero;
            ControllingCharacter.OnStop?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}