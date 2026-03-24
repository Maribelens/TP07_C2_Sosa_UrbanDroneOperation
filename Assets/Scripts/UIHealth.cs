using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private DroneHealth lifeTarget;
    [SerializeField] private Image lifeBar;
    [SerializeField] private TMP_Text lifeText;

    private void Awake()
    {
        // Suscripcion a eventos del sistema de salud
        lifeTarget.onLifeUpdated += UpdateLifeBar;
        lifeTarget.onDie += EmptyLifeBar;
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes al destruir el objeto
        lifeTarget.onLifeUpdated -= UpdateLifeBar;
        lifeTarget.onDie -= EmptyLifeBar;
    }

    public void UpdateLifeBar(float current, float max)
    {
        // Actualiza la barra de vida seg�n el porcentaje restante
        float lerp = current / max;
        lifeBar.fillAmount = lerp;
        if (lifeText != null)
            lifeText.text = $"{current:0}/{max:0}";
        //Ejemplo: "80/100"
    }

    private void EmptyLifeBar()
    {
        // Vacia la barra al morir el jugador
        lifeBar.fillAmount = 0;
        lifeText.text = "0/100";
    }
}
