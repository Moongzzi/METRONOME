using MET.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Account
{
    public class TokenManager : Singleton<TokenManager>
    {
        private string accessToken = string.Empty;

        public string AccessToken
        {
            get
            {
                return accessToken;
            }
            private set { accessToken = value; }
        }

        public void SetAccessTokenFromServer(string token)
        {
            AccessToken = token;
        }

        public void ExpirationToken()
        {
            /*UIPopup.Instance.HideLoading();
            UIPopup.Instance.OpenNoticePopup(new PopupInfo()
            {
                title = EXPIRED.GetLocalizedString(),
                content = LOGIN_FAIL.GetLocalizedString(),
                okTitle = APIManager.DEFAULT_OK_STRING,
                onOkPressed = () =>
                {
                    AccountManager.Instance.TryLogout();
                }
            });*/
        }

        public void RemoveToken()
        {
            AccessToken = string.Empty;
        }
    }
}