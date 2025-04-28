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

    public Animator animLevel;

    [Header("System Game")]
    public static float attack = 15;
    public bool heal = false;
    public bool TakeHealPlayer = true;

    private bool isHealingNow = false;
    private float timeHeal;

    [Header("HUD")]
    public Text levelText;
    public Text ProgressText;

    private float targetFill;

    // Start is called before the first frame update
    void Start()
    {
        gameSystem = this;

        if (animLevel == null)
        {
            GameObject animLevelGameObject = GameObject.FindGameObjectWithTag("TextAnimation");

            if (animLevelGameObject != null)
            {
                animLevel = animLevelGameObject.GetComponent<Animator>();
            }
            else
            {
                Debug.Log("No GameObject with tag 'TextAnimation' found!");
            }
        }
        else
        {
            Debug.Log("Animation already assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        LevelProgressSystem();

        if (animLevel == null)
        {
            GameObject animLevelGameObject = GameObject.FindGameObjectWithTag("TextAnimation");

            if (animLevelGameObject != null)
            {
                animLevel = animLevelGameObject.GetComponent<Animator>();
            }
            else
            {
                Debug.Log("No GameObject with tag 'TextAnimation' found!");
            }
        }
        else
        {
            Debug.Log("Animation already assigned.");
        }

        float takeHealThreshold = HealthPlayer.healthPlayerStatic.healthMaxPlayer - 40;

        if (heal && !isHealingNow && HealthPlayer.healthPlayerStatic.healthPlayer <= takeHealThreshold)
        {
            isHealingNow = true;
        }

        if (isHealingNow)
        {
            TakeHealPlayer = true;

            if (HealthPlayer.healthPlayerStatic.healthPlayer < HealthPlayer.healthPlayerStatic.healthMaxPlayer)
            {
                timeHeal += Time.deltaTime;
                if (timeHeal >= 0.2f)
                {
                    HealthPlayer.healthPlayerStatic.TakeHeal(1);
                    timeHeal = 0;
                }
            }
            else
            {
                isHealingNow = false; 
                TakeHealPlayer = false;
            }
        }
        else
        {
            TakeHealPlayer = false;
        }
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

            Skills += 2;
            Debug.Log("Level Player: 1");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 1 && progress >= maxProgress)
        {
            playerLevel = 2;
            progress = 0f;
            maxProgress = 250;

            Skills += 2;
            Debug.Log("Level Player: 2");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 2 && progress >= maxProgress)
        {
            playerLevel = 3;
            progress = 0f;
            maxProgress = 450;

            Skills += 2;
            Debug.Log("Level Player: 3");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 3 && progress >= maxProgress)
        {
            playerLevel = 4;
            progress = 0f;
            maxProgress = 700;

            Skills += 2;
            Debug.Log("Level Player: 4");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 4 && progress >= maxProgress)
        {
            playerLevel = 5;
            progress = 0f;
            maxProgress = 1000;

            Skills += 2;
            Debug.Log("Level Player: 5");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 5 && progress >= maxProgress)
        {
            playerLevel = 6;
            progress = 0f;
            maxProgress = 1350;

            Skills += 2;
            Debug.Log("Level Player: 6");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 6 && progress >= maxProgress)
        {
            playerLevel = 7;
            progress = 0f;
            maxProgress = 1750;

            Skills += 2;
            Debug.Log("Level Player: 7");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 7 && progress >= maxProgress)
        {
            playerLevel = 8;
            progress = 0f;
            maxProgress = 2200;

            Skills += 2;
            Debug.Log("Level Player: 8");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 8 && progress >= maxProgress)
        {
            playerLevel = 9;
            progress = 0f;
            maxProgress = 2700;

            Skills += 2;
            Debug.Log("Level Player: 9");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }
        else if (playerLevel == 9 && progress >= maxProgress)
        {
            playerLevel = 10;
            progress = 0f;
            maxProgress = 0;

            Skills += 2;
            Debug.Log("Level Player: 10");

            animLevel.SetTrigger("NewLevel");
            AudioManager.audioManager.NewLevel.Play();
        }

        levelText.text = playerLevel.ToString();
        
        targetFill = Mathf.Lerp(targetFill, progress, 5f * Time.deltaTime);
        ProgressText.text = Mathf.RoundToInt(targetFill).ToString() + " / " + maxProgress.ToString();
    }
}
