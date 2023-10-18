using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : StateBoss
{
    public BossIdleState(DragonController controller) : base(controller)
    {
        // Creamos nuestra transicion de Idle -> Follow
        TransitionBoss transitionIdleToFollow = new TransitionBoss(
            isValid: () => {
                float distance = Vector3.Distance(
                    controller.Player.position,
                    controller.transform.position
                );
                if (distance < controller.DistanceToFollow)
                {
                    return true;
                }else
                {
                    return false;    
                }
                
            },
            getNextState: () => {
                return new BossFollowState(controller);
            }
        );

        Transitions.Add(transitionIdleToFollow);
    }

    public override void OnStartBoss()
    {
       // Debug.Log("Estado Idle Start");
        controller.rb.velocity = Vector3.zero;
        controller.animator.SetFloat("Vertical", -1f);
    }

    public override void OnUpdateBoss()
    {
        //Debug.Log("Estado Idle: Update");
    }

    public override void OnFinishBoss()
    {
       // Debug.Log("Estado Idle Finish");
    }
}