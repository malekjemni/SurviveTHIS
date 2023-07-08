using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInterestManager : MonoBehaviour
    {
        public float interestLevel;

        private float interestPerSecond = -1.0f;

        private float maxInterest = 100.0f;
        private float minInterest = -20.0f;

        public string feedBack;

        public static event Action OnPlayerSatisfied;
        public static event Action OnPlayerBored;

        private void OnEnable()
        {
            PlayerHealth.OnPlayerDeath += PlayerDeathEnding;
            MobHealth.OnAnyMobDeath += GainInterestByKilling;
            PlayerLevelManager.OnLevelUp += LevelUpInterest;
        }

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDeath -= PlayerDeathEnding;
            MobHealth.OnAnyMobDeath -= GainInterestByKilling;
            PlayerLevelManager.OnLevelUp -= LevelUpInterest;
        }

        private void Update()
        {
            interestLevel += interestPerSecond * Time.deltaTime;

            if (interestLevel > maxInterest)
            {
                OnPlayerSatisfied?.Invoke();
                return;
            }

            if (interestLevel < minInterest) 
            { 
                OnPlayerBored?.Invoke();
                return;
            }
        }

        private void GainInterestByKilling(int level)
        {
            interestLevel += 0.5f;
        }

        private void LevelUpInterest(int level)
        {
            interestPerSecond = -1 - level * 0.1f;
        }

        private void PlayerDeathEnding()
        {
            //check time
            //check interest level
        }
    }
}