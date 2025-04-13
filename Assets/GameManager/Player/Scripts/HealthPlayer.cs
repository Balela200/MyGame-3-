using System;
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
            Destroy(gameObject);
            healthPlayer = 0;
            GameManager.gameManager.RespawnPlayer();
        }
    }

    public void TakeHeal(float heal)
    {
        healthPlayer += heal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            TakeDamage(3);
        }
    }
}
