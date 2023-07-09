using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimeManager : MonoBehaviour
    {
        public float timer;

        [SerializeField] private TextMeshProUGUI minutes1;
        [SerializeField] private TextMeshProUGUI minutes2;
        [SerializeField] private TextMeshProUGUI seconds1;
        [SerializeField] private TextMeshProUGUI seconds2;

        private bool gameRunning = true;

        private void OnEnable()
        {
            PlayerInterestManager.OnGameEnd += StopTimer;
        }

        private void OnDisable()
        {
            PlayerInterestManager.OnGameEnd -= StopTimer;
        }

        private void Start()
        {
            timer = 0.0f;
        }

        private void Update()
        {
            if (!gameRunning) return;

            timer += Time.deltaTime;
            UpdateTimeDisplay(timer);
        }

        private void UpdateTimeDisplay(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            string currentTime = string.Format("{00:00}{1:00}",minutes,seconds);

            minutes1.text = currentTime[0].ToString();
            minutes2.text = currentTime[1].ToString();
            seconds1.text = currentTime[2].ToString();
            seconds2.text = currentTime[3].ToString();
        }
        private void StopTimer(string obj)
        {
            gameRunning = false;
        }
    }
}