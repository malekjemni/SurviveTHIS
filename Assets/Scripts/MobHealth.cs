using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MobHealth : MonoBehaviour, IDamageable
    {
        public float health = 30;

        private Stats stats;
        public GameObject particles;
        public event Action OnMobDeath;
        public static event Action<int> OnAnyMobDeath;

        private void Awake()
        {
            stats = GetComponent<Stats>();
        }

        private void OnEnable()
        {
            OnMobDeath += MobDeath;
        }

        private void OnDisable()
        {
            OnMobDeath -= MobDeath;
        }

        private void Start()
        {
            health *= Mathf.Pow(2, stats.level-1) * stats.healthMultiplier;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if(health <= 0)
            {
                OnMobDeath?.Invoke();
                OnAnyMobDeath?.Invoke(stats.level);
            }
        }

        private void MobDeath()
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //Todo: play death anim and effects
        }
    }
}