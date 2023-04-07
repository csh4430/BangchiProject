using UnityEngine;
using UnityEngine.Serialization;

namespace SO.SkillIcon
{
    [CreateAssetMenu(fileName = "SkillIcon", menuName = "ScriptableObjects/SkillIcon")]
    public class SkillIconSO : ScriptableObject
    {
        public Sprite defaultIcon;
        public Sprite activeIcon;
    }
}