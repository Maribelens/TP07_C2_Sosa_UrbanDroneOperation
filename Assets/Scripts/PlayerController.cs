using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 5f;

    [Header("Rotacion")]
    [SerializeField] private float mouseSensitivity = 10f;

    [Header("Referencias")]
    private Rigidbody rb;

    private Vector3 currentVelocity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleRotation();
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
}
