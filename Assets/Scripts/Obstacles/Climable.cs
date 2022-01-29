using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climable : MonoBehaviour
{
    [SerializeField]
    private MovementMode mode;
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private bool byHuman;
    [SerializeField]
    private bool byRobot;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (byHuman && !mode.robotMode)
        {
            movement.canClimb = true;
        }
        else if (byRobot && mode.robotMode)
        {
            movement.canClimb = true;
        }
        else
        {
            movement.canClimb = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (byHuman && !mode.robotMode)
        {
            movement.canClimb = true;
        }
        else if (byRobot && mode.robotMode)
        {
            movement.canClimb = true;
        }
        else
        {
            movement.canClimb = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        movement.canClimb = false;
    }
}
