using System;
using UnityEngine;

namespace Characters.Players
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<Vector3> OnMove;

        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                OnMove?.Invoke(Vector3.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                OnMove?.Invoke(Vector3.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                OnMove?.Invoke(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                OnMove?.Invoke(Vector3.right);
            }
            else
            {
                OnMove?.Invoke(Vector3.zero);   
            }
        }
    }
}