using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager campManager;
    public GameObject CampUIGameObject;

    public bool isCamp1 = false;
    public bool isCamp2 = false;

    public bool isCamp1On = false;
    public bool isCamp2On = false;

    public Transform Camp1Transform;
    public Transform Camp2Transform;

    public AudioSource AudioCamp1;
    public AudioSource AudioCamp2;
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

    public void Camp()
    {
        if (isCamp1 == true)
        {
            AudioCamp1.Play();
        }
        if (isCamp2 == true)
        {
            AudioCamp2.Play();
        }
    }
}
