using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    private bool robotMode;

    [SerializeField]
    private Transform playerGraphics;
    [SerializeField]
    private GameObject human;
    [SerializeField]
    private GameObject robot;

    public bool RobotMode { get => robotMode; }

    public void Update()
    {
        HandleModeSwitch();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            robotMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            robotMode = true;
        }
        else if ( Input.mouseScrollDelta.y != 0.0f ||
                  Input.GetKeyDown(KeyCode.LeftShift) ||
                  Input.GetKeyDown(KeyCode.F) ||
                  Input.GetKeyDown(KeyCode.Tab) )
        {
            robotMode = !RobotMode;
        }
        else if ( Input.GetKeyDown(KeyCode.M) )
        {
            AudioSource[] tracks = GetComponentsInChildren<AudioSource>();
            AudioSource humanTrack = tracks[1];
            AudioSource robotTrack = tracks[0];

            humanTrack.volume = 0.0f;
            robotTrack.volume = 0.1f;

            //mainTrack[0].volume = 0.0f;

            //this.GetComponents<AudioSource>();






        }
    }

    private void HandleModeSwitch()
    {
        CheckInputs();

        if (RobotMode)
        {
            playerGraphics.transform.parent = robot.transform;
            robot.SetActive(true);
            human.SetActive(false);
        }
        else
        {
            playerGraphics.transform.parent = human.transform;
            robot.SetActive(false);
            human.SetActive(true);
        }
    }
}
