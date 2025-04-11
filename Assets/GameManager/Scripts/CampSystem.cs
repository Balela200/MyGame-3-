using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSystem : MonoBehaviour
{
    [Header("VFX")]
    public GameObject vfxFire;
    
    public void CampEvent()
    {
        vfxFire.SetActive(true);
    }
}
