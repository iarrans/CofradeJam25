using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("AudioSources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Music")]
    public AudioClip musicInGame;

    [Header("SFX")]
    public AudioClip[] damageTerry;
    public AudioClip[] deathTerry;

    public AudioClip[] audiosCapataz;
    public AudioClip[] audiosNazarenos;
    public AudioClip[] audiosPenitentes;
    public AudioClip[] audiosGuiriChico;
    public AudioClip[] audiosGuiriChica;
    public AudioClip[] audiosArmaos;

    public void Awake()
    {
        instance = this;
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip[] clip)
    {
        int index = Random.Range(0, clip.Length);
        SFXSource.PlayOneShot(clip[index]);
    }
}
