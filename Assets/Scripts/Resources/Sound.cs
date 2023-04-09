using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "ScriptableObjects/Sound")]
    public class Sound : ScriptableObject
    {
        public string type;
        [Range(0.001f, 1)]
        public float value = 0.5f;

        public float Volume => Mathf.Log(value) * 20f;
    }
}