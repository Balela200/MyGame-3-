using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Player")]
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        if(CampManager.campManager.isCamp1 == true)
        {
            Instantiate(player, CampManager.campManager.Camp1Transform.position, CampManager.campManager.Camp1Transform.rotation);
        }
        else if(CampManager.campManager.isCamp2 == true)
        {
            Instantiate(player, CampManager.campManager.Camp2Transform.position, CampManager.campManager.Camp2Transform.rotation);
        }
        else
        {

        }
    }
}
