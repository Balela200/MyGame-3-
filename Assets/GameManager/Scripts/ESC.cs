using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ESC : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //929292
    public void MainMenu()
    {
        mainMenu.color = Color.white;
        skills.color = new Color32(0x92, 0x92, 0x92, 0x92);
        profile.color = new Color32(0x92, 0x92, 0x92, 0x92);

        // Line
        LineMainMenu.SetActive(true);
        LineSkills.SetActive(false);
        LineProfile.SetActive(false);

        // Page
        MainMenuPage.SetActive(true);
        SkillsPage.SetActive(false);
        ProfilePage.SetActive(false);

    }

    public void Skills()
    {
        skills.color = Color.white;
        mainMenu.color = new Color32(0x92, 0x92, 0x92, 0x92);
        profile.color = new Color32(0x92, 0x92, 0x92, 0x92);

        // Line
        LineMainMenu.SetActive(false);
        LineSkills.SetActive(true);
        LineProfile.SetActive(false);

        // Page
        MainMenuPage.SetActive(false);
        SkillsPage.SetActive(true);
        ProfilePage.SetActive(false);
    }

    public void Profile()
    {
        profile.color = Color.white;
        mainMenu.color = new Color32(0x92, 0x92, 0x92, 0x92);
        skills.color = new Color32(0x92, 0x92, 0x92, 0x92);

        // Line
        LineMainMenu.SetActive(false);
        LineSkills.SetActive(false);
        LineProfile.SetActive(true);

        // Page
        MainMenuPage.SetActive(false);
        SkillsPage.SetActive(false);
        ProfilePage.SetActive(true);
    }
}
