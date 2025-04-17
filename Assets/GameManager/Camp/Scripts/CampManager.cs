using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CampManager : MonoBehaviour
{
    public static CampManager campManager;
    [Header("System")]
    public GameObject CampUIGameObject;

    public static bool isCamp1 = false;
    public static bool isCamp2 = false;

    public bool isCamp1On = false;
    public bool isCamp2On = false;

    public Image lineCamp_1;

    [Header("Audio")]
    public AudioSource AudioCamp1;
    public AudioSource AudioCamp2;

    [Header("Health")]
    float timeHeal;

    public bool isHeal = false;
    // Start is called before the first frame update
    void Start()
    {
        campManager = this;
        Camp();
    }

    // Update is called once per frame
    void Update()
    {
        Camp();

        timeHeal += Time.deltaTime;
        if (timeHeal >= 0.1 && isHeal == true)
        {
            HealthPlayer.healthPlayerStatic.TakeHeal(1);
            timeHeal = 0;
        }
    }

    public void Sit()
    {
        PlayerControllor.playerControllor.anim.SetBool("Sit", true);
        PlayerControllor.playerControllor.isSit = true;

        isHeal = true;
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

        isHeal = false;
    }

    public void Camp()
    {
        if (isCamp1 == true)
        {
            isCamp1On = true;
            lineCamp_1.color = Color.green;
        }
        if (isCamp2 == true)
        {
            isCamp2On = true;
        }
    }
}
