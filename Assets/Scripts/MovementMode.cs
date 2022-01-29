using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMode : MonoBehaviour
{
    public bool robotMode;

    [SerializeField]
    private GameObject human;
    [SerializeField]
    private GameObject robot;

    public void Update()
    {
        CheckModeSwitch();

        if (robotMode)
        {
            robot.SetActive(true);
            human.SetActive(false);
        }
        else
        {
            robot.SetActive(false);
            human.SetActive(true);
        }
    }

    public void CheckModeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            robotMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            robotMode = true;
        }

        if (Input.mouseScrollDelta.y != 0.0f)
        {
            robotMode = !robotMode;
        }
    }
}
