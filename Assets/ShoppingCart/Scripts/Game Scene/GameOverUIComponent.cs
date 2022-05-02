using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class GameOverUIComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CountDownTime;
        [SerializeField] private TextMeshProUGUI StartGameTime;
        [SerializeField] private TextMeshProUGUI StartText;

        [SerializeField] private GameObject CountDown3;
        [SerializeField] private GameObject CountDown2;
        [SerializeField] private GameObject CountDown1;
        [SerializeField] private GameObject StartGameObject;

        private void Start()
        {
            CountDown3.SetActive(false);
            CountDown2.SetActive(false);
            CountDown1.SetActive(false);
            StartGameObject.SetActive(false);

            // CountDownTime.gameObject.SetActive(true);
            // StartGameTime.gameObject.SetActive(false);
            // StartText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameOverSystem.OnStartTimerUpdate += UpdateStartGameTime;
            GameOverSystem.OnCountTimerUpdate += UpdateCountDown;
            GameOverSystem.OnCountdownEnd += EndCountDown;
        }

        private void OnDisable()
        {
            GameOverSystem.OnStartTimerUpdate -= UpdateStartGameTime;
            GameOverSystem.OnCountTimerUpdate -= UpdateCountDown;
            GameOverSystem.OnCountdownEnd -= EndCountDown;
        }

        private void UpdateCountDown(int countDownInt)
        {
            switch (countDownInt)
            {
                case 3:
                    CountDown3.SetActive(true);
                    break;
                case 2:
                    CountDown3.SetActive(false);
                    CountDown2.SetActive(true);
                    break;
                case 1:
                    CountDown2.SetActive(false);
                    CountDown1.SetActive(true);
                    break;
            }


            // CountDownTime.text = "" + countDownInt;
        }

        private void UpdateStartGameTime(int min, int sec)
        {
            // StartGameTime.text = "" + min + " : " + sec;
        }

        private void EndCountDown()
        {
            // StartGameTime.gameObject.SetActive(true);
            CountDown1.SetActive(false);
            
            StartCoroutine(DisplayStartText());
            // CountDownTime.gameObject.SetActive(false);
        }

        private IEnumerator DisplayStartText()
        {
            StartGameObject.SetActive(true);
            // StartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            StartGameObject.SetActive(false);
            // StartText.gameObject.SetActive(false);
            yield return null;
        }
    }
}