using Assets.Scripts.Abstracts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAttack : Attacker
    {
        public GameObject swordSlice;
        [SerializeField] private AudioClip swordSliceSound;
        private float baseDamage;
        private List<GameObject> mobsInRange = new List<GameObject>();

        private new void Awake()
        {
            animator = GetComponentInParent<Animator>();
            stats = GetComponentInParent<Stats>();

            targetTag = "Mob";
            baseDamage = damage;

            mobsInRange.Clear();
        }

        private void Start()
        {
            UpdateDamage(stats.level);
        }

        private void OnEnable()
        {
            PlayerLevelManager.OnLevelUp += UpdateDamage;
        }

        private void OnDisable()
        {
            PlayerLevelManager.OnLevelUp -= UpdateDamage;
        }

        private void Update()
        {
            AttackMobs();

        }

        private void AttackMobs()
        {
            if (!canAttack) return;

            //foreach (GameObject mob in mobsInRange)
            //{
            //    mob.GetComponent<IDamageable>().TakeDamage(damage);
            //}

            animator.SetTrigger("attack");
            SoundManager.instance.PlaySound(swordSliceSound);
            Instantiate(swordSlice,transform.position,Quaternion.identity, transform);

            StartCoroutine(SetAttackCooldown());
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.CompareTag(targetTag))
        //    {
        //        mobsInRange.Add(collision.gameObject);
        //    }
        //}

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.gameObject.CompareTag(targetTag))
        //    {
        //        mobsInRange.Remove(collision.gameObject);
        //    }
        //}

        private void UpdateDamage(int level)
        {
            damage = baseDamage * Mathf.Pow(2, level-1) * stats.damageMultiplier;
        }
    }
}