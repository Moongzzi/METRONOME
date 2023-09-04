using MET.Account;
using MET.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MET.Title.UI
{
    public class TitleView : MonoBehaviour
    {
        public bool IsHoverNewGameButton
        {
            get => buttonHoverObjs[0].activeSelf;
            set => buttonHoverObjs[0].SetActive(value);
        }
        public bool IsHoverLoadGameButton
        {
            get => buttonHoverObjs[1].activeSelf;
            set => buttonHoverObjs[1].SetActive(value);
        }
        public bool IsHoverSettingButton
        {
            get => buttonHoverObjs[2].activeSelf;
            set => buttonHoverObjs[2].SetActive(value);
        }
        public bool IsHoverExitButton
        {
            get => buttonHoverObjs[3].activeSelf;
            set => buttonHoverObjs[3].SetActive(value);
        }


        [Header("MainPage")]
        public GameObject bigLogo;
        public GameObject pressGuideObj;

        [Header("ButtonPage")]
        public GameObject buttonPage;
        public Button newGameButton;
        public Button loadGameButton;
        public Button settingButton;
        public Button exitButton;

        [Header("ButtonHover")]
        public GameObject[] buttonHoverObjs;

        [Header("LoginPage")]
        public GameObject loginPage;
        public TMP_InputField idField;
        public TMP_InputField passWordField;
        public Button loginExitButton;
        public Button loginButton;

        [Header("ExitPage")]
        public GameObject exitPage;
        public Button exitYesButton;
        public Button exitNoButton;

        #region Unity Methods
        private void Start()
        {
            Localization();
        }

        private void Update()
        {
            if(Input.anyKey && pressGuideObj.activeSelf)
            {
                bigLogo.SetActive(false);
                pressGuideObj.SetActive(false);

                buttonPage.SetActive(true);
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void Localization()
        {
            bigLogo.SetActive(true);
            pressGuideObj.SetActive(true);

            buttonPage.SetActive(false);
            loginPage.SetActive(false);
            exitPage.SetActive(false);

            for(int i = 0; i < buttonHoverObjs.Length; i++)
            {
                buttonHoverObjs[i].SetActive(false);
            }

            idField.text = "";
            passWordField.text = "";

            BindEvents();
        }

        private void BindEvents()
        {
            newGameButton.onClick.AddListener(OnClickNewGameButton);
            loadGameButton.onClick.AddListener(OnClickLoadGameButton);
            settingButton.onClick.AddListener(OnClickSettingButton);
            exitButton.onClick.AddListener(OnClickExitButton);

            loginExitButton.onClick.AddListener(OnClickExitButton);
            loginButton.onClick.AddListener(OnClickLogin);

            exitYesButton.onClick.AddListener(OnClickExitYes);
            exitNoButton.onClick.AddListener(OnClickExitNo);
        }


        // 새게임 버튼을 눌렀을 때 호출되는 함수
        private void OnClickNewGameButton()
        {
            buttonPage.SetActive(false);
            loginPage.SetActive(true);
        }

        // 불러오기 버튼을 눌렀을 때 호출되는 함수
        private void OnClickLoadGameButton()
        {
            //우선 보류
            buttonPage.SetActive(false);
            loginPage.SetActive(true);
        }

        // 설정 버튼을 눌렀을 때 호출되는 함수
        private void OnClickSettingButton()
        {
            //우선 보류
        }

        // 나가기 버튼을 눌렀을 때 호출되는 함수 
        private void OnClickExitButton()
        {
            exitPage.SetActive(true);
        }

        private void OnClickExitNo()
        {
            exitPage.SetActive(false);
        }

        private void OnClickExitYes()
        {
            Application.Quit();
        }

        // 로그인 함수
        private void OnClickLogin()
        {
            string idText = idField.text;
            string pwText = passWordField.text;

            if (string.IsNullOrEmpty(idText) || string.IsNullOrEmpty(pwText))
            {
                Debug.Log("ID, PW 입력 요망");
                return;
            }

            AccountManager.Instance.TryLogin(idText, pwText, OnClickLoginCallback);
        }

        private void OnClickLoginCallback(APIResult result, string msg)
        {
            if (result == APIResult.SUCCESS)
            {
                // 기존 저장 파일 있는지 체크 하여 화면 표출 및 씬 이동


                SceneLoadManager.Instance.LoadScene(1);
            }
        }
        #endregion
    }
}