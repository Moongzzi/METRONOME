using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using UnityEngine.Video;

namespace MET.Battle
{
    public class GenerateNotes : MonoBehaviour
    {
        public Animator chAnimator;
        public Animator monAnimator;
        public VideoPlayer videoPlayer;

        public Image noteLine;
        public RectTransform[] outCircles;
        public RectTransform[] inCircles;

        public Button[] noteButtons;

        public Image skilIcon;
        public RawImage skilLiveImage;

        private bool isRytming = false;
        private bool isSuccess = false;
        private int noteNum = 0;
        private float lineSpeed = 7f;
        private float noteSpeed = 2f;

        private void Start()
        {
            noteLine.fillAmount = 0f;

            foreach (var circle in outCircles)
                circle.gameObject.SetActive(false);

            foreach (var circle in inCircles)
                circle.gameObject.SetActive(false);

            for(int i = 0; i < noteButtons.Length; i++)
            {
                int j = i;
                noteButtons[i].onClick.AddListener(() => OnClickNote(j));
            }

            skilIcon.fillAmount = 0f;
            skilLiveImage.gameObject.SetActive(false);

            Invoke("StartRythm", 5);
        }

        private void StartRythm()
        {
            isRytming = true;
            noteNum = 0;
            StartCoroutine(FillLine(noteNum));
        }

        private IEnumerator FillLine(int areaNum)
        {
            float targetAmount = 0f;

            switch (areaNum)
            {
                case 0:
                    targetAmount = 0.3f;
                    break;

                case 1:
                    targetAmount = 0.65f;
                    break;

                case 2:
                    targetAmount = 0.96f;
                    break;
            }

            while(noteLine.fillAmount < targetAmount)
            {
                noteLine.fillAmount += Time.deltaTime * lineSpeed;
                yield return new WaitForSeconds(0.1f);
            }

            StartCoroutine(NoteSizing(areaNum));
            yield return null;
        }

        private IEnumerator NoteSizing(int areaNum)
        {
            isSuccess = false;

            float max = 250;
            float min = 10;

            outCircles[areaNum].sizeDelta = new Vector2(max, max);
            inCircles[areaNum].sizeDelta = new Vector2(min, min);

            outCircles[areaNum].gameObject.SetActive(true);
            inCircles[areaNum].gameObject.SetActive(true);

            while (outCircles[areaNum].rect.width + 10 > inCircles[areaNum].rect.width)
            {
                max -= Time.deltaTime * 1000 * noteSpeed;
                min += Time.deltaTime * 1000 * noteSpeed;

                outCircles[areaNum].sizeDelta = new Vector2(max, max);
                inCircles[areaNum].sizeDelta = new Vector2(min, min);

                yield return new WaitForSeconds(0.1f);

                if (isSuccess)
                {
                    break;
                }
            }

            outCircles[areaNum].gameObject.SetActive(false);
            inCircles[areaNum].gameObject.SetActive(false);

            if (isSuccess)
            {
                skilIcon.fillAmount += 0.35f;
                chAnimator.SetInteger("State", 1);
            }
            else
            {
                chAnimator.SetInteger("State", 2);
            }

            noteNum++;

            yield return new WaitForSeconds(1f);

            if (noteNum < 3)
            {
                StartCoroutine(FillLine(noteNum));
                chAnimator.SetInteger("State", 0);
            }
            else
            {
                chAnimator.SetInteger("State", 0);
                AttackMonster();
            }

            yield return null;
        }

        private void AttackMonster()
        {
            if(skilIcon.fillAmount >= 1f)
            {
                videoPlayer.Play();
                skilLiveImage.gameObject.SetActive(true);
                Invoke("ActionSkil", 3f);
            }
            else
            {
                Debug.Log("평타");
            }
        }

        private void ActionSkil()
        {
            skilLiveImage.gameObject.SetActive(false);
            chAnimator.SetTrigger("StartSkill");
            Invoke("MonsterHurt", 2.5f);
        }

        private void MonsterHurt()
        {
            monAnimator.SetTrigger("Attacked");
        }

        private void OnClickNote(int areaNum)
        {
            if(outCircles[areaNum].rect.width - inCircles[areaNum].rect.width < 30f)
            {
                isSuccess = true;
            }
        }
    }
}