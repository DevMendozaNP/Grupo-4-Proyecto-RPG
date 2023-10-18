using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoss
{
    public Func<bool> IsValid {private set; get;}
    public Func<StateBoss> GetNextState {private set; get;}

    public TransitionBoss(
        Func<bool> isValid,
        Func<StateBoss> getNextState
    ){
        IsValid = isValid;
        GetNextState = getNextState;
    }
}
