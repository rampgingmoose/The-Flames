using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDeathSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] deathSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        HandleRandomDeathSFX();
    }

    private void HandleRandomDeathSFX()
    {
        int randomSFXClip = Random.Range(0, deathSounds.Length);
        audioSource.clip = deathSounds[randomSFXClip];
        audioSource.Play();
    }
}
