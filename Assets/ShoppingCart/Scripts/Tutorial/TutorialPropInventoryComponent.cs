using System.Collections.Generic;
using ShoppingCart.Scripts.Audio;
using UnityEngine;

namespace ShoppingCart.Scripts.Tutorial
{
    public class TutorialPropInventoryComponent : MonoBehaviour
    {
        public List<TutorialPropBaseComponent> PropList = new List<TutorialPropBaseComponent>();

        public TutorialPropBaseComponent CurrentProp => _currentProp;
        private TutorialPropBaseComponent _currentProp;

        public delegate void OnChangePropHandler(int number);

        public event OnChangePropHandler OnChangeProp;

        private AudioSource _audioSource;
        
        private void OnEnable()
        {
            var scoreComponent = GetComponent<TutorialScoreComponent>();
            _audioSource = GetComponent<AudioSource>();
            if (!scoreComponent) return;
            scoreComponent.OnGetProp += GetProp;
        }

        private void GetProp()
        {
            if (PropList == null || PropList.Count < 1) return;

            var index = Random.Range(0, PropList.Count - 1);

            var newProp = PropList[index];

            AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.PropGet);
            
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