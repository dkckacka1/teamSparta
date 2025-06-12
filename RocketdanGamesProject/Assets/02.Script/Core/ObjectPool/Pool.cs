using System.Collections.Generic;
using UnityEngine;

namespace RocketdanGamesProject.Core.ObjectPool
{
    // 오브젝트 풀 클래스
    public sealed class Pool
    {
        private readonly PoolObject _originObject;
        private readonly Queue<PoolObject> _poolQueue = new();
        private Transform _parent;
        private string _poolName;

        public Pool(PoolObject originObject, Transform parent, string poolName)
        {
            _originObject = originObject;
            _parent = parent;
            _poolName = poolName;
        }

        public T Get<T>() where T : Object, IPoolable
        {
            if (_poolQueue.Count <= 0)
            {
                CreateInstance();
            }

            var poolObject = _poolQueue.Dequeue();
            poolObject.OnGet();
            poolObject.gameObject.SetActive(true);
            return poolObject.GetComponent<T>();
        }

        public void CreateInstance()
        {
            var instanceObject = Object.Instantiate(_originObject, _parent);
            instanceObject.poolName = _poolName;
            instanceObject.OnCreate();
            instanceObject.gameObject.SetActive(false);
            _poolQueue.Enqueue(instanceObject);
        }

        public void Release(PoolObject poolObject)
        {
            poolObject.OnRelease();
            poolObject.gameObject.SetActive(false);
            _poolQueue.Enqueue(poolObject);
        }
    }
}