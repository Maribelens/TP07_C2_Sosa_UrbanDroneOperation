using UnityEngine;
using UnityEngine.AI;
public enum NPCType
{
    Civil,
    Enemy
}

public class NPC : MonoBehaviour
{
    public NPCType npcType;
    public Transform player;

    [Header("Movement")]
    //public Waypoint currentWaypoint;
    public NavMeshAgent agent;
    //public Transform[] waypoints;
    public int currentWaypoint = 0;

    [Header("Ranges")]
    public float detectionRange = 10f;
    public float attackRange = 5f;

    [Header("Combate")]
    public float attackCooldown = 1.5f;
    public float lastAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public void DoAttack()
    {
        lastAttackTime = Time.time;
        Debug.Log("NPC ataca!");

        // Ejemplo simple de daño
        // player.GetComponent<PlayerHealth>()?.TakeDamage(10);
    }
}