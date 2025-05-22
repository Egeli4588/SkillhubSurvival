using System;
using System.Collections;
using UnityEngine;

public class PlayerState : SingletonMonobehaviour<PlayerState>
{
    public float currentHealth;
    public float maxHealth;

    public float currentCalorie;
    public float maxCalorie;


    public float currentWater;
    public float maxWater;


    public GameObject player;
    private float distanceTravelled;
    private Vector3 lastPosition;

    public bool waterSystemActive = true;
    float timer;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentCalorie = maxCalorie;
        currentWater = maxWater;

        StartCoroutine(DecreaseWater());

    }

    IEnumerator DecreaseWater()
    {
        while (waterSystemActive)
        {
            currentWater--;
            yield return new WaitForSeconds(5f);
        }
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(player.transform.position, lastPosition);
        lastPosition = player.transform.position;

        if (distanceTravelled >= 5f)
        {
            distanceTravelled = 0f;
            currentCalorie--;
        }


        if (currentCalorie <= 0f)
        {
            currentCalorie = 0f;
            if (currentCalorie == 0f)
            {
                timer += Time.deltaTime;

                if (timer >= 5f)
                {
                    timer = 0f;
                    currentHealth--;
                }
            }
        }


    }

}
