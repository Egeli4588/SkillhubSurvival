using System;
using UnityEngine;
using UnityEngine.UI;

public class HeallthBar : MonoBehaviour
{
    [SerializeField] Image[] healthBars;
    [SerializeField] Image[] healthBarsUpgradable;


    [SerializeField] float currentHealth, maxHealth;
    [SerializeField] float eachHeartHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
          maxHealth = eachHeartHealth * healthBars.Length;
    }
    void Start()
    {
      
        currentHealth = maxHealth;
        PlayerState.Instance.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerState.Instance.currentHealth;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        float tempHealth = currentHealth;

        for (int i = 0; i < healthBars.Length; i++)
        {
            if (tempHealth >= eachHeartHealth)
            {
                healthBars[i].fillAmount = 1f;
                tempHealth -= eachHeartHealth;
            }
            else if (tempHealth > 0f)
            {
                healthBars[i].fillAmount = tempHealth / eachHeartHealth;
                tempHealth = 0f;
            }
            else
            {
                healthBars[i].fillAmount = 0f;
            }
        }
    }
}
