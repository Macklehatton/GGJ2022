using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private PlayerMode mode;

    [SerializeField]
    private bool damageHuman;
    [SerializeField]
    private bool damageRobot;

    private bool playerInZone;

    private void Update()
    {
        if (playerInZone)
        {
            if (mode.RobotMode && damageRobot)
            {

                playerHealth.AdjustHealth(-1 * damage * Time.deltaTime);
            }
            else if (!mode.RobotMode && damageHuman)
            {
                playerHealth.AdjustHealth(-1 * damage * Time.deltaTime);
            }
        }           
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}
