using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillsManager : MonoBehaviour
{
    [Header("booling Skills")]
    public bool isStaminaSkills = false;
    public bool isAttackSkills = false;
    public bool isHealthSkills = false;
    public bool isTreatmentSkills = false;
    public bool isAttack2Skills = false;
    public bool isComboSkills = false;

    [Header("Image")]
    public Image Stamina;
    public Image Attack;
    public Image Health;
    public Image Treatment;
    public Image Attack2;
    public Image Combo;

    [Header("On Image")]
    public Sprite healthOn;
    public Sprite TreatmentOn;
    public Sprite Attack2On;
    public Sprite ComboOn;

    [Header("is Image")]
    public Sprite isStaminaSprite;
    public Sprite isAttackSprite;
    public Sprite isHealthSprite;
    public Sprite isTreatmentSprite;
    public Sprite isComboSprite;

    [Header("Line")]
    public Image AttackOrStaminaToHealth;
    public Image StaminaToHealth;
    public Image AttackToHealth;
    public Image HealthToTreatment;
    public Image TreatmentToAttack2;
    public Image HealthToCombo1;
    public Image HealthToCombo2;
    public Image ComboToAttack1;
    public Image ComboToAttack2;

    [Header("Timer")]
    public Image FillStaminaImage;
    public Image FillAttackImage;
    public Image FillHealthImage;
    public Image FillTreatmentImage;
    public Image FillAttack2Image;
    public Image FillComboImage;

    float timeStamina;
    float timeAttack;
    float timeHealth;
    float timeTreatment;
    float timeAttack2;
    float timeCombo;

    float timerStamina;
    float timerAttack;
    float timerHealth;
    float timerTreatment;
    float timerAttack2;
    float timerCombo;

    // For holding the button state (holding or not)
    private bool isHoldingStamina = false;
    private bool isHoldingAttack = false;
    private bool isHoldingHealth = false;
    private bool isHoldingTreatment = false;
    private bool isHoldingAttack2 = false;
    private bool isHoldingCombo = false;

    void Start()
    {

    }

    void Update()
    {
        LineSystem();
        ImageSystem();
        SkillsUpdate();
        UpdateTimer();

        timerStamina = timeStamina / 0.5f;
        FillStaminaImage.fillAmount = Mathf.Lerp(FillStaminaImage.fillAmount, timerStamina, 20 * Time.deltaTime);

        timerAttack = timeAttack / 0.5f;
        FillAttackImage.fillAmount = Mathf.Lerp(FillAttackImage.fillAmount, timerAttack, 20 * Time.deltaTime);

        timerHealth = timeHealth / 0.5f;
        FillHealthImage.fillAmount = Mathf.Lerp(FillHealthImage.fillAmount, timerHealth, 20 * Time.deltaTime);

        timerTreatment = timeTreatment / 0.5f;
        FillTreatmentImage.fillAmount = Mathf.Lerp(FillTreatmentImage.fillAmount, timerTreatment, 20 * Time.deltaTime);

        timerAttack2 = timeAttack2 / 0.5f;
        FillAttack2Image.fillAmount = Mathf.Lerp(FillAttack2Image.fillAmount, timerAttack2, 20 * Time.deltaTime);

        timerCombo = timeCombo / 0.5f;
        FillComboImage.fillAmount = Mathf.Lerp(FillComboImage.fillAmount, timerCombo, 20 * Time.deltaTime);

        // Handle button hold logic
        if (isHoldingStamina && !isStaminaSkills) timeStamina += Time.deltaTime;
        if (isHoldingAttack && !isAttackSkills) timeAttack += Time.deltaTime;
        if (isHoldingHealth && !isHealthSkills) timeHealth += Time.deltaTime;
        if (isHoldingTreatment && !isTreatmentSkills) timeTreatment += Time.deltaTime;
        if (isHoldingAttack2 && !isAttack2Skills) timeAttack2 += Time.deltaTime;
        if (isHoldingCombo && !isComboSkills) timeCombo += Time.deltaTime;
    }

    void ImageSystem()
    {
        if (isStaminaSkills)
        {
            Stamina.sprite = isStaminaSprite;

            if (!isHealthSkills)
                Health.sprite = healthOn;
        }

        if (isAttackSkills)
        {
            Attack.sprite = isAttackSprite;

            if (!isHealthSkills)
                Health.sprite = healthOn;
        }

        if (isHealthSkills)
        {
            Health.sprite = isHealthSprite;

            if (!isTreatmentSkills)
                Treatment.sprite = TreatmentOn;

            if (!isComboSkills)
                Combo.sprite = ComboOn;
        }

        if (isTreatmentSkills)
        {
            Treatment.sprite = isTreatmentSprite;

            if (!isAttack2Skills)
                Attack2.sprite = Attack2On;
        }

        if (isAttack2Skills)
        {
            Attack2.sprite = isAttackSprite;
        }

        if (isComboSkills)
        {
            Combo.sprite = isComboSprite;

            if (!isAttack2Skills)
                Attack2.sprite = Attack2On;
        }
    }

    void LineSystem()
    {
        if (isStaminaSkills)
        {
            StaminaToHealth.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }

        if (isAttackSkills)
        {
            AttackToHealth.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }

        if (isAttackSkills || isStaminaSkills)
        {
            AttackOrStaminaToHealth.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }

        if (isHealthSkills)
        {
            HealthToTreatment.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
            HealthToCombo1.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
            HealthToCombo2.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }

        if (isTreatmentSkills)
        {
            TreatmentToAttack2.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }

        if (isComboSkills)
        {
            ComboToAttack1.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
            ComboToAttack2.color = new Color32(0xFF, 0xC4, 0x00, 0xFF);
        }
    }

    void SkillsUpdate()
    {
        if (isStaminaSkills)
        {
            StaminaSystem.staminaSystem.maxStamina = 200;
            StaminaSystem.staminaSystem.StaminaHeal = 3;
        }
    }

    void UpdateTimer()
    {
        if((timeStamina < 0) || timeAttack < 0 || timeHealth < 0 || timeTreatment < 0 || timeAttack2 < 0 || timeCombo < 0)
        {
            timeStamina = 0;
            timeAttack = 0;
            timeHealth = 0;
            timeTreatment = 0;
            timerAttack2 = 0;
            timerCombo = 0;
        }
    }

    // Button events for holding the buttons down
    public void OnStaminaButtonDown() => isHoldingStamina = true;
    public void OnStaminaButtonUp() => isHoldingStamina = false;

    public void OnAttackButtonDown() => isHoldingAttack = true;
    public void OnAttackButtonUp() => isHoldingAttack = false;

    public void OnHealthButtonDown() => isHoldingHealth = true;
    public void OnHealthButtonUp() => isHoldingHealth = false;

    public void OnTreatmentButtonDown() => isHoldingTreatment = true;
    public void OnTreatmentButtonUp() => isHoldingTreatment = false;

    public void OnAttack2ButtonDown() => isHoldingAttack2 = true;
    public void OnAttack2ButtonUp() => isHoldingAttack2 = false;

    public void OnComboButtonDown() => isHoldingCombo = true;
    public void OnComboButtonUp() => isHoldingCombo = false;

    // Button press methods
    public void ButtonStamina()
    {
        if (!isStaminaSkills && GameSystem.Skills >= 1)
        {
            timeStamina += Time.deltaTime;
            if (timeStamina >= 0.5f)
            {
                isStaminaSkills = true;
                timeStamina = 0;

                GameSystem.Skills -= 1;
            }
        }
    }

    public void ButtonAttack()
    {
        if (!isAttackSkills && GameSystem.Skills >= 1)
        {
            timeAttack += Time.deltaTime;
            if (timeAttack >= 0.5f)
            {
                isAttackSkills = true;
                timeAttack = 0;

                GameSystem.Skills -= 1;
            }
        }
    }

    public void ButtonHealth()
    {
        if (!isHealthSkills && GameSystem.Skills >= 2)
        {
            timeHealth += Time.deltaTime;
            if (timeHealth >= 0.5f)
            {
                isHealthSkills = true;
                timeHealth = 0;

                GameSystem.Skills -= 2;
            }
        }
    }

    public void ButtonTreatment()
    {
        if (!isTreatmentSkills && GameSystem.Skills >= 2)
        {
            timeTreatment += Time.deltaTime;
            if (timeTreatment >= 0.5f)
            {
                isTreatmentSkills = true;
                timeTreatment = 0;

                GameSystem.Skills -= 2;
            }
        }
    }

    public void ButtonAttack2()
    {
        if (!isAttack2Skills && GameSystem.Skills >= 2)
        {
            timeAttack2 += Time.deltaTime;
            if (timeAttack2 >= 0.5f)
            {
                isAttack2Skills = true;
                timeAttack2 = 0;

                GameSystem.Skills -= 2;
            }
        }
    }

    public void ButtonCombo()
    {
        if (!isComboSkills && GameSystem.Skills >= 3)
        {
            timeCombo += Time.deltaTime;
            if (timeCombo >= 0.5f)
            {
                isComboSkills = true;
                timeCombo = 0;

                GameSystem.Skills -= 3;
            }
        }
    }
}
