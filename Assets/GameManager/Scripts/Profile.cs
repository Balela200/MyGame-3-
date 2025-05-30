using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    public static Profile profile;

    [Header("Text Attributes")]
    public TMP_Text textHP;
    public TMP_Text textSTM;
    public TMP_Text textATK;

    [Header("Text StatsPage")]
    public TMP_Text textKD;
    public TMP_Text textWins;
    public TMP_Text textScore;
    public TMP_Text textKills;
    public TMP_Text textDeaths;


    public TMP_Text textLevel;

    [Header("Bar")]
    public Image levelBar;

    [Header("System")]
    public static int kills = 0;
    public static int deaths = 0;

    HealthPlayer foundPlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        profile = this;

        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            foundPlayerHealth = foundPlayer.GetComponent<HealthPlayer>();
        }
        else
        {
            Debug.Log("No Player Found in Start");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            foundPlayerHealth = foundPlayer.GetComponent<HealthPlayer>();
        }
        else
        {
            Debug.Log("No Player Found in Start");
        }

        Attributes();
        StatsPage();
    }

    void Attributes()
    {
        if (foundPlayerHealth != null)
            textHP.text = foundPlayerHealth.healthPlayer.ToString();

        if (StaminaSystem.staminaSystem != null)
            textSTM.text = StaminaSystem.staminaSystem.Stamina.ToString();

            textATK.text = GameSystem.attack.ToString();
    }

    void StatsPage()
    {
        float kdRatio = deaths > 0 ? kills / deaths : kills;

        textKD.text = kdRatio.ToString("F2"); 
        textKills.text = kills.ToString();

        textDeaths.text = deaths.ToString();

        levelBar.fillAmount = GameSystem.progress / GameSystem.maxProgress;

        textLevel.text = GameSystem.progress.ToString() + " / " + GameSystem.maxProgress.ToString() + " - " + "Low rank, low skill. Are you that weak?";

    }
}
