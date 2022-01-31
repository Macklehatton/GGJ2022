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
    private AudioSource transformToRobotSound;
    private AudioSource transformToHumanSound;

    private bool muted = false;
    public bool RobotMode { get => robotMode; }

    public void Start()
    {
        AudioSource[] tracks = GetComponentsInChildren<AudioSource>();
        humanTrack = tracks[0];
        robotTrack = tracks[1];

        transformToRobotSound = gameObject.AddComponent<AudioSource>();
        transformToRobotSound.clip = Resources.Load("sfx/transform_to_robot") as AudioClip;
        transformToRobotSound.volume = 0.03f;

        transformToHumanSound = gameObject.AddComponent<AudioSource>();
        transformToHumanSound.clip = Resources.Load("sfx/transform_to_human") as AudioClip;
        transformToHumanSound.volume = 0.03f;
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
            playTransformSound(robotMode);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            robotMode = true;
            playTransformSound(robotMode);
        }
        else if ( Input.mouseScrollDelta.y != 0.0f ||
                  Input.GetKeyDown(KeyCode.LeftShift) ||
                  Input.GetKeyDown(KeyCode.F) ||
                  Input.GetKeyDown(KeyCode.Tab) )
        {
            robotMode = !RobotMode;
            playTransformSound(robotMode);
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

    private void playTransformSound(bool forRobot)
    {
        if (forRobot)
        {
            transformToRobotSound.Play();
            return;
        }
        transformToHumanSound.Play();
    }
}
