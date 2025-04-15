using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    public static StaminaSystem staminaSystem;
    [Header("Settings")]
    public float Stamina = 100f;
    public float maxStamina = 100f;
    private float targetFill;

    float timeStamina;

    [Header("UI")]
    public Image staminaBar;
    public TMP_Text staminaText;

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
            targetFill = Stamina / maxStamina;
            staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, targetFill, 5 * Time.deltaTime);
        }

        staminaText.text = Stamina.ToString() + " / " + maxStamina.ToString();

        timeStamina += Time.deltaTime;
        if(timeStamina >= 0.3 && Stamina < 100)
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
