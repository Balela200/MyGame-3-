using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public static HealthPlayer healthPlayerStatic;
    [Header("Health")]
    public float healthPlayer = 100;
    public float healthMaxPlayer = 100;

    void Start()
    {
        healthPlayerStatic = this;
    }
    public void TakeDamage(float damage)
    {
        healthPlayer -= damage;
        if (healthPlayer < 0)
        {
            // Daed
            PlayerControllor.playerControllor.anim.SetTrigger("Dead");
        }
    }

    public void TakeHeal(float heal)
    {
        healthPlayer += heal;
    }
}
