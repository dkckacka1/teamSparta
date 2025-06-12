using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketdanGamesProject.Core
{
    // 싱글턴 모노비헤이비어 클래스
    public class SingletonMono<T> : MonoBehaviour where T: Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    var obj = new GameObject
                    {
                        name = nameof(T)
                    };
                    var ins = obj.AddComponent<T>();
                    _instance = ins; 
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance is not null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
            }

            Initialize();
        }

        protected virtual void Initialize()
        {
            
        }
    }
}