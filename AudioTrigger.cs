using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    public AudioSource audioSource;
    public List<AudioClip> randomAudioSources = new List<AudioClip>();
    public AudioClip audioClip;
    public float delay = 0f;
    public bool playOneShot;

    public void PlaySound()
    {
        AudioClip clip = randomAudioSources.Count <= 0 ? audioClip : randomAudioSources[Random.Range(0, randomAudioSources.Count)] ;
        
        if (playOneShot)
        {
            audioSource.PlayOneShot(clip);
            Destroy(gameObject);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
        }
        
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Invoke(nameof(PlaySound), delay);
        onTrigger.Invoke();
    }
}
