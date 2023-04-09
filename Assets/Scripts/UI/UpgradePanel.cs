using System;
using Managers;
using Resources;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        public Image icon;
        public TMP_Text statName;
        public TMP_Text statInformation;
        public Button upgradeButton;
        public TMP_Text upgradeCost;
        
        private StatSO _stat;

        private void Start()
        {
            upgradeButton.onClick.AddListener(Upgrade);
            upgradeButton.onClick.AddListener(AudioManager.Instance.ButtonSound);
        }

        public void SetData(StatSO stat)
        {
            _stat = stat;
            this.icon.sprite = stat.icon;
            this.statName.text = $"{stat.statName} : Lv.{stat.level}";
            statInformation.text = $"{stat.multiplier * 100}% -> {(stat.multiplier + 0.01f) * 100}%";
            upgradeCost.text = LevelToCost(stat.level).ToString();
        }

        private void Upgrade()
        {
            if (ResourceManager.Instance.Resource.gold < LevelToCost(_stat.level)) return;
            ResourceManager.Instance.Resource.gold -= LevelToCost(_stat.level);
            ResourceManager.Instance.SetCoin(ResourceManager.Instance.Resource.gold);
            _stat.level++;
            SetData(_stat);
        }

        public int LevelToCost(int level)
        {
            var defaultCost = 200;
            var cost = 0;
            for(var i = 1; i <= level; i++)
            {
                cost += defaultCost;
                defaultCost += 30;
            }

            return cost;
        }
    }
}