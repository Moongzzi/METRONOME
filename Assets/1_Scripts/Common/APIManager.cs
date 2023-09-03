using MET.Account;
using MET.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MET.Common
{
    public enum APIResult
    {
        /// Response Code: 200
        SUCCESS,
        /// Response Code: 400, 403
        FAIL,
        /// Response Code: NOT 200, 400, 403
        ERROR
    }

    public enum RESTAPI_TYPE
    {
        POST,
        PUT,
        GET,
        DELETE
    }

    public class APIManager : Singleton<APIManager>
    {
        public const string APIServerSettingsFileName = "APIServerSettings";
        private static APIServerSettings restAPIserverSettings;
        public static APIServerSettings RESTAPIServerSettings
        {
            get
            {
                if (restAPIserverSettings == null)
                {
                    LoadAPIServerSettings();
                }

                return restAPIserverSettings;
            }
            private set { restAPIserverSettings = value; }
        }

        public const string DEFAULT_ERROR_MSG = "API ��ȸ�� �����Ͽ����ϴ�.\n�ٽ� �õ��� �ּ���.",
                            DEFAULT_ERROR_TITLE = "���� �߻�",
                            DEFAULT_FAIL_MSG = "API ��ȸ�� �����Ͽ����ϴ�.\n�ٽ� �õ��� �ּ���.",
                            DEFAULT_FAIL_TITLE = "���� �߻�",
                            DEFAULT_CANCEL_STRING = "���";



        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// ������ ����ϱ� ���� REST API ȣ�� �Լ�
        /// </summary>
        /// <param name="caseUrl">ȣ���� URL (ROOT URL ����)</param>
        /// <param name="type">REST API Ÿ�� ex) POST, GET ...</param>
        /// <param name="bodyData">Body�� ���� JSON ������</param>
        public void CallRESTAPI(string caseUrl, RESTAPI_TYPE type, Action<APIResult, JSONObject> OnRESTAPIAckReceived,
            JSONObject bodyData = null)
        {
            StartCoroutine(CoRequestAPI(caseUrl, type, OnRESTAPIAckReceived, bodyData));
        }

        public void CallRESTAPIUsedToken(string caseUrl, RESTAPI_TYPE type, Action<APIResult, JSONObject> OnRESTAPIAckReceived,
            JSONObject bodyData = null, string targetToken = null)
        {
            StartCoroutine(CoRequestAPI(caseUrl, type, OnRESTAPIAckReceived, bodyData, true, targetToken));
        }

        public IEnumerator CoRequestAPI(string caseUrl, RESTAPI_TYPE type, Action<APIResult, JSONObject> OnRESTAPIAckReceived,
            JSONObject bodyData, bool isUsedToken = false, string targetToken = null)
        {
            UnityWebRequest www = null;

            switch (type)
            {
                case RESTAPI_TYPE.POST:
                    www = UnityWebRequest.Post($"{RESTAPIServerSettings.RootURL}{caseUrl}", bodyData.ToString());

                    byte[] jsonToSendPOST = new System.Text.UTF8Encoding().GetBytes(bodyData.ToString());
                    // �ű� UploadHandler�� �Ҵ��ϱ� ��, UnityWebRequest.Post()���� �Ҵ��� �� ���� ���� ���������Ŵ
                    if (www.uploadHandler != null)
                        www.uploadHandler.Dispose();

                    www.uploadHandler = new UploadHandlerRaw(jsonToSendPOST);
                    www.SetRequestHeader("Content-Type", "application/json");
                    break;

                case RESTAPI_TYPE.PUT:
                    www = UnityWebRequest.Put($"{RESTAPIServerSettings.RootURL}{caseUrl}", bodyData.ToString());

                    byte[] jsonToSendPUT = new System.Text.UTF8Encoding().GetBytes(bodyData.ToString());
                    // ����ó��, UnityWebRequest.Put()���� �Ҵ��� �� ���� ���� ���������Ŵ
                    if (www.uploadHandler != null)
                        www.uploadHandler.Dispose();

                    www.uploadHandler = new UploadHandlerRaw(jsonToSendPUT);
                    www.SetRequestHeader("Content-Type", "application/json");
                    break;

                case RESTAPI_TYPE.GET:
                    www = UnityWebRequest.Get($"{RESTAPIServerSettings.RootURL}{caseUrl}");
                    break;

                case RESTAPI_TYPE.DELETE:
                    www = UnityWebRequest.Delete($"{RESTAPIServerSettings.RootURL}{caseUrl}");
                    www.downloadHandler = new DownloadHandlerBuffer();
                    break;

                default:
                    Debug.Log($"APIManager::CoRequestAPI() : Wrong api request type called. ({type})");
                    yield break;
            }
            www.timeout = 10;

            Debug.Log($"[{type}] {www.url} called.\nbodyData : {bodyData?.ToString(pretty: true) ?? "(None)"}");

            var token = targetToken ?? TokenManager.Instance.AccessToken;
            if (token is not null)
                www.SetRequestHeader("Authorization", token);

            yield return www.SendWebRequest();

            JSONObject ackData = null;

            switch (www.result)
            {
                case UnityWebRequest.Result.Success:
                case UnityWebRequest.Result.ProtocolError:
                    ackData = new JSONObject(www.downloadHandler?.text);

                    if ((bool)(ackData?.HasField("token")))
                    {
                        TokenManager.Instance.SetAccessTokenFromServer($"Bearer {ackData.GetField("token").str}");
                    }

                    break;

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogWarning($"======================== {www.responseCode} {www.error}");
                    Debug.Log($"======================== {www.result}");

                    break;
                default:
                    Debug.LogWarning($"======================== {www.responseCode} {www.error}");

                    break;
            }

            Debug.Log($"[{type}] {www.url} result : {www.result} ({www.responseCode})\nackData : {ackData?.ToString(pretty: true) ?? "(None)"}");

            if (OnRESTAPIAckReceived != null)
            {
                APIResult result;

                switch (www.responseCode)
                {
                    case 200:
                        result = APIResult.SUCCESS;
                        break;

                    case 400:
                        result = APIResult.FAIL;
                        break;

                    case 403:
                        result = APIResult.ERROR;
                        TokenManager.Instance.ExpirationToken();
                        break;

                    default:
                        result = APIResult.ERROR;
                        break;
                }

                try
                {
                    if (www.responseCode != 403)
                        OnRESTAPIAckReceived(result, ackData);
                }
                catch (System.NullReferenceException e)
                {
                    Debug.LogError("APIManager::CoRequestAPI : NullReference error occurred\n" + e);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            www.Dispose();
        }

        public static void LoadAPIServerSettings()
        {
            restAPIserverSettings = (APIServerSettings)Resources.Load(APIServerSettingsFileName, typeof(APIServerSettings));

            if (restAPIserverSettings != null)
            {
                return;
            }
        }
    }
}