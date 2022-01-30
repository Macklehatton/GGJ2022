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

    private bool muting = false;

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
        else if (Input.GetKeyDown(KeyCode.M))
        {
            mute();
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

        SwitchTrack(robotMode);
    }

    private void SwitchTrack(bool toRobot)
    {
        AudioSource[] tracks = GetComponentsInChildren<AudioSource>();
        AudioSource humanTrack = tracks[0];
        AudioSource robotTrack = tracks[1];

        float mutagePerSecond = 0.1f;
        float deltaVolume = mutagePerSecond * Time.deltaTime;

        if (toRobot && robotTrack.volume < 0.1f)
        {

            humanTrack.volume -= deltaVolume;
            robotTrack.volume += deltaVolume;
        }
        else if (humanTrack.volume < 0.1f)
        {
            humanTrack.volume += deltaVolume;
            robotTrack.volume -= deltaVolume;
        }

    }

    private void mute()
    {
        AudioSource[] tracks = GetComponentsInChildren<AudioSource>();
        AudioSource humanTrack = tracks[0];
        AudioSource robotTrack = tracks[1];

        if (muting)
        {
            humanTrack.volume = 0.1f;
            robotTrack.volume = 0.0f;
            muting = false;
        }
        else
        {
            humanTrack.volume = 0.0f;
            robotTrack.volume = 0.0f;
            muting = true;
        }



    }
}
