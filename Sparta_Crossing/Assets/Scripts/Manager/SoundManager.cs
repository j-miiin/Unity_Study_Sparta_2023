using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource _audioSource;
    private AudioClip _animalCrossingBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _audioSource = this.AddComponent<AudioSource>();
        _animalCrossingBGM = Resources.Load<AudioClip>("Music/animal_crossing_bgm");
        _audioSource.clip = _animalCrossingBGM;
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}
