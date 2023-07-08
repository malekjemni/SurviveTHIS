using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Stats : MonoBehaviour
    {
        [SerializeField]
        public float healthMultiplier = 1;

        [SerializeField]
        public float damageMultiplier = 1;

        [SerializeField]
        public int level = 1;
    }
}