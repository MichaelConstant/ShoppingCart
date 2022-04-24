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

        private void Start()
        {
            CountDownTime.gameObject.SetActive(true);
            StartGameTime.gameObject.SetActive(false);
            StartText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameOverSystem.Instance.OnStartTimerUpdate += UpdateStartGameTime;
            GameOverSystem.Instance.OnCountTimerUpdate += UpdateCountDown;
            GameOverSystem.Instance.OnCountdownEnd += EndCountDown;
        }

        private void OnDisable()
        {
            GameOverSystem.Instance.OnStartTimerUpdate -= UpdateStartGameTime;
            GameOverSystem.Instance.OnCountTimerUpdate -= UpdateCountDown;
            GameOverSystem.Instance.OnCountdownEnd -= EndCountDown;
        }

        private void UpdateCountDown(int countDownInt)
        {
            CountDownTime.text = "" + countDownInt;
        }

        private void UpdateStartGameTime(int min, int sec)
        {
            StartGameTime.text = "" + min + " : " + sec;
        }

        private void EndCountDown()
        {
            StartGameTime.gameObject.SetActive(true);
            StartCoroutine(DisplayStartText());
            CountDownTime.gameObject.SetActive(false);
        }

        private IEnumerator DisplayStartText()
        {
            StartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            StartText.gameObject.SetActive(false);
            yield return null;
        }
    }
}