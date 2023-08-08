using MET.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MET.Common
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        private string[] sceneNames = new string[]
        {
            "1_TitleScene",
            "2_FieldScene",
            "3_BattleScene"
        };

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        public void LoadScene(int sceneNum)
        {
            SceneManager.LoadScene(sceneNames[sceneNum], LoadSceneMode.Single);
        }
    }
}