using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ShoppingCart.Scripts.Game_Scene
{
    public class GameVictory : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnEnable()
        {
            GameOverSystem.OnGameOver += CheckVictory;
        }
        void OnDisable()
        {
            GameOverSystem.OnGameOver -= CheckVictory;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CheckVictory()
        {
            SceneManager.LoadScene("VictoryAnimTest");
           
        }
    }
}
