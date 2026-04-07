using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private bool isLaserShowed = false;

    [SerializeField] private bool isPlayerWeapon = false;

    private float damage = 10f;
    private float range = 50f;

    private void Start()
    {
        if (laserLine != null)
            laserLine.enabled = false;
    }

    private void Update()
    {
        if (!isPlayerWeapon) return;

        if (isPlayerWeapon) // solo jugador
        {
            if (Input.GetKeyDown(KeyCode.L))
                isLaserShowed = !isLaserShowed;

            ShowLaser();

        }
    }

    private void ShowLaser()
    {
        if (isLaserShowed) laserLine.enabled = false;

        if (!isLaserShowed && laserLine != null && firePoint != null)
        {
            RaycastHit hit;
            Vector3 endPoint = firePoint.position + firePoint.forward * range;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
            {
                endPoint = hit.point;
            }

            laserLine.enabled = true;
            laserLine.SetPosition(0, firePoint.position);
            laserLine.SetPosition(1, endPoint);
        }

        else if (laserLine != null)
        {
            laserLine.enabled = false;
        }
    }

    public void Shoot()
    {
        if (firePoint == null) return;

        RaycastHit hit;
        Vector3 endPoint = firePoint.position + firePoint.forward * range;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            HealthSystem health = hit.transform.GetComponent<HealthSystem>();
            if (health != null) health.TakeDamage(damage);
        }
    }
}