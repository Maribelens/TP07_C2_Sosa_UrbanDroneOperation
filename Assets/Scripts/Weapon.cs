using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private Transform firePoint;
    private float damage = 10f;
    private float range = 50f;

    public void Shoot()
    {
        if (firePoint == null)
        {
            //Debug.LogError("FirePoint no asignado");
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 0.5f);
            //Debug.Log("Disparo a: " + hit.transform.name);
            //Debug.Log(hit.transform.name);

            HealthSystem health = hit.transform.GetComponent<HealthSystem>();

            //if (health == null)
            //    health = hit.transform.GetComponentInParent<HealthSystem>();


            if (health != null)
            {
                health.TakeDamage(damage);
                //Debug.Log("Daño de disparo aplicado a" + health.gameObject.name);
            }
            else
            {
                //Debug.LogWarning("NO encontré HealthSystem ni en padres");
            }

            // DEBUG extra
            //Debug.Log("Padre: " + hit.transform.parent?.name);
        }
    }

}
