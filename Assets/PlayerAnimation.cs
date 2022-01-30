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

        ResetAllBools(robotAnimator);
        ResetAllBools(humanAnimator);

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
            humanAnimator.SetBool("humanWalk", true);
            robotAnimator.SetBool("humanWalk", true);
        }
        else
        {
            humanAnimator.SetBool("humanIdle", true);
            robotAnimator.SetBool("humanIdle", true);
        }
    }

    private void HandleRobotMode()
    {
        if (speed > 0.1f)
        {
            humanAnimator.SetBool("robotWalk", true);
            robotAnimator.SetBool("robotWalk", true);
        }
        else
        {
            humanAnimator.SetBool("robotIdle", true);
            robotAnimator.SetBool("robotIdle", true);
        }
    }

    private void ResetAllBools(Animator animator)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(param.name, false);
            }
        }
    }
}
