using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBoss
{
    protected DragonController controller;
    public List<TransitionBoss> Transitions;

    public StateBoss(DragonController controller)
    {
        this.controller = controller;
        Transitions = new List<TransitionBoss>();
    }

    public abstract void OnStartBoss();
    public abstract void OnUpdateBoss();
    public abstract void OnFinishBoss();

}
