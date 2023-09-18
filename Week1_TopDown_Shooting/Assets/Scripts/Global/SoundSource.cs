using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        CancelInvoke(); // 이전에 Invoke가 남아있는 상태라면 취소
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        Invoke("Disable", clip.length + 2); // 클립 길이 + 2 이후에 지연 실행
    }

    public void Disable()
    {
        _audioSource.Stop();    // 재사용 중이므로 삭제하면 안 되고 꺼야 함
        gameObject.SetActive(false);
    }
} 
