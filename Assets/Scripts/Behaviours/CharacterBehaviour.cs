using System;
using Characters;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(Character))]
    public class CharacterBehaviour : MonoBehaviour
    {
        protected Character ThisCharacter;

        protected virtual void Awake()
        {
            ThisCharacter = GetComponent<Character>();
        }
    }
}