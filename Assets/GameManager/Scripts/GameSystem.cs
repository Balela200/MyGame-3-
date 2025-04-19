using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public static GameSystem gameSystem;
    [Header("Level")]
    public static int playerLevel = 0;
    public static float progress = 0;
    public static float maxProgress = 100;
    public static int Skills = 10;

    
    [Header("HUD")]
    public Text levelText;
    public Text ProgressText;

    private float targetFill;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = this;
    }

    // Update is called once per frame
    void Update()
    {
        LevelProgressSystem();
    }

    public void TakeProgress(float TakeProgress)
    {
        progress += TakeProgress;
    }

    public void LevelProgressSystem()
    {
        if (playerLevel == 0 && progress >= maxProgress)
        {
            playerLevel = 1;
            progress = 0f;
            maxProgress = 100;
            Debug.Log("Level Player: 1");
        }
        else if (playerLevel == 1 && progress >= maxProgress)
        {
            playerLevel = 2;
            progress = 0f;
            maxProgress = 250;
            Debug.Log("Level Player: 2");
        }
        else if (playerLevel == 2 && progress >= maxProgress)
        {
            playerLevel = 3;
            progress = 0f;
            maxProgress = 450;
            Debug.Log("Level Player: 3");
        }
        else if (playerLevel == 3 && progress >= maxProgress)
        {
            playerLevel = 4;
            progress = 0f;
            maxProgress = 700;
            Debug.Log("Level Player: 4");
        }
        else if (playerLevel == 4 && progress >= maxProgress)
        {
            playerLevel = 5;
            progress = 0f;
            maxProgress = 1000;
            Debug.Log("Level Player: 5");
        }
        else if (playerLevel == 5 && progress >= maxProgress)
        {
            playerLevel = 6;
            progress = 0f;
            maxProgress = 1350;
            Debug.Log("Level Player: 6");
        }
        else if (playerLevel == 6 && progress >= maxProgress)
        {
            playerLevel = 7;
            progress = 0f;
            maxProgress = 1750;
            Debug.Log("Level Player: 7");
        }
        else if (playerLevel == 7 && progress >= maxProgress)
        {
            playerLevel = 8;
            progress = 0f;
            maxProgress = 2200;
            Debug.Log("Level Player: 8");
        }
        else if (playerLevel == 8 && progress >= maxProgress)
        {
            playerLevel = 9;
            progress = 0f;
            maxProgress = 2700;
            Debug.Log("Level Player: 9");
        }
        else if (playerLevel == 9 && progress >= maxProgress)
        {
            playerLevel = 10;
            progress = 0f;
            maxProgress = 0;
            Debug.Log("Level Player: 10");
        }

        levelText.text = playerLevel.ToString();
        
        targetFill = Mathf.Lerp(targetFill, progress, 5f * Time.deltaTime);
        ProgressText.text = Mathf.RoundToInt(targetFill).ToString() + " / " + maxProgress.ToString();
    }
}
