using UnityEngine;

public class Idle : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 0);
        //npc.agent.isStopped = true;
        //npc.agent.ResetPath();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        fsm.SwapStateTo(StateType.Patrol);
        return;
    }
}

