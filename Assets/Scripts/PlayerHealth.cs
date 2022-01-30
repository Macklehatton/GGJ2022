using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    public HealthBar healthBar;

    public float currentHP;

    public float CurrentHP { get => currentHP; }


    public void AdjustHealth(float deltaHealth)
    {
        currentHP += deltaHealth;
        healthBar.SetHealth(currentHP);
    }

    private void Start()
    {
        Reset();
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
