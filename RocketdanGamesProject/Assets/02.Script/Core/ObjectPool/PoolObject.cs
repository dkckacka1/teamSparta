using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketdanGamesProject.Core.ObjectPool
{
    // 풀링 오브젝트
    public class PoolObject : MonoBehaviour
    {
        private readonly List<IPoolable> _poolableList = new List<IPoolable>();

        [HideInInspector] public string poolName;

        private void Awake()
        {
            _poolableList.AddRange(GetComponents<IPoolable>());
        }

        public void OnCreate()
        {
            foreach (var poolable in _poolableList)
            {
                poolable.OnCreate();
            }
        }

        public void OnGet()
        {
            foreach (var poolable in _poolableList)
            {
                poolable.OnGet();
            }
        }

        public void OnRelease()
        {
            foreach(var poolable in _poolableList)
            {
                poolable.OnRelease();
            }
        }
    }
}