using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : StateBoss
{
    private float timer = 0.0f;
    public BossAttackState(DragonController controller) : base(controller)
    {
        // Attack -> Follow
        TransitionBoss transitionAttackToFollow = new TransitionBoss(
            isValid : () => {
                float distance = Vector3.Distance(
                    controller.Player.position,
                    controller.transform.position
                );
                if (distance >= controller.DistanceToAttack)
                {
                    return true;
                }else
                {
                    return false;
                }
            },
            getNextState : () => {
                return new BossFollowState(controller);
            }
        );

        Transitions.Add(transitionAttackToFollow);
    }


    public override void OnStartBoss()
    {
       // Debug.Log("Estado Attack: Start");
    }

    public override void OnUpdateBoss()
    {
        timer += Time.deltaTime;
        if (timer > controller.CoolDownTime)
        {
            controller.Fire();
        }
    }
    public override void OnFinishBoss()
    {
       // Debug.Log("Estado Attack: Finish");
    }
}
