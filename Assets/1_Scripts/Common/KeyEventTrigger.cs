using MET.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Common
{
    public class KeyEventTrigger : Singleton<KeyEventTrigger>
    {
        /// <summary>
        /// KeyEventTrigger.Instance.keyInputAction -= / += 으로 이벤트 호출
        /// </summary>
        public Action keyInputAction = null;


        private void Update()
        {
            OnUpdate();
        }

        public void OnUpdate()
        {
            // 입력 된 키가 있는지 체크
            if (Input.anyKey == false)
                return;

            if (keyInputAction != null)
                keyInputAction.Invoke();
        }
    }
}