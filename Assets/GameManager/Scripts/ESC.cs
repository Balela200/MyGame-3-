using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ESC : MonoBehaviour
{
    public static ESC eSC;
    [Header("System Up")]
    public Text mainMenu;
    public Text skills;
    public Text profile;

    // Line
    public GameObject LineMainMenu;
    public GameObject LineSkills;
    public GameObject LineProfile;

    [Header("Page")]
    public GameObject MainMenuPage;
    public GameObject SkillsPage;
    public GameObject ProfilePage;
    // Start is called before the first frame update
    void Start()
    {
        eSC = this;
    }

    // Update is called once per frame
    void Update()
    {
        SystemESC();
    }

    void SystemESC()
    {
        bool isActiveMainMenu = MainMenuPage.activeSelf;
        bool isActiveSkills = SkillsPage.activeSelf;
        bool isActiveProfile = ProfilePage.activeSelf;

        if (isActiveMainMenu)
        {
            mainMenu.color = Color.white;
            skills.color = new Color32(0x92, 0x92, 0x92, 0x92);
            profile.color = new Color32(0x92, 0x92, 0x92, 0x92);

            // Line
            LineMainMenu.SetActive(true);
            LineSkills.SetActive(false);
            LineProfile.SetActive(false);
        }

        if (isActiveSkills)
        {
            skills.color = Color.white;
            mainMenu.color = new Color32(0x92, 0x92, 0x92, 0x92);
            profile.color = new Color32(0x92, 0x92, 0x92, 0x92);

            // Line
            LineMainMenu.SetActive(false);
            LineSkills.SetActive(true);
            LineProfile.SetActive(false);
        }

        if (isActiveProfile)
        {
            profile.color = Color.white;
            mainMenu.color = new Color32(0x92, 0x92, 0x92, 0x92);
            skills.color = new Color32(0x92, 0x92, 0x92, 0x92);

            // Line
            LineMainMenu.SetActive(false);
            LineSkills.SetActive(false);
            LineProfile.SetActive(true);
        }
    }

    //929292
    public void MainMenu()
    {
        // Page
        MainMenuPage.SetActive(true);
        SkillsPage.SetActive(false);
        ProfilePage.SetActive(false);
    }

    public void Skills()
    {
        // Page
        MainMenuPage.SetActive(false);
        SkillsPage.SetActive(true);
        ProfilePage.SetActive(false);
    }

    public void Profile()
    {
        // Page
        MainMenuPage.SetActive(false);
        SkillsPage.SetActive(false);
        ProfilePage.SetActive(true);
    }

    public void Resume()
    {
        PlayerControllor.playerControllor.ESCPage();
        MainMenuPage.SetActive(true);
        SkillsPage.SetActive(false);
        ProfilePage.SetActive(false);
    }
    public void Settings()
    {

    }

    public void SaveGame()
    {

    }

    public void Help()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
