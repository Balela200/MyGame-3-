using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static SaveGame saveGame;
    // Start is called before the first frame update
    void Start()
    {
        saveGame = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSystemGame()
    {
        // progress
        PlayerPrefs.SetInt("isAttackSkills", SkillsManager.isAttackSkills ? 1 : 0);
        PlayerPrefs.SetInt("isAttack2Skills", SkillsManager.isAttack2Skills ? 1 : 0);
        PlayerPrefs.SetInt("isComboSkills", SkillsManager.isComboSkills ? 1 : 0);
        PlayerPrefs.SetInt("isHealthSkills", SkillsManager.isHealthSkills ? 1 : 0);
        PlayerPrefs.SetInt("isStaminaSkills", SkillsManager.isStaminaSkills ? 1 : 0);
        PlayerPrefs.SetInt("isTreatmentSkills", SkillsManager.isTreatmentSkills ? 1 : 0);

        PlayerPrefs.SetInt("Skills", GameSystem.Skills);

        PlayerPrefs.SetInt("isCamp1", CampManager.isCamp1 ? 1 : 0);
        PlayerPrefs.SetInt("isCamp2", CampManager.isCamp2 ? 1 : 0);

        PlayerPrefs.SetInt("kills", Profile.kills);
        PlayerPrefs.SetInt("deaths", Profile.deaths);

        PlayerPrefs.SetInt("playerLevel", GameSystem.playerLevel);
        PlayerPrefs.SetFloat("progress", GameSystem.progress);

        Debug.Log("Data has been saved!");
        PlayerPrefs.Save();
    }

    public void Load()
    {
        SkillsManager.isAttackSkills = PlayerPrefs.GetInt("isAttackSkills") == 1;
        SkillsManager.isAttack2Skills = PlayerPrefs.GetInt("isAttack2Skills") == 1;
        SkillsManager.isComboSkills = PlayerPrefs.GetInt("isComboSkills") == 1;
        SkillsManager.isHealthSkills = PlayerPrefs.GetInt("isHealthSkills") == 1;
        SkillsManager.isStaminaSkills = PlayerPrefs.GetInt("isStaminaSkills") == 1;
        SkillsManager.isTreatmentSkills = PlayerPrefs.GetInt("isTreatmentSkills") == 1;

        GameSystem.Skills = PlayerPrefs.GetInt("Skills");

        CampManager.isCamp1 = PlayerPrefs.GetInt("isCamp1") == 1;
        CampManager.isCamp2 = PlayerPrefs.GetInt("isCamp2") == 1;

        Profile.kills = PlayerPrefs.GetInt("kills");
        Profile.deaths = PlayerPrefs.GetInt("deaths");

        GameSystem.playerLevel = PlayerPrefs.GetInt("playerLevel");
        GameSystem.progress = PlayerPrefs.GetFloat("progress");
    }
}
