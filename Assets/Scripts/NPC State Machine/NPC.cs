using System;
using UnityEngine;
public enum NPCType
{
    Civil,
    Enemy
}

public class NPC : MonoBehaviour
{
    public NPCType npcType;
    public Transform target;

    [Header("Hurt")]
    public float hurtDuration = 0.5f;
    [HideInInspector] public float hurtTimer;

    [Header("Movement")]
    //public Transform[] waypoints;
    //public int currentWaypoint = 0;
    public float speed = 2f;

    [Header("Ranges")]
    public float detectionRange = 10f;
    public float attackRange = 5f;

    [Header("References")]
    [SerializeField] private FsmManager fsm;
    public HealthSystem health;
    public Weapon weapon;

    [Header("Combat")]
    public float attackCooldown = 1.5f;
    public float lastAttackTime;

    private void Awake()
    {
        //health = GetComponent<HealthSystem>();
        //fsm = GetComponent<FsmManager>();
        //weapon = GetComponent<Weapon>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");

            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogError("No se encontró un objeto con tag 'Player'");
        }
    }

    private void OnEnable()
    {
        if (health == null) return;

        health.onTakeDamage += HandleTakeDamage;
        health.onDie += HandleDie;
    }

    private void OnDisable()
    {
        if (health == null) return;

        health.onTakeDamage -= HandleTakeDamage;
        health.onDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        if (fsm.currentState.stateType == StateType.Die) return;
        fsm.SwapStateTo(StateType.Hurt);
    }

    private void HandleDie()
    {
        fsm.SwapStateTo(StateType.Die);
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public void DoAttack()
    {
        if (!CanAttack()) return;
        lastAttackTime = Time.time;
    }
}