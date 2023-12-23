using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HadronStateManager : MonoBehaviour
{
    Hadron_PhotoInfo_BaseState currentState;

    public Test_HadronColliderCollision hadronSphere;

    //States
    public HadronPointOfInterest0 PointOfInterest0 = new HadronPointOfInterest0();
    public HadronPointOfInterest1 PointOfInterest1 = new HadronPointOfInterest1();
    public HadronPointOfInterest2 PointOfInterest2 = new HadronPointOfInterest2();
    public HadronPointOfInterest3 PointOfInterest3 = new HadronPointOfInterest3();

    void Start()
    {
        // starting state for the state machine
        currentState = PointOfInterest0;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(Hadron_PhotoInfo_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
