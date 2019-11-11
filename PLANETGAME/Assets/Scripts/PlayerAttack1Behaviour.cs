using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1Behaviour : StateMachineBehaviour
{
    // Start is called before the first frame update

    int noOfClicks = 0;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        FirstPersonController firstpersoncontroller = player.GetComponent<FirstPersonController>();
        noOfClicks = firstpersoncontroller.noOfClicks;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("condition", 0);
        if(noOfClicks>= 2)
        {
            animator.SetInteger("condition", 3);
        }
    }
}
