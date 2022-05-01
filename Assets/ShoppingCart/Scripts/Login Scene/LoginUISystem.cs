using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShoppingCart.Scripts.Login_Scene
{
    public class LoginUISystem : MonoBehaviour
    {
        public GameObject ConnectOptionsPanelGameObject;
        public GameObject SignInPanelGameObject;

        private void Start()
        {
            ConnectOptionsPanelGameObject.SetActive(true);
            SignInPanelGameObject.SetActive(false);
        }

        public void OnTutorialClicked()
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
