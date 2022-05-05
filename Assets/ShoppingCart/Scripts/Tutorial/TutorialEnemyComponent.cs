using System;
using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialEnemyComponent : MonoBehaviour
    {
        public Guid Guid;
        private Animator _animator;

        private void Start()
        {
            Guid = Guid.NewGuid();
            _animator = GetComponent<Animator>();
        }

        public void BeHurt()
        {
            _animator.SetTrigger("BeHurt");
        }
    }
}