using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animator)
        {
            if (gameManager.GetComponent<GameManagerScript>().paused)
            {
                animator.updateMode = AnimatorUpdateMode.Normal;
                print("hello");
            }
            else
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            Vector3 newPosition = transform.parent.position;
            Quaternion newRotation = transform.parent.rotation;

            newPosition.y += animator.deltaPosition.y;
            newPosition.x += animator.deltaPosition.x;


            if (stateInfo.IsTag("IsDead"))
            {
                animator.ApplyBuiltinRootMotion();
            }
            else
                animator.applyRootMotion = false;
        }
    }
}
