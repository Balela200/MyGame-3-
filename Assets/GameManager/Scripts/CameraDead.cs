using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraDead : MonoBehaviour
{
    int fun = 0;
    public Text FunText;
    // Start is called before the first frame update
    void Start()
    {
        fun = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (fun == 1)
        {
            FunText.text = "Your Enemies Thank You for the XP";
        }
        else if (fun == 2)
        {
            FunText.text = "Try Again, But With Less Dying";
        }
        else if (fun == 3)
        {
            FunText.text = "You’ve Been Promoted to Ghost!";
        }
        else if (fun == 4)
        {
            FunText.text = "You Fought Bravely... For About 10 Seconds";
        }
        else if (fun == 5)
        {
            FunText.text = "You Died Doing What You Love... Hopefully?";
        }
    }

    public void RespawnPlayer()
    {
        if(GameManager.gameManager.isLevel_1)
        {
            SceneManager.LoadScene(1);
        }

        if (GameManager.gameManager.isLevel_2)
        {
            SceneManager.LoadScene(2);
        }

        if (GameManager.gameManager.isLevel_3)
        {
            SceneManager.LoadScene(3);
        }

        Profile.deaths += 1;
    }
    public void QuitGame()
    {
        Application.Quit();

        Profile.deaths += 1;
    }
}
