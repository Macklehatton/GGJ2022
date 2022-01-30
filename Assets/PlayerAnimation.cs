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

        //ResetAllTriggers(robotAnimator);
        //ResetAllTriggers(humanAnimator);

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
            if (!humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
            {
                ResetAllTriggers(humanAnimator);
                humanAnimator.SetTrigger("humanWalk");
            }

            if (!robotAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_HumanWalk"))
            {
                ResetAllTriggers(robotAnimator);
                robotAnimator.SetTrigger("humanWalk");
            }
        }
        else
        {
            if (!humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            {
                ResetAllTriggers(humanAnimator);
                humanAnimator.SetTrigger("humanIdle");
            }

            if (!humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_HumanIdle"))
            {
                ResetAllTriggers(robotAnimator);
                robotAnimator.SetTrigger("humanIdle");
            }
        }
    }

    private void HandleRobotMode()
    {
        if (speed > 0.1f)
        {
            if (!humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("HumanDangle"))
            {
                ResetAllTriggers(humanAnimator);
                humanAnimator.SetTrigger("robotWalk");
            }

            if (!robotAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_RobotWalk"))
            {
                ResetAllTriggers(robotAnimator);
                robotAnimator.SetTrigger("robotWalk");
            }

            //robotAnimator.SetTrigger("robotWalk");
            //humanAnimator.SetTrigger("robotWalk");
        }
        else
        {
            if (!humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("HumanDangleIdle"))
            {
                ResetAllTriggers(humanAnimator);
                humanAnimator.SetTrigger("robotIdle");
            }

            if (!robotAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_RobotIdle"))
            {
                ResetAllTriggers(robotAnimator);
                robotAnimator.SetTrigger("robotIdle");
            }

            //robotAnimator.SetTrigger("robotIdle");
            //humanAnimator.SetTrigger("robotIdle");
        }
    }

    private void ResetAllTriggers(Animator animator)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }
}
