using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Util
{
    [Serializable]
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "APIServerSettings", menuName = "MET")]
    public class APIServerSettings : ScriptableObjectSingleton<APIServerSettings>
    {
        [SerializeField] private string rootURL;
        public string RootURL { get => rootURL; set => rootURL = value; }

        // API SERVER
        [HideInInspector]
        public const string API_URL = "";
    }
}