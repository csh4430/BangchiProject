using System;
using Behaviours;
using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CoolDown : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private int skillIndex;
        private CharacterSkill _characterSkill;
        [SerializeField] private GameObject coolDownParent;
        private Image coolDownImage;
        [SerializeField] private TMP_Text coolDownText;

        private void Awake()
        {
            coolDownImage = coolDownParent.GetComponent<Image>();
        }

        private void Start()
        {
            _characterSkill = _character.Skills[skillIndex];
            _characterSkill.OnSkill.AddListener(ActiveCoolDown);
            _characterSkill.OnSkillInCoolDown += SetCoolDown;
            _characterSkill.ExitSkillInCoolDown += ExitCoolDown;
        }
        private void ActiveCoolDown()
        {
            coolDownParent.SetActive(true);
        }
        
        private void ExitCoolDown()
        {
            coolDownParent.SetActive(false);
        }
        private void SetCoolDown()
        {
            coolDownText.SetText(_characterSkill.Timer.ToString("F1"));
            coolDownImage.fillAmount = _characterSkill.Timer / _characterSkill.skillCoolTime;
        }
    }
}