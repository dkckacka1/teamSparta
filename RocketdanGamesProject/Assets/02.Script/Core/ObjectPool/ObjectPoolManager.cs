using System.Collections.Generic;
using UnityEngine;

namespace RocketdanGamesProject.Core.ObjectPool
{
    // 오브젝트 풀 매니저 클래스
    public class ObjectPoolManager : SingletonMono<ObjectPoolManager>
    {
        private readonly Dictionary<string, Pool> _poolDic = new Dictionary<string, Pool>();
        private const int DefaultCreateCount = 10;

        public void CreatePool(string poolName, PoolObject originObject, Transform parent)
        {
            _poolDic.Add(poolName, new Pool(originObject, parent, poolName));

            var pool = _poolDic[poolName];

            for (int i = 0; i < DefaultCreateCount; ++i)
            {
                pool.CreateInstance();
            }
        }

        public T Get<T>(string poolName) where T : Object, IPoolable
        {
            var pool = _poolDic[poolName];

            return pool.Get<T>();
        }
        
        public void Release(string poolName, PoolObject poolObject)
        {
            var pool = _poolDic[poolName];
            
            pool.Release(poolObject);
        }
    }
}