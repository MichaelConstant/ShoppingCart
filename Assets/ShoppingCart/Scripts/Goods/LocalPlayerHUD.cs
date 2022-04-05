using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShoppingCart.Scripts.Goods
{
    public class LocalPlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image ExpBar;
        [SerializeField] private TextMeshProUGUI ScoreText;
        
        [SerializeField] private ScoreComponent ScoreComponent;

        public List<Image> PropImage = new List<Image>();

        private void Awake()
        {
            ScoreComponent.OnGetScore += UpdateExpBar;
            ScoreComponent.OnGetScore += UpdateScoreText;
            PropInventory.OnChangeProp += UpdatePropImage;
        }

        private void Start()
        {
            UpdateExpBar();
            UpdateScoreText();
            UpdatePropImage(0);
        }

        public void UpdateExpBar()
        {
            if (!ExpBar || !ScoreComponent) return;

            ExpBar.fillAmount = (float) ScoreComponent.Exp / ScoreComponent.MaxExp;
        }

        public void UpdateScoreText()
        {
            if (!ScoreText || !ScoreComponent) return;

            ScoreText.text = ScoreComponent.Score.ToString();
        }

        public void UpdatePropImage(int index)
        {
            if(index > PropImage.Count) return;

            foreach (var image in PropImage)
            {
                image.gameObject.SetActive(PropImage.IndexOf(image) == index);
            }
        }
    }
}