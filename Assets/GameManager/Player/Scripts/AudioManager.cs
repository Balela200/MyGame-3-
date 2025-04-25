using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource walking;
    public AudioSource Sword;
    public AudioSource ManAttack;
    // Start is called before the first frame update
    void Start()
    {

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
