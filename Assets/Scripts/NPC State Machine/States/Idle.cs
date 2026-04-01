using UnityEngine;

public class Idle : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager)
    {
        base.Initialize(animator, fsmManager);
        stateType = StateType.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 0);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        float dist = npc.DistanceToPlayer();

        if (npc == null)
        {
            Debug.LogError("NPC es NULL");
            return;
        }

        if (npc.player == null)
        {
            Debug.LogError("Player es NULL");
            return;
        }

        if (dist < npc.attackRange)
        {
            fsm.SwapStateTo(StateType.Attack);
            return;
        }

        if (dist < npc.detectionRange)
        {
            fsm.SwapStateTo(StateType.Chase);
            return;
        }

        //Si no hay nada, patrulla
        fsm.SwapStateTo(StateType.Patrol);
    }
}
