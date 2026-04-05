using UnityEngine;
using System.Collections.Generic;
public class FsmManager : MonoBehaviour
{
    [SerializeField] private NPC npc;
    //[SerializeField] private HealthSystem health;
    [SerializeField] private Animator animator;
    private List<StateBase> states = new List<StateBase>();
    public StateBase currentState;
    private StateBase previousState;

    private void Awake()
    {
        //npc = GetComponent<NPC>();
        //npc.health = GetComponent<HealthSystem>();

        //if (npc.health != null)
        //{
        //    npc.health.onTakeDamage += HandleTakeDamage;
        //    npc.health.onDie += HandleDie;
        //}

        states.Add(new Idle());
        states.Add(new Patrol());
        states.Add(new Hurt());
        states.Add(new Die());

        if(npc.npcType == NPCType.Enemy)
        {
            states.Add(new Chase());
            states.Add(new Attack());
        }
        foreach (StateBase state in states)
        {
            state.Initialize(animator, this, npc);
        }
        
        currentState = FindState(StateType.Idle);
        currentState.OnEnter();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    //private void OnDestroy()
    //{
    //    if (npc.health != null)
    //    {
    //        npc.health.onTakeDamage -= HandleTakeDamage;
    //        npc.health.onDie -= HandleDie;
    //    }
    //}

    //--------------------- GESTIÓN DE ESTADOS ---------------------

    public void SwapStateTo(StateType nextState)
    {
        if (currentState != null && currentState.stateType == StateType.Die) return;

        foreach (StateBase stateBase in states)
        {
            if (stateBase.stateType == nextState)
            {
                Debug.Log($"CAMBIO DE ESTADO: {currentState?.stateType} A {nextState}");

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
