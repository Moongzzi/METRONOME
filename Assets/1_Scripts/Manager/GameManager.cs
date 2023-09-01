using MET.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Common
{
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// 게임 시작 시 필요한 요소 
        /// 저장 데이터 로드, 매니저 로드, 기타 등등
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
        }
    }
}