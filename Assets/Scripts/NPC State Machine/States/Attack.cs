using UnityEngine;

public class Attack : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Attack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 3);
    }
    public override void OnUpdate()
    {
        if (npc.target == null) return;
        float distance = npc.DistanceToPlayer();
        //Transiciones
        if (distance > npc.attackRange)
        {
            fsm.SwapStateTo(StateType.Chase);
            return;
        }
        //Mirar al jugador
        Vector3 dir = (npc.target.position - npc.transform.position).normalized;
        npc.transform.forward = dir;
        if (npc.CanAttack())
        {
            npc.DoAttack();
            //Debug.Log("NPC dispara!");
        }
    }
}
