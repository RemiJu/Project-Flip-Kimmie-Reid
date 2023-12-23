using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadronPointOfInterest3 : Hadron_PhotoInfo_BaseState
{
    public override void EnterState(HadronStateManager hadron)
    {
        //hadron.particle1.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest3;
        //hadron.particle2.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest3;

        hadron.gameObject.GetComponent<PhotoItem>().photoItem = PhotoInfo.PhotoItem.HadronPointOfInterest3;
    }
    public override void UpdateState(HadronStateManager hadron)
    {

    }
}
