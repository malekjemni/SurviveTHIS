using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public int health;

        public static event Action OnPlayerDeath;

        private void OnEnable()
        {
            OnPlayerDeath += PlayerDeath;
        }

        private void OnDisable()
        {
            OnPlayerDeath -= PlayerDeath;
        }

        public void TakeDamage(int damage)
        {
            health = health - damage;
            if (health <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }

        public void PlayerDeath()
        {
            //Todo: death and scene transitions
        }
    }
}