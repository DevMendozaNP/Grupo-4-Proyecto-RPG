using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] 
    private GameObject healthBar;
    private Transform healthTransform;
    private float xNewHealthScale;
    public float health;
    private float startHealth;
    private Vector2 healthScale;

    private void Awake()
    {
        healthTransform = healthBar.GetComponent<RectTransform>();
        healthScale = healthBar.transform.localScale;
        startHealth = health;
    }

    public void ReceiveDamage(float HP)
    {
        health = HP;

        if(health <= 0)
        {
            Destroy(healthBar);
        }
        else
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthBar.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }

    }
}
