using MET.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MET.Title.UI
{
    public class TitleView : MonoBehaviour
    {
        public bool IsExitPopupActive
        {
            get => exitPopup.activeSelf;
            set => exitPopup.SetActive(value);
        }


        [Header("Page")]
        public GameObject buttonGroup;

        [Header("ButtonGroup")]
        public Button mainButton;
        public Button newGameButton;
        public Button loadGameButton;
        public Button settingButton;
        public Button exitButton;

        [Header("ButtonGroup")]
        public GameObject exitPopup;
        public Button[] exitPopupButtons;


        #region Unity Methods
        private void Start()
        {
            BindEvents();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void BindEvents()
        {
            mainButton.onClick.AddListener(() => buttonGroup.SetActive(true));

            newGameButton.onClick.AddListener(OnClickNewGameButton);
            loadGameButton.onClick.AddListener(OnClickLoadGameButton);
            settingButton.onClick.AddListener(OnClickSettingButton);
            exitButton.onClick.AddListener(OnClickExitButton);

            exitPopupButtons[0].onClick.AddListener(OnClickExitNo);
            exitPopupButtons[1].onClick.AddListener(OnClickExitYes);
        }


        // 새게임 버튼을 눌렀을 때 호출되는 함수
        private void OnClickNewGameButton()
        {
            SceneLoadManager.Instance.LoadScene(1);
        }

        // 불러오기 버튼을 눌렀을 때 호출되는 함수
        private void OnClickLoadGameButton()
        {
            //우선 보류
        }

        // 설정 버튼을 눌렀을 때 호출되는 함수
        private void OnClickSettingButton()
        {
            //우선 보류
        }

        // 나가기 버튼을 눌렀을 때 호출되는 함수 
        private void OnClickExitButton()
        {
            IsExitPopupActive = true;
        }

        private void OnClickExitNo()
        {
            IsExitPopupActive = false;
        }

        private void OnClickExitYes()
        {
            Application.Quit();
        }
        #endregion
    }
}