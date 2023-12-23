using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hadron_PhotoInfo_BaseState
{
    public abstract void EnterState(HadronStateManager hadron);
    public abstract void UpdateState(HadronStateManager hadron);

}