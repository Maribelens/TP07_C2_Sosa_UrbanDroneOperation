using UnityEngine;

public class Die : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 5);

        // Desactivar comportamiento
        Collider col = npc.GetComponentInChildren<Collider>();
        if (col != null) col.enabled = false;

        // Opcional: detener movimiento
        npc.agent.isStopped = true;
        //npc.enabled = false;

        //Oopcional: destruir despues
        GameObject.Destroy(npc.gameObject, 3f);
        //npc.gameObject.SetActive(false);
    }
}
