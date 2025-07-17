using UnityEngine;

public class SoundController : MonoBehaviour
{

    //SOUND SOURCES
    public AudioSource sfxSource;      

    public AudioSource musicSource;

    //SOUND CLIPS

    public AudioClip popSFX;

    public AudioClip musicSONG1;


    public static SoundController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        musicSource.clip = musicSONG1;
        musicSource.loop = true;
        musicSource.volume = 0.2f;
        musicSource.Play();

        sfxSource.volume = 0.9f;
    }

    public void AddToDictionary()
    {
        Debug.Log("Pressed on interactible!");
            sfxSource.PlayOneShot(popSFX);
    }
}
