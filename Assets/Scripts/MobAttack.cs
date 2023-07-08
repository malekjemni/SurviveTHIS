using Assets.Scripts.Abstracts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class MobAttack : Attacker
    {
        private void Awake()
        {
            base.Awake();

            targetTag = "Player";
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                if (canAttack)
                {
                    collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
                    //Todo: play animation?

                    StartCoroutine(SetAttackCooldown());
                }
            }
        }
    }
}