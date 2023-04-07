using System;
using System.Diagnostics.Tracing;
using Managers;
using UnityEngine;

namespace Bullets
{
    public class Grenade : MonoBehaviour
    {
        public void Disable()
        {
            PoolManager.Instance.Pool(transform.GetChild(0).gameObject, transform.position, transform.rotation);
            PoolManager.Instance.DePool(gameObject);
        }
    }
}