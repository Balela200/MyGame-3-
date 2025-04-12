using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager campManager;
    public GameObject CampUIGameObject;
    // Start is called before the first frame update
    void Start()
    {
        campManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sit()
    {
        PlayerControllor.playerControllor.anim.SetBool("Sit", true);
        PlayerControllor.playerControllor.isSit = true;
    }
    
    public void Stand()
    {
        PlayerControllor.playerControllor.anim.SetBool("Sit", false);
        PlayerControllor.playerControllor.isSit = false;
        CampUIGameObject.SetActive(false);

        //  Can Rotation Camera
        PlayerControllor.playerControllor.isCanRotationCamera = true;
        Cursor.lockState = CursorLockMode.Locked;

        if (PlayerControllor.playerControllor.isSit == false)
        {
            // Movement
            PlayerControllor.playerControllor.isCanMove = true;
            PlayerControllor.playerControllor.isAttacking = true;
            PlayerControllor.playerControllor.isDodging = true;
            PlayerControllor.playerControllor.isCanRotation = true;
        }
    }
}
