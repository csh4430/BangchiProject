using System;
using DG.Tweening;
using Resources;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private TMP_Text coinCountText;
        [SerializeField] private GameObject popUpTextPrf;
        [Space]
        [SerializeField] private Transform upgradePanelParent;
        [SerializeField] private GameObject upgradePanelPrf;
        [Space]
        [SerializeField] private GameObject soundPanelParent;
        [SerializeField] private GameObject soundPanelPrf;
        private int _coinCount = 0;
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            ResourceManager.Instance.OnCoinCountInit += InitCoinCount;
            ResourceManager.Instance.OnCoinCountAdded += UpdateCoinCount;
            InitUI(StatManager.Instance.statList);
            InitUI(SoundManager.Instance.soundList);
        }

        private void InitUI(StatList statList)
        {
            foreach (var stat in statList.stats)
            {
                var obj = Instantiate(upgradePanelPrf, upgradePanelParent);
                var upgradePanel = obj.GetComponent<UpgradePanel>();
                upgradePanel.SetData(stat);
            }
        }
        
        private void InitUI(SoundList soundList)
        {
            foreach (var sound in soundList.sounds)
            {
                var obj = Instantiate(soundPanelPrf, soundPanelParent.transform);
                var soundPanel = obj.GetComponent<SoundSlider>();
                soundPanel.SetData(sound);
            }
        }
        
        private void InitCoinCount(int value)
        {
            coinCountText.text = value.ToString();
        }
        
        private void UpdateCoinCount(int value)
        {
            coinCountText.DOCounter(_coinCount, value, 0.3f);
            _coinCount = value;
        }
        
        public void CreateDamageText(string text, Color color, Vector3 position)
        {
            var obj = PoolManager.Instance.Pool(popUpTextPrf, position, Quaternion.identity);
            var damageText = obj.GetComponent<PopUpText>();
            damageText.SetText(text, color);
        }
    }
}