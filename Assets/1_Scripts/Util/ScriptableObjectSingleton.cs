using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Util
{
    public class ScriptableObjectSingleton<T> : ScriptableObject, ISingleton where T : ScriptableObject
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!Instantiated)
                {
                    Instantiate();
                }

                return _instance;
            }
        }
        public static bool Instantiated => _instance != null;

        public static T Instantiate()
        {
            T[] assets = Resources.LoadAll<T>(string.Empty);

            if (assets == null || assets.Length < 1)
            {
                throw new System.Exception("Could not found");
            }
            else if (assets.Length > 1)
            {
                Debug.LogWarning("Multiple instance");
            }

            _instance = assets[0];
            return _instance;
        }
    }
}