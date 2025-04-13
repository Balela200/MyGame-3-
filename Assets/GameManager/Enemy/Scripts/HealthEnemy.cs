using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    public static HealthEnemy HealthEnemyStatic;
    [Header("Health")]
    public float healthEnemy = 100;
    public float healthMaxEnemy = 100;

    [Header("Camera")]
    public Camera cam;

    [Header("UI")]
    public Transform UIEnemy;
    public Image HealthUI;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        else
        {
            Debug.Log("No Camera");
        }

        HealthEnemyStatic = this;
    }

    void Update()
    {
        HealthUI.fillAmount = healthEnemy / healthMaxEnemy; 


        if (Vector3.Distance(transform.position, cam.transform.position) < 30f) 
        {
            UIEnemy.gameObject.SetActive(true);

            UIEnemy.transform.LookAt(cam.transform);
            UIEnemy.transform.Rotate(0, 180f, 0);
        }
        else
        {
            UIEnemy.gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float damage)
    {
        healthEnemy -= damage;
        if (healthEnemy < 0)
        {
            // Daed
            Destroy(gameObject);
            healthEnemy = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(5);
        }
    }
}
