using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MET.Field
{
    public class ScriptViewer : MonoBehaviour
    {
        public ScriptManager scriptManager;

        [Header("Image")]
        public Image speakerImage;
        public Image researchItemImage;

        [Header("ScriptArea")]
        public TextMeshProUGUI speakerNameText;
        public TextMeshProUGUI scriptText;

        private int currentOrder = 0;
        private ResearchScript researchScript;


        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.KeypadEnter))
                return;

            if(researchScript.scriptInfos.Count > currentOrder + 1)
            {
                currentOrder += 1;
                SetScript(researchScript.scriptInfos[currentOrder]);
            }
            else if(researchScript.scriptInfos.Count == currentOrder + 1)
            {
                EndScript();
            }
        }

        public void OpenScript(ResearchObj researchObj)
        {
            researchScript = scriptManager.GetResearchInfo(researchObj);
            researchItemImage.sprite = scriptManager.GetResearchItemImg(researchObj);

            SetScript(researchScript.scriptInfos[currentOrder]);
        }

        private void SetScript(ScriptInfo scriptInfo)
        {
            switch (scriptInfo.speakerType)
            {
                case SpeakerType.MainPlayer:
                    speakerNameText.text = "주인공";
                    break;

                case SpeakerType.CEO:
                    speakerNameText.text = "사장";
                    break;

                case SpeakerType.Comment:
                    speakerNameText.text = "";
                    break;
            }

            if(scriptInfo.speakerType != SpeakerType.Comment)
                speakerImage.sprite = scriptManager.GetSpeakerImg(scriptInfo.speakerType);
            else
                speakerImage.sprite = null;

            scriptText.text = scriptInfo.speakText;
        }

        private void EndScript()
        {
            // 재화 획득 UI 발생
            Debug.Log("재화 획득");

            Destroy(gameObject);
        }
    }
}