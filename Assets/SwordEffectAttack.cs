using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffectAttack : MonoBehaviour
{
    PlayerAttack attacker;

    private void OnEnable()
    {
        attacker = GetComponentInParent<PlayerAttack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(attacker.damage);
        }
    }
}
