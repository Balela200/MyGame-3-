using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public static StaminaSystem staminaSystem;
    [Header("Settings")]
    public float Stamina = 100f;
    public float maxStamina = 100f;

    float timeStamina;

    [Header("UI")]
    public Image staminaBar;

    void Start()
    {
        staminaSystem =this;

        Stamina = maxStamina;
    }

    void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        if (staminaBar)
        {
            staminaBar.fillAmount = Stamina / maxStamina;
        }

        timeStamina += Time.deltaTime;
        if(timeStamina >= 0.2)
        {
            Stamina += 1;

            timeStamina = 0;
        }
    }

    
    public void StaminaLoss(float staminaLoss)
    {

        Stamina -= staminaLoss;
        Stamina = Mathf.Clamp(Stamina, 0, maxStamina);
    }
}
