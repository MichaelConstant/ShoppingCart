using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Player
{
    public class InputComponent : MonoBehaviour
    {
        public bool CanPlayerInput { get; private set; }
        
        private const float _INPUT_RECOVER_TIME = 3f;

        private void OnEnable()
        {
            CanPlayerInput = true;
        }

        public void MutePlayerInput()
        {
            CanPlayerInput = false;
        
            Invoke(nameof(AllowPlayerInput) , _INPUT_RECOVER_TIME);
        }

        public void AllowPlayerInput()
        {
            CanPlayerInput = true;
        }
    }
}