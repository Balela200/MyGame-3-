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

    public void Save()
    {
        // progress
        PlayerPrefs.SetInt("isAttackSkills", SkillsManager.sillsManager.isAttackSkills ? 1 : 0);
        PlayerPrefs.SetInt("isAttack2Skills", SkillsManager.sillsManager.isAttack2Skills ? 1 : 0);
        PlayerPrefs.SetInt("isComboSkills", SkillsManager.sillsManager.isComboSkills ? 1 : 0);
        PlayerPrefs.SetInt("isHealthSkills", SkillsManager.sillsManager.isHealthSkills ? 1 : 0);
        PlayerPrefs.SetInt("isStaminaSkills", SkillsManager.sillsManager.isStaminaSkills ? 1 : 0);
        PlayerPrefs.SetInt("isTreatmentSkills", SkillsManager.sillsManager.isTreatmentSkills ? 1 : 0);

        PlayerPrefs.SetInt("Skills", GameSystem.Skills);

        PlayerPrefs.SetInt("isCamp1", CampManager.isCamp1 ? 1 : 0);
        PlayerPrefs.SetInt("isCamp2", CampManager.isCamp2 ? 1 : 0);

        PlayerPrefs.SetInt("kills", Profile.kills);
        PlayerPrefs.SetInt("deaths", Profile.deaths);

        PlayerPrefs.SetInt("playerLevel", GameSystem.playerLevel);
        PlayerPrefs.SetFloat("progress", GameSystem.progress);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        SkillsManager.sillsManager.isAttackSkills = PlayerPrefs.GetInt("isAttackSkills") == 1;
        SkillsManager.sillsManager.isAttack2Skills = PlayerPrefs.GetInt("isAttack2Skills") == 1;
        SkillsManager.sillsManager.isComboSkills = PlayerPrefs.GetInt("isComboSkills") == 1;
        SkillsManager.sillsManager.isHealthSkills = PlayerPrefs.GetInt("isHealthSkills") == 1;
        SkillsManager.sillsManager.isStaminaSkills = PlayerPrefs.GetInt("isStaminaSkills") == 1;
        SkillsManager.sillsManager.isTreatmentSkills = PlayerPrefs.GetInt("isTreatmentSkills") == 1;

        GameSystem.Skills = PlayerPrefs.GetInt("Skills");

        CampManager.isCamp1 = PlayerPrefs.GetInt("isCamp1") == 1;
        CampManager.isCamp2 = PlayerPrefs.GetInt("isCamp2") == 1;

        Profile.kills = PlayerPrefs.GetInt("kills");
        Profile.deaths = PlayerPrefs.GetInt("deaths");

        GameSystem.playerLevel = PlayerPrefs.GetInt("playerLevel");
        GameSystem.progress = PlayerPrefs.GetFloat("progress");
    }
}
