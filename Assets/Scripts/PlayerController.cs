using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 5f;

    [Header("Rotacion")]
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("Combate")]
    public float attackCooldown = 1.5f;
    public float lastAttackTime;

    [Header("Referencias")]
    [SerializeField] private HealthSystem droneHealth;
    [SerializeField] private Weapon weapon;
    private Rigidbody rb;

    [Header("Daño")]
    [SerializeField] private float hitCooldown = 1f;
    private float lastHitTime;

    private Vector3 currentVelocity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
        droneHealth = GetComponent<HealthSystem>();
        droneHealth.onDie += OnDie;
    }

    private void Update()
    {
        HandleRotation();
        if (Input.GetMouseButtonDown(0))
        {
            DoAttack();
            weapon.Shoot();
        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void DoAttack()
    {
        if (!CanAttack()) return;
        lastAttackTime = Time.time;
        weapon.Shoot();

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float moveY = 0f;

        if (Input.GetKey(KeyCode.Space))
            moveY = 1f; //Subir
        if (Input.GetKey(KeyCode.LeftControl))
            moveY = -1f; //Bajar

        //Direccion relativa al dron
        Vector3 direction = transform.forward * moveZ +
                            transform.right * moveX +
                            transform.up * moveY;

        direction.Normalize();

        //Velocidad objetivo
        Vector3 targetVelocity = direction * moveSpeed;

        //Interpolacion para simular inercia
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        //Aplicacion al rigibbody
        rb.linearVelocity = currentVelocity;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yaw += mouseX; //rotacion horizontal
        pitch -= mouseY; //rotacion vertical

        //Limitar la rotacion vertical
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        //Aplicar rotacion
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        if (collision.gameObject.CompareTag("Blocks"))
        {
            float impactForce = collision.relativeVelocity.magnitude;

            if (impactForce > 2f)
            {
                float damage = impactForce * 2f;
                droneHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDie()
    {
        gameObject.SetActive(false);
    }
}

