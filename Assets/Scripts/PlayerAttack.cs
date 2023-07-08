using Assets.Scripts.Abstracts;
using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAttack : Attacker
    {
        private void Awake()
        {
            base.Awake();

            targetTag = "Mob";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                if (canAttack)
                {
                    collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
                    //Todo: play animation?
                    Debug.Log("attacking");

                    StartCoroutine(SetAttackCooldown());
                }
            }
        }
    }
}