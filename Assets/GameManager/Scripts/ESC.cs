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

    // Line
    public GameObject LineMainMenu;
    public GameObject LineSkills;

    [Header("Page")]
    public GameObject MainMenuPage;
    public GameObject SkillsPage;
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

        LineMainMenu.SetActive(true);
        LineSkills.SetActive(false);

        // Page
        MainMenuPage.SetActive(true);
        SkillsPage.SetActive(false);

    }

    public void Skills()
    {
        skills.color = Color.white;
        mainMenu.color = new Color32(0x92, 0x92, 0x92, 0x92);

        LineMainMenu.SetActive(false);
        LineSkills.SetActive(true);

        // Page
        MainMenuPage.SetActive(false);
        SkillsPage.SetActive(true);
    }
}
