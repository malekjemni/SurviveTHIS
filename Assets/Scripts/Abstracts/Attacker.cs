using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Abstracts
{
    public abstract class Attacker : MonoBehaviour
    {
        internal Animator animator;
        internal Stats stats;

        [SerializeField]
        public float damage;
        [SerializeField]
        public float attackInterval;
        internal bool canAttack = true;

        internal string targetTag;

        internal void Awake()
        {
            stats = GetComponent<Stats>();
            animator = GetComponent<Animator>();
        }

        internal IEnumerator SetAttackCooldown()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackInterval);

            canAttack = true;
            yield return null;
        }
    }
}