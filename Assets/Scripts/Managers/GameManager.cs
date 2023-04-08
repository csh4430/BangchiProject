using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public List<Character> characters = new List<Character>();
        public List<Character> enemies => characters.FindAll((c)=> c is Enemy).ToList();
        
        private void Awake()
        {
            Instance = this;
        }
    }
}