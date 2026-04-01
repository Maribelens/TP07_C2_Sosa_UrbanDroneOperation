using UnityEngine;

public class Patrol : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager)
    {
        base.Initialize(animator, fsmManager);
        stateType = StateType.Patrol;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 1);

        //if (npc.waypoints.Length == 0) return;

        //npc.agent.isStopped = false;
        //npc.agent.SetDestination(npc.waypoints[npc.currentWaypoint].position);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float dist = npc.DistanceToPlayer();

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

        //if (npc.waypoints.Length == 0) return;

        if (!npc.agent.pathPending && npc.agent.remainingDistance < 0.5f)
        {
            //npc.currentWaypoint = (npc.currentWaypoint + 1) % npc.waypoints.Length;
            //npc.agent.SetDestination(npc.waypoints[npc.currentWaypoint].position);
        }

    }
}
