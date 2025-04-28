using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    public static HealthPlayer healthPlayerStatic;
    [Header("Health")]
    public float healthPlayer = 100;
    public float healthMaxPlayer = 100;

    [Header("Camera")]
    public Camera GameCamera;
    public Camera DeadCamera;

    [Header("Bool")]
    private bool isPlayingAudio = false;
    void Start()
    {
        healthPlayerStatic = this;
    }

    void Update()
    {
        if(healthPlayer > healthMaxPlayer)
        {
            healthPlayer = healthMaxPlayer;
        }
    }
    public void TakeDamage(float damage)
    {
        healthPlayer -= damage;

        if (healthPlayer <= 0)
        {
            GameCamera = Camera.main;
            // Daed
            if(GameCamera != null)
            {
                GameCamera.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("No GameCamera");
            }
            DeadCamera.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            HUD();

            PlayerControllor.playerControllor.anim.SetTrigger("Dead");
            healthPlayer = 0;
        }
    }

    void HUD()
    {
        GameObject hud = GameObject.FindGameObjectWithTag("HUDPlayer");
        if (hud == null)
        {
            Debug.Log("No HUD");
        }
        else
        {
            hud = GameObject.FindGameObjectWithTag("HUDPlayer");
            hud.SetActive(false);
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
            if (PlayerControllor.playerControllor.isShield)
            {
                if (!isPlayingAudio)
                {
                    StaminaSystem.staminaSystem.StaminaLoss(20);

                    // Play shield sound
                    AudioManager.audioManager.AudioShields.Play();
                    StartCoroutine(WaitForAudio(AudioManager.audioManager.AudioShields));
                }
            }
            else
            {
                if (!isPlayingAudio)
                {
                    TakeDamage(10);

                    // Play damage sound
                    AudioManager.audioManager.Damage.Play();
                    StartCoroutine(WaitForAudio(AudioManager.audioManager.Damage));
                }
            }
        }

        if (other.CompareTag("EnemyAttackBoss"))
        {
            if (PlayerControllor.playerControllor.isShield)
            {
                if (!isPlayingAudio)
                {
                    StaminaSystem.staminaSystem.StaminaLoss(40);

                    // Play shield sound
                    AudioManager.audioManager.AudioShields.Play();
                    StartCoroutine(WaitForAudio(AudioManager.audioManager.AudioShields));
                }
            }
            else
            {
                if (!isPlayingAudio)
                {
                    TakeDamage(30);

                    // Play damage sound
                    AudioManager.audioManager.Damage.Play();
                    StartCoroutine(WaitForAudio(AudioManager.audioManager.Damage));
                }
            }
        }
    }

    private IEnumerator WaitForAudio(AudioSource audioSource)
    {
        isPlayingAudio = true;
        yield return new WaitForSeconds(audioSource.clip.length);
        isPlayingAudio = false;
    }
}
