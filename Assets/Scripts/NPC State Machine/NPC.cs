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
    public Transform target;

    [Header("Hurt")]
    public float hurtDuration = 0.5f;
    [HideInInspector] public float hurtTimer;

    [Header("Movement")]
    public float speed = 2f;

    [Header("Ranges")]
    public float detectionRange = 10f;
    public float attackRange = 5f;

    [Header("References")]
    [SerializeField] private FsmManager fsm;
    public HealthSystem health;
    [SerializeField] private Weapon weapon;
    public NavMeshAgent agent;
    [SerializeField] private UIScoreManager scoreUI;

    [Header("Combat")]
    public float attackCooldown = 1.5f;
    public float lastAttackTime;

    [Header("Waypoints")]
    public Transform waypointContainer;
    //public List<Transform> waypoints = new List<Transform>();
    public Transform[] waypoints;
    public int currentWaypoint;
    public float waypointTolerance = 0.2f;

    private void Awake()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogError("No se encontró un objeto con tag 'Player'");
        }

        agent.speed = speed;
        LoadWaypoints();

        if (waypoints.Length > 0)
        {
            currentWaypoint = 0; // o Random.Range(0, waypoints.Length) si querés variar
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    

    private void LoadWaypoints()
    {

        if (waypointContainer != null)
        {
            waypoints = new Transform[waypointContainer.childCount];
            for (int i = 0; i < waypointContainer.childCount; i++)
            {
                waypoints[i] = waypointContainer.GetChild(i);
            }
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

    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }

    public void PatrolMovement()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform targetWP = waypoints[currentWaypoint];
        Debug.Log("NPC va hacia: " + targetWP.name);

        if (!agent.hasPath || agent.remainingDistance < waypointTolerance)
        {
            agent.speed = speed;
            agent.SetDestination(targetWP.position);
        }

        if (!agent.pathPending && agent.remainingDistance <= waypointTolerance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    //if (waypoints == null || waypoints.Length == 0) return;

    //Transform targetWP = waypoints[currentWaypoint];

    //if (!agent.hasPath || agent.remainingDistance < waypointTolerance)
    //{
    //    agent.speed = speed;
    //    agent.SetDestination(targetWP.position);
    //}

    //if (!agent.pathPending && agent.remainingDistance <= waypointTolerance)
    //{
    //    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    //}


public void ChasePLayer()
    {
        if (target == null) return;
        agent.SetDestination(target.position);
    }

    private void HandleTakeDamage()
    {
        //if (fsm.currentState.stateType == StateType.Die) return;
        fsm.SwapStateTo(StateType.Hurt);
    }

    private void HandleDie()
    {
        fsm.SwapStateTo(StateType.Die);
        if (npcType == NPCType.Enemy)
            scoreUI.AddScore(+10); // recompensa
        else if (npcType == NPCType.Civil)
            scoreUI.AddScore(-5); // penalización
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
        if (npcType == NPCType.Civil) return; // civiles no atacan
        if (weapon == null) return;
        if (!CanAttack()) return;

        lastAttackTime = Time.time;
        weapon.Shoot();
    }
    }


