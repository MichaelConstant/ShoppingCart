using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoppingCart.Scripts.Tutorial
{
    public class PlayerLocalUIComponent : MonoBehaviour
    {
        public void OnBackButtonClick()
        {
            SceneManager.LoadScene("LoginScene");
        }
    }
}