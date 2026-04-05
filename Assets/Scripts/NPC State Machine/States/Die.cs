using UnityEngine;

public class Die : StateBase
{
    public override void Initialize(Animator animator, FsmManager fsmManager, NPC npc)
    {
        base.Initialize(animator, fsmManager, npc);
        stateType = StateType.Die;
        //npc.agent.isStopped = true;
        //npc.gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetInteger(State, 5);

        // Desactivar comportamiento
        Collider col = npc.GetComponentInChildren<Collider>();
        if (col != null) col.enabled = false;
        //npc.GetComponent<Collider>().enabled = false;

        // Opcional: detener movimiento
        npc.enabled = false;

        //Oopcional: destruir despues
        npc.gameObject.SetActive(false);
        //GameObject.Destroy(npc.gameObject, 3f);
    }
}
