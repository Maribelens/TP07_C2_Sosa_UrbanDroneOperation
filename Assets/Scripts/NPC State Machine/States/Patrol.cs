using System.Xml.Linq;
using UnityEngine;

public class Patrol : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Patrol;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 1);
        //Debug.Log($"NPC{npc.name} va hacia: " + npc.waypoints[npc.currentWaypoint].name);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (npc.npcType == NPCType.Civil)
        {
            npc.PatrolMovement();
            // Transiciones
            float dist = npc.DistanceToPlayer();
            //Debug.Log($"[Patrol]Distancia al player: {dist}");
            if (dist < npc.detectionRange)
            {
                fsm.SwapStateTo(StateType.Idle);
                return;
            }
        }

        if (npc.npcType == NPCType.Enemy)
        {
            npc.PatrolMovement();
            if (npc.DistanceToPlayer() < npc.detectionRange)
            {
                fsm.SwapStateTo(StateType.Chase);
            }
        }

    }
}
