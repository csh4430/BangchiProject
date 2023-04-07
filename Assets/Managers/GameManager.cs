using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public List<Character> characters = new List<Character>();
        
        private void Awake()
        {
            Instance = this;
        }
    }
}