using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FlipPhone_BaseState
{
    public abstract void EnterState(FlipPhoneManager flipPhone);
    public abstract void UpdateState(FlipPhoneManager flipPhone);
    public abstract void ExitState(FlipPhoneManager flipPhone);

}
