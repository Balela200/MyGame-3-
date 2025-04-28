using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveGame.saveGame.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBot()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
