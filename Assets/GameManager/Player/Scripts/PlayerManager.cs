using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Health Player")]
    TMP_Text healthTextComponent;
    GameObject healthTextGO;

    GameObject healthBarImage;
    Image healthBar;

    private float targetFill;

    void Start()
    {
        // Find Health Bar
        healthBarImage = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = healthBarImage.GetComponent<Image>();

        // Find Health Text
        healthTextGO = GameObject.FindGameObjectWithTag("HealthText");
        healthTextComponent = healthTextGO.GetComponent<TMP_Text>();
    }

    void Update()
    {
        float currentHealth = HealthPlayer.healthPlayerStatic.healthPlayer;
        float maxHealth = HealthPlayer.healthPlayerStatic.healthMaxPlayer;

        // Bar Health
        targetFill = currentHealth / maxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFill, 20 * Time.deltaTime);
        healthTextComponent.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
