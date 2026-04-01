using UnityEngine;

public class Die : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager)
    {
        base.Initialize(animator, fsmManager);
        stateType = StateType.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 5);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        //desaparecer o desactivar
    }
}
