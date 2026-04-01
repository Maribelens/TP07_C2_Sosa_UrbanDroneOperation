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

        if(npc.npcType == NPCType.Enemy)
        {
            states.Add(new Chase());
            states.Add(new Attack());
            states.Add(new Hurt());
            states.Add(new Die());
        }
        foreach (StateBase state in states) 
            state.Initialize(animator, this);
        
        currentState = FindState(StateType.Idle);
        currentState.OnEnter();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    //--------------------- GESTIÓN DE ESTADOS ---------------------
    public void SwapStateTo(StateType nextState)
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

    public StateBase FindState(StateType stateToFind)
    {
        foreach (StateBase stateBase in states)
        {
            if (stateBase.stateType == stateToFind)
                return stateBase;
        }
        return null;
    }
}
