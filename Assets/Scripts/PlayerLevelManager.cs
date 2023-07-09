using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerLevelManager : MonoBehaviour
    { 
        public int currentXp;

        public List<float> xpThreshholds;

        private Stats stats;

        [SerializeField]
        private GameObject levelUpParticles;
        [SerializeField] private AudioClip levelUpsound;

        public static event Action<int> OnLevelUp;

        private void Awake()
        {
            stats = GetComponent<Stats>();
        }

        private void Start()
        {
            float req;
            for(int i = 0; i <9;  i++)
            {
                req = Mathf.Pow((i + 1) / 0.4f, 3.0f);

                Debug.Log($"Level {i + 2}: {req}xp");
                xpThreshholds.Add(req);
            }
        }

        private void OnEnable()
        {
            MobHealth.OnAnyMobDeath += GainXP;
        }

        private void OnDisable()
        {
            MobHealth.OnAnyMobDeath -= GainXP;
        }

        private void GainXP(int mobLevel)
        {
            //switch(mobLevel)
            //{
            //    case 1:
            //        currentXp += 1;
            //        break;
            //    case 2:
            //        currentXp += 4;
            //        break;
            //    case 3:
            //        currentXp += 8;
            //        break;
            //    case 4:
            //        currentXp += 13;
            //        break;
            //    case 5: 
            //        currentXp += 20;
            //        break;
            //    case 6:
            //        currentXp += 27;
            //        break;
            //    case 7:
            //        currentXp += 35;
            //        break;
            //    case 8: 
            //        currentXp += 42;
            //        break;
            //    case 9:
            //        currentXp += 50;
            //        break;
            //    case 10:
            //        currentXp += 70;
            //        break;
            //}
            currentXp += mobLevel;

            if (currentXp > xpThreshholds[stats.level - 1])
            {
                stats.level++;
                OnLevelUp?.Invoke(stats.level);
                GameObject partcile = Instantiate(levelUpParticles, transform.position, Quaternion.identity,transform);
                Destroy(partcile, 2f);
                SoundManager.instance.PlaySound(levelUpsound);
            }
        }
    }
}