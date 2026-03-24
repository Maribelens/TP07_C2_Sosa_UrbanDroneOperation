using System;
using UnityEngine;

public class DroneHealth : MonoBehaviour
{
    // Eventos de estado de vida e invulnerabilidad
    public event Action<float, float> onLifeUpdated;    // (vida actual, vida máxima)
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
            onDie?.Invoke();
        }
        else
        {
            onLifeUpdated?.Invoke(currentHealth, maxHealth);
        }

        Debug.Log("DoDamage", gameObject);
    }
}
