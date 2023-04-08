using System;
using Resources;
using UnityEngine;

namespace Managers
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField] public StatList statList;

        public static StatManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        
        
    }
}