using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Eventos de estado de vida e invulnerabilidad
    public event Action<float, float> onLifeUpdated;    // (vida actual, vida máxima)
    public event Action onTakeDamage;
    public event Action onDie;

    [Header("Vida")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    public bool isInvulnerable = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        onLifeUpdated?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0 || isInvulnerable) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDie?.Invoke();
        }
        else
        {
            onTakeDamage?.Invoke();
            //Debug.Log("EVENTO onTakeDamage lanzado");
            onLifeUpdated?.Invoke(currentHealth, maxHealth);
        }

        Debug.Log($"RECIBI DAÑO: {amount} en {gameObject.name}");
        //Debug.Log("DoDamage", gameObject);
    }
}
