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
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (npc.waypoints.Length == 0) return;

        //Transform target = npc.waypoints[npc.currentWaypoint];

        //npc.transform.position = Vector3.MoveTowards(
        //    npc.transform.position,
        //    npc.target.position,
        //    npc.speed * Time.deltaTime
        //);

        //if (Vector3.Distance(npc.transform.position, target.position) < 0.2f)
            //npc.currentWaypoint = (npc.currentWaypoint + 1) % npc.waypoints.Length;

        // Transiciones
        float dist = npc.DistanceToPlayer();
        //Debug.Log($"[Patrol]Distancia al player: {dist}");

        if (dist < npc.detectionRange)
            fsm.SwapStateTo(StateType.Chase);
        //Debug.Log("Drone detectado, cambiando a Chase");
    }
}
