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
    [SerializeField]
    private Vector3 robotOffset;
    AudioSource humanTrack;
    AudioSource robotTrack;

    private bool muted = false;
    public bool RobotMode { get => robotMode; }

    public void Start()
    {
        AudioSource[] tracks = GetComponentsInChildren<AudioSource>();
        humanTrack = tracks[0];
        robotTrack = tracks[1];
    }

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
            muted = !muted;
            muteTrack();
        }

    }

    private void HandleModeSwitch()
    {
        CheckInputs();

        if (RobotMode)
        {
            playerGraphics.transform.parent = robot.transform;
            playerGraphics.transform.position = robot.transform.position + robotOffset;
            robot.SetActive(true);
            human.SetActive(false);
        }
        else
        {
            playerGraphics.transform.parent = human.transform;
            playerGraphics.transform.position = human.transform.position;
            robot.SetActive(false);
            human.SetActive(true);
        }

        if (muted)
        {
            return;
        }
        else
        {
            SwitchTrack(robotMode);
        }
    }

    private void SwitchTrack(bool toRobot)
    {
        Debug.Log("Mixing track: " + humanTrack.clip.name);

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

    private void muteTrack()
    {
        Debug.Log("Muting track: " + humanTrack.clip.name);

        if (muted)
        {
            humanTrack.volume = 0.0f;
            robotTrack.volume = 0.0f;
        }
        else
        {
            humanTrack.volume = 0.1f;
            robotTrack.volume = 0.0f;
        }

    }
}
