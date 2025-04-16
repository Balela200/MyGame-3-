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
