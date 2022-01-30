using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float startingHP;
    [SerializeField]
    private GameState gameState;

    public float currentHP;


    public void AdjustHealth(float deltaHealth)
    {
        currentHP += deltaHealth;
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
        currentHP = startingHP;
    }
}
