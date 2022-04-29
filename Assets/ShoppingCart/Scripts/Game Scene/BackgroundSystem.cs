using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class BackgroundSystem : MonoBehaviour
    {
        private void OnEnable()
        {
            GameOverSystem.Instance.OnCountdownEnd += StartPlayMusic;
        }

        private void OnDisable()
        {
            GameOverSystem.Instance.OnCountdownEnd -= StartPlayMusic;
        }

        private void StartPlayMusic()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}