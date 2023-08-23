using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Util
{
    public class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
    {
        private static T _instance = null;

        public static bool Instantiated
        {
            get
            {
                return _instance != null;
            }
        }
        public static T Instance
        {
            get
            {
                if (_instance == null && (_instance = GameObject.FindObjectOfType<T>()) == null)
                {
                    _instance = Instantiate(false);
                }
                return _instance;
            }
        }


        /// <summary>
        /// 싱글톤 객체를 강제로 생성한다.
        /// </summary>
        public static T Instantiate(bool _setDontDestroyOnLoad = false)
        {
            GameObject insObj = new GameObject(typeof(T).Name);
            if (_setDontDestroyOnLoad)
                GameObject.DontDestroyOnLoad(insObj);

            _instance = insObj.AddComponent<T>();
            return _instance;

        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(_instance.gameObject);
            }
            DontDestroyOnLoad(this);
        }
    }
}