using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NPCType { Civil, Enemy }
    public NPCType type;

    protected FsmManager fsm;
    //protected NPCHealth health;

    protected virtual void Start()
    {
        fsm = GetComponent<FsmManager>();
        //health = GetComponent<NPCHealth>();
    }
}