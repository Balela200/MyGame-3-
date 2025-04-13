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
    
    public void CampEvent()
    {
        vfxFire.SetActive(true);
    }
}
