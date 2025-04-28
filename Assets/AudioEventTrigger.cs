using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioEventTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private bool Number1 = false;
    public GameObject Number1GameObject;

    private bool Number2 = false;
    public GameObject Number2GameObject;

    private bool Number3 = false;
    public GameObject Number3GameObject;

    private bool Number4 = false;
    public GameObject Number4GameObject;

    void Update()
    {
        if (audioSource.isPlaying && !Number1 && audioSource.time >= 0.5f)
        {
            Number1 = true;
            Number1GameObject.SetActive(true);
        }

        if (audioSource.isPlaying && !Number2 && audioSource.time >= 4.4f)
        {
            Number2 = true;
            Number1GameObject.SetActive(false);

            Number2GameObject.SetActive(true);
        }

        if (audioSource.isPlaying && !Number3 && audioSource.time >= 10f)
        {
            Number3 = true;
            Number3GameObject.SetActive(true);

            Number2GameObject.SetActive(false);
        }

        if (audioSource.isPlaying && !Number4 && audioSource.time >= 40f)
        {
            SceneManager.LoadScene(0);
        }
    }
}

