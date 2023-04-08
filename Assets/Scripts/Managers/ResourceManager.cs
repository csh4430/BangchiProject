using System;
using Resources;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance;
        private int _coinCount = 0;
        public event Action<int> OnCoinCountInit;
        public event Action<int> OnCoinCountAdded;
        private Resource _resource;
        public Resource Resource => _resource;
        private void Awake()
        {
            Instance = this;
            _resource = JsonManager.LoadJsonFile<Resource>(Application.dataPath + "/Save", "Resource.json");
            _coinCount = _resource.gold;
        }

        private void Start()
        {
            SetCoin(_coinCount);
        }

        public void AddCoin(int value)
        {
            _coinCount += value;
            _resource.gold = _coinCount;
            var json = JsonManager.ObjectToJson(_resource);
            JsonManager.SaveJsonFile(Application.dataPath + "/Save", "Resource.json", json);
            OnCoinCountAdded?.Invoke(_coinCount);
        }
        
        public void SetCoin(int value)
        {
            _coinCount = value;
            _resource.gold = _coinCount;
            var json = JsonManager.ObjectToJson(_resource);
            JsonManager.SaveJsonFile(Application.dataPath + "/Save", "Resource.json", json);
            OnCoinCountInit?.Invoke(_coinCount);
        }
    }
}