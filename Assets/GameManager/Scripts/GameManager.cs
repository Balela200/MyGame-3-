using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Player")]
    public GameObject player;

    public Transform StartGame;

    [Header("Camp")]
    public GameObject Camp1Transform;
    public GameObject Camp2Transform;

    [Header("UI")]
    public GameObject HUDPlayer;
    public GameObject UIEsc;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Camp1Transform = GameObject.FindGameObjectWithTag("Camp1");
        Camp2Transform = GameObject.FindGameObjectWithTag("Camp2");

        bool isCampUI = CampManager.campManager.CampUIGameObject.activeSelf;
        bool isUIEsc = UIEsc.activeSelf;

        if (isCampUI || isUIEsc)
        {
            HUDPlayer.SetActive(false);
        }
        else
        {
            HUDPlayer.SetActive(true);
        }
    }

    public void RespawnPlayer()
    {
        if(CampManager.isCamp1 == true)
        {
            Instantiate(player, Camp1Transform.transform.position,Camp1Transform.transform.rotation);
        }
        else if(CampManager.isCamp2 == true)
        {
            Instantiate(player, Camp2Transform.transform.position, Camp2Transform.transform.rotation);
        }
        else
        {
            Instantiate(player, StartGame.position, StartGame.rotation);
        }
    }
}
