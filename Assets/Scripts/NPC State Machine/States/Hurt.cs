using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Hurt : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Hurt;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 4);

        npc.hurtTimer = npc.hurtDuration;
        npc.health.isInvulnerable = true;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        npc.hurtTimer -= Time.deltaTime;
        npc.health.isInvulnerable = true;

        if (npc.hurtTimer <= 0)
        {
            if(npc.npcType == NPCType.Enemy)
            {
                // Volver a comportamiento normal
                float dist = npc.DistanceToPlayer();

                if (dist <= npc.attackRange)
                {
                    fsm.SwapStateTo(StateType.Attack);
                    return;
                }
                else if (dist <= npc.detectionRange)
                {
                    fsm.SwapStateTo(StateType.Chase);
                    return;
                }
                else
                {
                    fsm.SwapStateTo(StateType.Patrol);
                    return;
                }
            }
            else // Civil
            {
                fsm.SwapStateTo(StateType.Patrol);
                return;
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        npc.health.isInvulnerable = false;
    }
}
