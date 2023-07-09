using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class PlayerInterestManager : MonoBehaviour
    {
        public float interestLevel;

        private float interestPerSecond = -1.0f;

        [SerializeField]
        private float baseInterestPerSecond = -1.0f;
        [SerializeField]
        private float interestPerKill = 0.5f;

        private float maxInterest = 100.0f;
        private float minInterest = -20.0f;

        public string feedBack;

        [SerializeField]
        private TimeManager timeManager;
        [SerializeField]
        private Image playerFace;
        [SerializeField]
        private Sprite boredSprite;
        [SerializeField]
        private Sprite neutralSprite;
        [SerializeField]
        private Sprite kinaFunSprite;
        [SerializeField]
        private Sprite satisfiedSprite;

        private float randomMessageTimer;
        [SerializeField]
        private List<string> randomMessages;
        [SerializeField]
        private TextMeshProUGUI randomMessageTextField;
        [SerializeField]
        private GameObject gameOverPanel;
        public TextMeshProUGUI PlayerFeed;

        public static event Action OnPlayerSatisfied;
        public static event Action OnPlayerBored;
        public static event Action<string> OnGameEnd;

        public static event Action OnPlayerInterestChanged;

        private void OnEnable()
        {
            PlayerHealth.OnPlayerDeath += PlayerDeathEnding;
            OnPlayerBored += PlayerBoredEnding;
            OnPlayerSatisfied += PlayerSatisfiedEnding;


            MobHealth.OnAnyMobDeath += GainInterestByKilling;
            PlayerLevelManager.OnLevelUp += LevelUpInterest;

            OnGameEnd += GameEnds;
        }

       

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDeath -= PlayerDeathEnding;
            OnPlayerBored -= PlayerBoredEnding;
            OnPlayerSatisfied -= PlayerSatisfiedEnding;


            MobHealth.OnAnyMobDeath -= GainInterestByKilling;
            PlayerLevelManager.OnLevelUp -= LevelUpInterest;

            OnGameEnd -= GameEnds;
        }

        private void GameEnds(string obj)
        {
          gameOverPanel.SetActive(true);
          PlayerFeed.text = obj;
        }

        private void Start()
        {
            OnPlayerInterestChanged?.Invoke();
            LevelUpInterest(1);

            randomMessageTimer = 60.0f;
        }

        private void Update()
        {
            randomMessageTimer -= Time.deltaTime;
            if(randomMessageTimer < 0)
            {
                PlayRandomMessage();
                randomMessageTimer = 60.0f;
            }

            interestLevel += interestPerSecond * Time.deltaTime;

            if (interestLevel > maxInterest)
            {
                OnPlayerSatisfied?.Invoke();
                return;
            }

            if (interestLevel < minInterest) 
            { 
                OnPlayerBored?.Invoke();
                return;
            }

            UpdatePlayerFace();
        }

        private void PlayRandomMessage()
        {
            int i = Random.Range(0, randomMessages.Count);

            randomMessageTextField.text = randomMessages[i];

        }

        private void GainInterestByKilling(int level)
        {
            interestLevel += interestPerKill;
        }

        private void LevelUpInterest(int level)
        {
            interestPerSecond = baseInterestPerSecond - level * 0.1f;
        }

        private void PlayerDeathEnding()
        {
            if(timeManager.timer < 20.0f)
            {
                feedBack = "I think this game is not for me, it's too hard!";
                OnGameEnd?.Invoke(feedBack);
                return;
            }
                

            else if(timeManager.timer < 60.0f)
            {
                feedBack = "the balance seems a bit off";
                OnGameEnd?.Invoke(feedBack);
                return;
            }
            

            if (interestLevel < 0.0f)
                feedBack = "Are those monsters too hard to kill?";

            else if (interestLevel < 50.0f)
                feedBack = "it's unfair that I died, but it was kinda fun";

            else if (interestLevel < 80.0f)
                feedBack = "this game is awesome!";

            else
                feedBack = "WOW! best game ever! Can't wait to start again!";

            OnGameEnd?.Invoke(feedBack);
        }

        private void PlayerSatisfiedEnding()
        {
            if (timeManager.timer < 60.0f)
                feedBack = "This was Awesome. a little fast paced but fun.";

            else
                feedBack = "This game is awesome, I killed so many monsters. But I feel like I had enough.";

            OnGameEnd?.Invoke(feedBack);
        }

        private void PlayerBoredEnding()
        {
            if (timeManager.timer < 21.0f)
                feedBack = "Where did the monsters go??!";

            else
                feedBack = "Alright, I had enough. This game is boring!";

            OnGameEnd?.Invoke(feedBack);
        }

        private void UpdatePlayerFace()
        {
            if (interestLevel < 0.0f)
                playerFace.sprite = boredSprite;
            else if (interestLevel < 50.0f)
                playerFace.sprite = neutralSprite;
            else if (interestLevel < 80.0f)
                playerFace.sprite = kinaFunSprite;
            else
                playerFace.sprite = satisfiedSprite;
        }
    }
}