using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private PlayerHealth health;
    [SerializeField] private Stats playerStats;
    [SerializeField] private Slider healthbar;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI leveltext;
    void Start()
    {
        //totalHealthBar.fillAmount = health.currentHealth / 10;
        healthbar.maxValue = health.maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        // currentHealthBar.fillAmount = health.currentHealth / 10;
        healthbar.value = health.health;
        text.text = health.health + "/" + health.maxHealth;
        leveltext.text = playerStats.level.ToString();


    }
}
