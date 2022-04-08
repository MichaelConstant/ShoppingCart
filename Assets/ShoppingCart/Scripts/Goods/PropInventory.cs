using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Goods
{
    public class PropInventory : MonoBehaviour
    {
        public List<PropBase> PropList = new List<PropBase>();

        public PropBase CurrentProp => _currentProp;
        private PropBase _currentProp;

        public delegate void OnChangePropHandler(int number);

        public static event OnChangePropHandler OnChangeProp;
        
        private void OnEnable()
        {
            ScoreComponent.OnGetProp += GetProp;
        }

        private void OnDisable()
        {
            ScoreComponent.OnGetProp -= GetProp;
        }

        private void GetProp()
        {
            if (PropList == null || PropList.Count < 1) return;

            var index = Random.Range(0, PropList.Count - 1);

            var newProp = PropList[index];

            _currentProp = newProp;

            OnChangeProp?.Invoke(index + 1);
        }
        
        public void ShootProp()
        {
            _currentProp = null;
            OnChangeProp?.Invoke(0);
        }
    }
}