using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public AudioSource walking;
    public AudioSource Sword;
    public AudioSource ManAttack;

    public AudioSource AudioShields;

    public AudioSource LevelUp;
    public AudioSource NewLevel;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Walking()
    {
        walking.Play();
    }
    public void Attack()
    {
        Sword.Play();
        ManAttack.Play();
    }
}
