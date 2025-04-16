using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSystem : MonoBehaviour
{
    [Header("VFX")]
    public GameObject vfxFire;

    [Header("is")]
    public bool Camp1;
    public bool Camp2;

    private void Update()
    {
        CampEvent();
    }
    public void CampEvent()
    {
        if(CampManager.campManager.isCamp1On == true && Camp1 == true)
        {
            vfxFire.SetActive(true);
        }
        else if(CampManager.campManager.isCamp2On == true && Camp2 == true)
        {
            vfxFire.SetActive(true);
        }
    }
}
