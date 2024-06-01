using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioClipSequencer : MonoBehaviour
{
    public List<AudioClip> stepsAudioClips;
    public float interval = 1f;
    public bool isActive;
    
    private AudioSource audioSource;
    private int index;
    private float cooldown;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = stepsAudioClips[0];
    }

    private void Update()
    {
        if (!isActive) return;
        if (cooldown >= 0f)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        
        audioSource.clip = stepsAudioClips[index];
        cooldown = interval;
        index = ++index % stepsAudioClips.Count;
        audioSource.Play();
    }
    
    public void SetInterval(float interval = 1f)
    {
        this.interval = interval;
        isActive = true;
    }
    
    public void Stop()
    {
        cooldown = interval;
        isActive = false;
    }
}