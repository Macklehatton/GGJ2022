using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerMode mode;
    [SerializeField]
    private Animator humanAnimator;
    [SerializeField]
    private Animator robotAnimator;

    private float speed;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;

        if (!mode.RobotMode)
        {
            HandleHumanMode();
        }
        else
        {
            HandleRobotMode();
        }
    }

    private void HandleHumanMode()
    {
        if (speed > 0.1f)
        {
            humanAnimator.SetTrigger("humanWalk");
            robotAnimator.SetTrigger("humanWalk");
        }
        else
        {
            humanAnimator.SetTrigger("humanIdle");
            robotAnimator.SetTrigger("humanIdle");
        }
    }

    private void HandleRobotMode()
    {
        if (speed > 0.1f)
        {
            robotAnimator.SetTrigger("robotWalk");
            humanAnimator.SetTrigger("robotWalk");
        }
        else
        {
            robotAnimator.SetTrigger("robotIdle");
            humanAnimator.SetTrigger("robotIdle");
        }
    }
}
