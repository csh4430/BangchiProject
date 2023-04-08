using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;
        
        private Dictionary<string, List<GameObject>> poolPrefabs;
        [SerializeField] private List<GameObject> firstPoolPrefabs;

        private void Awake()
        {
            Instance = this;
            foreach (var prefab in firstPoolPrefabs)
            {
                CreatePool(prefab, 10);
            }
        }
        
        public void CreatePool(GameObject prefab, int count)
        {
            if (poolPrefabs == null)
                poolPrefabs = new Dictionary<string, List<GameObject>>();
            if (poolPrefabs.ContainsKey(prefab.name))
                return;
            poolPrefabs.Add(prefab.name, new List<GameObject>());
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.name = prefab.name;
                obj.SetActive(false);
                poolPrefabs[prefab.name].Add(obj);
            }
        }
        
        public GameObject Pool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (poolPrefabs == null)
                poolPrefabs = new Dictionary<string, List<GameObject>>();
            if (poolPrefabs.ContainsKey(prefab.name) == false)
                CreatePool(prefab, 10);
            var obj = poolPrefabs[prefab.name].Find(o => o.activeSelf == false);
            if (obj == null)
            {
                obj = Instantiate(prefab, transform);
            }
            else
            {
                poolPrefabs[prefab.name].Remove(obj);
            }

            obj.name = prefab.name;
            obj.transform.SetParent(null);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        public void DePool(GameObject prefab)
        {
            if (poolPrefabs == null)
                poolPrefabs = new Dictionary<string, List<GameObject>>();
            if (poolPrefabs.ContainsKey(prefab.name) == false)
                CreatePool(prefab, 10);
            poolPrefabs[prefab.name].Add(prefab);
            prefab.transform.SetParent(transform);
            prefab.SetActive(false);
        }
    }
}