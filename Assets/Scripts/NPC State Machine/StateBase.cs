using UnityEngine;
public abstract class StateBase
{
    protected static readonly int State = Animator.StringToHash("State");

    protected Animator animator;
    protected FsmManager fsm;
    protected NPC npc;

    public StateType stateType = StateType.None;

    public virtual void Initialize (Animator animator, FsmManager fsmManager)
    {
        this.animator = animator;
        this.fsm = fsmManager;
        this.npc = fsmManager.GetComponent<NPC>();
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
