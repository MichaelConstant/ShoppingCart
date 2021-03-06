using System;
using System.Collections;
using ShoppingCart.Scripts.Game_Scene;
using ShoppingCart.Scripts.Goods;
using TMPro;
using UnityEngine;

namespace ShoppingCart.Scripts.UI
{
    public class LocalPlayerTimeOutUIComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimeOutText;
        [SerializeField] private TextMeshProUGUI CouponAlertText;

        private const float _DISPLAY_TIME = 2f;

        private void Start()
        {
            TimeOutText.gameObject.SetActive(false);
            CouponAlertText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameOverSystem.OnGameOver += GameOver;
            CourtesyCardComponent.OnCouponSpawnOrTakeAway += StartDisplayText;
        }

        private void OnDisable()
        {
            GameOverSystem.OnGameOver -= GameOver;
        }

        private void GameOver()
        {
            TimeOutText.gameObject.SetActive(true);
        }
 
        private void StartDisplayText(string playerName)
        {
            CouponAlertText.gameObject.SetActive(true);
            StopAllCoroutines();

            StartCoroutine(playerName == "1230"
                ? DisplayText("The Coupon has appeared！")
                : DisplayText($"The Coupon has taken by {playerName}！"));
        }

        private IEnumerator DisplayText(string text)
        {
            CouponAlertText.gameObject.SetActive(true);
            CouponAlertText.text = text;
            
            yield return new WaitForSeconds(_DISPLAY_TIME);
            
            CouponAlertText.gameObject.SetActive(false);
        }
    }
}