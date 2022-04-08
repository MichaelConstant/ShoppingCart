using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShoppingCart.Scripts.Goods
{
    public enum GenerateState
    {
        Generator,
        Regenerator,
    }
    public class CourtesyCardRegenerator : Singleton<CourtesyCardRegenerator>
    {
        public List<Transform> CourtesyCardSpawnTransforms = new List<Transform>();
        public GameObject CourtesyCardGameObject;

        public float CourtesyCardDuration = 60f;
        public float RegenerateTime = 120f;
        public GenerateState _GenerateState;
        
        private float _regeneratorTimer = .0f;
        private float _generateCoolDownTimer = .0f;

        public static event Action OnClearCourtesyCard;
        
        private void Update()
        {
            if (!CourtesyCardGameObject || CourtesyCardSpawnTransforms == null) return;

            switch (_GenerateState)
            {
                case GenerateState.Generator:
                    GeneratorBehaviour();
                    break;
                case GenerateState.Regenerator:
                    RegeneratorBehaviour();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RegeneratorBehaviour()
        {
            _regeneratorTimer += Time.deltaTime;

            if (!(_regeneratorTimer >= RegenerateTime)) return;
            
            ClearAllCourtesyCard();
            
            _regeneratorTimer = .0f;

            _GenerateState = GenerateState.Generator;
        }

        private void GeneratorBehaviour()
        {
            _generateCoolDownTimer += Time.deltaTime;

            if (!(_generateCoolDownTimer >= CourtesyCardDuration)) return;
            
            RandomSpawnCourtesyCard();

            _generateCoolDownTimer = .0f;

            _GenerateState = GenerateState.Regenerator;
        }

        private void ClearAllCourtesyCard()
        {
            OnClearCourtesyCard?.Invoke();
        }

        private void RandomSpawnCourtesyCard()
        {
            var index = Random.Range(0, CourtesyCardSpawnTransforms.Count);
            var spawnTransform = CourtesyCardSpawnTransforms[index];
            SpawnCourtesyCardAtPosition(spawnTransform.position);
        }

        public void SpawnCourtesyCardAtPosition(Vector3 position)
        {
            Instantiate(CourtesyCardGameObject, position, Quaternion.identity);
        }
    }
}