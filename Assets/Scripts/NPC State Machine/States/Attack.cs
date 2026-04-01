using UnityEngine;

public class Attack : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager)
    {
        base.Initialize(animator, fsmManager);
        stateType = StateType.Attack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 3);
        npc.agent.isStopped = true;
    }

    public override void OnUpdate()
    {
        float distance = npc.DistanceToPlayer();

        // Si se aleja, volver a perseguir
        if (distance > npc.attackRange)
        {
            npc.agent.isStopped = false;
            fsm.SwapStateTo(StateType.Chase);
            return;
        }

        Vector3 dir = (npc.player.position - npc.transform.position).normalized;
        dir.y = 0;
        npc.transform.forward = dir;

        if (npc.CanAttack())
        {
            npc.DoAttack();
        }
    }
}

