using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameState gameState;
    private float maxHP = 100.0f;
    private HealthBar healthBar;

    private float currentHP;

    public float CurrentHP { get => currentHP; }

    public void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        gameState = FindObjectOfType<GameState>();
        Reset();
    }

    public void AdjustHealth(float deltaHealth)
    {
        currentHP += deltaHealth;
        healthBar.SetHealth(currentHP);
    }

    private void Update()
    {
        if (currentHP <= 0.0f)
        {
            Reset();
            gameState.Restart();
        }
    }

    public void Reset()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP);
    }
}
