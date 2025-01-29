using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;
    [SerializeField] private Transform canvasObj;
    [SerializeField] private Slider healthBar;

    public Action<float> onDamage;
    public Action onDie;

    private Camera mainCamera;

    public float CurHealth { get => curHealth; set 
        {
            curHealth = value;
            onDamage?.Invoke(curHealth);

            if (curHealth <= 0)
            {
                onDie?.Invoke();
                curHealth = 0;
            }

            healthBar.value = curHealth;
        } 
    }

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        CurHealth = maxHealth;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        canvasObj.transform.LookAt(mainCamera.transform.position);
    }

    public void Damage(float damage)
    {
        CurHealth -= damage;
    }
}
