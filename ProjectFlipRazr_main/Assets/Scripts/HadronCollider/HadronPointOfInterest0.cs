using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadronPointOfInterest0 : Hadron_PhotoInfo_BaseState
{
    public override void EnterState(HadronStateManager hadron)
    {

    }
    public override void UpdateState(HadronStateManager hadron)
    {
        if (hadron.hadronSphere.triggerCount >= 3)
        {
            hadron.SwitchState(hadron.PointOfInterest1);
        }
    }
}
