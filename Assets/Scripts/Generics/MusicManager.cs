using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            return _instance ??
                   (_instance = 
                       (Instantiate(Resources.Load<GameObject>("musicmanager")) as GameObject)
                       .GetComponent<MusicManager>());
        }
    }

    [SerializeField] private AudioClip[] normalSongs;
    [SerializeField] private AudioClip coolSong;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _instance = this;
    }

    public void PlayNormalMusic()
    {
        audioSource.Stop();
        audioSource.clip = normalSongs[Random.Range(0, normalSongs.Length)];
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void PlayCoolSong()
    {
        audioSource.Stop();
        audioSource.clip = coolSong;
        audioSource.volume = 0.8f;
        audioSource.time = 2;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    void OnDestroy()
    {
        _instance = null;
    }
}
