using MET.Common;
using MET.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace MET.Account
{
    public class AccountManager : Singleton<AccountManager>
    {
        public User UserData => userData;
        private User userData = new();


        /// <summary>
        /// 회원가입 시 호출하는 함수
        /// </summary>
        public void TryRegister(string id, string pw, string nickName, Action<APIResult, string> callback)
        {
            JSONObject bodyData = new JSONObject();
            bodyData.AddField("loginId", id);
            bodyData.AddField("password", pw);
            bodyData.AddField("name", nickName);

            APIManager.Instance.CallRESTAPI("/user/registration", RESTAPI_TYPE.POST, 
                (result, ackData) =>
                {
                    if(result == APIResult.SUCCESS)
                    {
                        callback(result, ackData.GetField("errorMsg")?.str);
                    }
                    else
                    {
                        callback(result, ackData.GetField("errorMsg")?.str);
                    }
                },
                bodyData);
        }

        /// <summary>
        /// 로그인 시 호출하는 함수
        /// </summary>
        public void TryLogin(string id, string pw, Action<APIResult, string> callback)
        {
            //Test
            /*callback(APIResult.SUCCESS, "");
            return;*/


            JSONObject bodyData = new JSONObject();
            bodyData.AddField("loginId", id);
            bodyData.AddField("password", pw);

            APIManager.Instance.CallRESTAPI("/user/login", RESTAPI_TYPE.POST,
                (result, ackData) =>
                {
                    if (result == APIResult.SUCCESS)
                    {
                        callback(result, ackData.GetField("errorMsg")?.str);
                    }
                    else
                    {
                        callback(result, ackData.GetField("errorMsg")?.str);
                    }
                },
                bodyData);
        }
    }
}