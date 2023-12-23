using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HadronPointOfInterest1 : Hadron_PhotoInfo_BaseState
{
    public override void EnterState(HadronStateManager hadron)
    {
        //hadron.particle1.AddComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest1;
        //hadron.particle2.AddComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest1;

        hadron.gameObject.AddComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest1;
    }
    public override void UpdateState(HadronStateManager hadron)
    {
        if (hadron.hadronSphere.triggerCount >= 6)
        {
            hadron.SwitchState(hadron.PointOfInterest2);
        }
    }
}
