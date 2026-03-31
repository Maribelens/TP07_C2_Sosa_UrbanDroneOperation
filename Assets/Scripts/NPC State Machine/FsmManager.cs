using UnityEngine;
using System.Collections.Generic;

public class FsmManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private List<StateBase> states = new List<StateBase>();
    private StateBase currentState;
    private StateBase previousState;
    private NPC npc;

    private void Awake()
    {
        npc = GetComponent<NPC>();

        states.Add(new Idle());
        states.Add(new Patrol());
        states.Add(new Chase());
        states.Add(new Attack());
        states.Add(new Hurt());
        states.Add(new Die());

        foreach (StateBase state in states)
            state.Initialize(animator, this);

        currentState = FindState(State.Idle);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    //--------------------- GESTIÓN DE ESTADOS ---------------------
    public void SwapStateTo(State nextState)
    {
        foreach (StateBase stateBase in states)
        {
            if (stateBase.stateType == nextState)
            {
                currentState?.OnExit();
                previousState = currentState;
                currentState = stateBase;
                currentState.OnEnter();
                break;
            }
        }
    }

    public StateBase FindState (State stateToFind)
    {
        foreach (StateBase stateBase in states)
        {
            if (stateBase.stateType != stateToFind)
                return stateBase;
        }
        return null;
    }
}