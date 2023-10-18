using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowState : StateBoss
{
    public BossFollowState(DragonController controller) : base(controller)
    {
        // Transicion Follow -> Idle
        TransitionBoss transitionFollowToIdle = new TransitionBoss(
            isValid: () => {
                float distance = Vector3.Distance(
                    controller.Player.position,
                    controller.transform.position
                );
                if (distance >= controller.DistanceToFollow)
                {
                    return true;
                }else
                {
                    return false;    
                }
            },
            getNextState: () => {
                return new BossIdleState(controller);
            }
        );
        Transitions.Add(transitionFollowToIdle);

        // Transicion Follow -> Attack
        TransitionBoss transitionFollowToAttack = new TransitionBoss(
            isValid: () =>{
                float distance = Vector3.Distance(
                    controller.Player.position,
                    controller.transform.position
                );
                if (distance < controller.DistanceToAttack)
                {
                    return true;
                }else
                {
                    return false;
                }
            },
            getNextState: () => {
                return new BossAttackState(controller);
            }
        );
        Transitions.Add(transitionFollowToAttack);
    }


    public override void OnStartBoss()
    {
        Debug.Log("Estado Follow: Start");
    }

    public override void OnUpdateBoss()
    {
        //Debug.Log("Estado Follow: Update");
        Vector3 dir = (
            controller.Player.position - controller.transform.position
        ).normalized;
        controller.animator.SetFloat("Horizontal", dir.x);
        controller.animator.SetFloat("Vertical", dir.z);
        controller.rb.velocity = dir * controller.Speed;

    }
    public override void OnFinishBoss()
    {
        Debug.Log("Estado Follow: FInish");
    }
}
