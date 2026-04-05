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
        npc.transform.position = Vector3.MoveTowards(
            npc.transform.position,
            npc.target.position,
            npc.speed * Time.deltaTime
        );
    }
}

