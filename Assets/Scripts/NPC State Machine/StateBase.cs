using UnityEngine;
public abstract class StateBase
{
    public State stateType = State.None;

    protected Animator animator;
    protected FsmManager fsmManager;
    protected FsmManager fsm;

    public virtual void Initialize (Animator animator, FsmManager fsmManager)
    {
        this.animator = animator;
        this.fsmManager = fsmManager;
    }
    public virtual void OnEnter() 
    {
        Debug.Log($"OnEnter de {stateType}");
    }
    public virtual void OnUpdate() 
    {
        Debug.Log($"OnUpdate de {stateType}");
    }
    public virtual void OnExit() 
    {
        Debug.Log($"OnExit de {stateType}");
    }
}
