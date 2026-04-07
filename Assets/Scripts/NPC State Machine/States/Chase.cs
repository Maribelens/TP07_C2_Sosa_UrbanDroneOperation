using UnityEngine;

public class Chase : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Chase;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 2);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        npc.ChasePLayer();

        float distance = npc.DistanceToPlayer();
        // Si está cerca, atacar
        if (distance <= npc.attackRange)
        {
            fsm.SwapStateTo(StateType.Attack);
            return;
        }
        // Si lo pierde, volver a patrulla
        if (distance > npc.detectionRange * 1.5f)
        {
            fsm.SwapStateTo(StateType.Patrol);
            return;
        }
    }
}