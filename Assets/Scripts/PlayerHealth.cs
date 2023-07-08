using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        public float maxHealth = 100;
        public float health;

        private Stats stats;

        public static event Action OnPlayerDeath;

        private void Awake()
        {
            stats = GetComponent<Stats>();
        }

        private void OnEnable()
        {
            OnPlayerDeath += PlayerDeath;
            //Todo: onlevelup += UpdateHealth;
        }

        private void OnDisable()
        {
            OnPlayerDeath -= PlayerDeath;
        }

        public void TakeDamage(float damage)
        {
            health = health - damage;

            if (health <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void UpdateHealth(int level)
        {
            maxHealth *= stats.healthMultiplier;
            health *= stats.healthMultiplier;
        }

        public void PlayerDeath()
        {
            //Todo: death and scene transitions
        }
    }
}