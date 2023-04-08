using System;
using UnityEngine;

namespace Resources
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Stat", menuName = "ScriptableObjects/Stat")]
    public class StatSO : ScriptableObject
    {
        public Sprite icon;
        public string statName;
        public int level = 1;
        public float multiplier => 1 + (level - 1) * 0.01f;
    }
}