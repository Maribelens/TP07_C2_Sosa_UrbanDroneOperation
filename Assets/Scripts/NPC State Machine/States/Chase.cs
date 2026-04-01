using UnityEngine;

public class Chase : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager)
    {
        base.Initialize(animator, fsmManager);
        stateType = StateType.Chase;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 2);
        npc.agent.isStopped = false;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        float distance = npc.DistanceToPlayer();

        // Si está cerca, atacar
        if (distance <= npc.attackRange)
        {
            fsm.SwapStateTo(StateType.Attack);
            return;
        }

        // Si lo pierde, volver a patrulla
        if (distance > npc.detectionRange)
        {
            fsm.SwapStateTo(StateType.Patrol);
            return;
        }

        // Perseguir
        npc.agent.SetDestination(npc.player.position);

        if (!npc.agent.isOnNavMesh)
        {
            Debug.LogError("No está en NavMesh");
        }
    }
}

