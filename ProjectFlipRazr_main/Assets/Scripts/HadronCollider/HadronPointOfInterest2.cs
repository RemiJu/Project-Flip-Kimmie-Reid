using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadronPointOfInterest2 : Hadron_PhotoInfo_BaseState
{
    public override void EnterState(HadronStateManager hadron)
    {
        //hadron.particle1.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest2;
        //hadron.particle2.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest2;

        hadron.gameObject.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest2;
    }
    public override void UpdateState(HadronStateManager hadron)
    {
        if (hadron.hadronSphere.triggerCount >= 9)
        {
            hadron.SwitchState(hadron.PointOfInterest3);
        }
    }
}
