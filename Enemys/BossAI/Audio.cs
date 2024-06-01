using UnityEngine;
using Random = UnityEngine.Random;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] audioClipsGeneral;
    public AudioClip[] audioClipsAttack;
    public AudioClip[] audioClipsTakeDamage;
    public AudioClip starter;

    private bool playedStarterclip = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playstarterClip()
    {
        if (!playedStarterclip)
        {
            audioSource.clip = starter;
            audioSource.PlayOneShot(audioSource.clip);
            playedStarterclip = true;
        }
        
    }

    public void playGeneralBossAudio()
    {
        audioSource.clip = audioClipsGeneral[Random.Range(0, audioClipsGeneral.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }
    
    public void playAttackBossAudio()
    {
        audioSource.clip = audioClipsAttack[Random.Range(0, audioClipsAttack.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }
    
    public void playBossTakeDamageAudio()
    {
        audioSource.clip = audioClipsTakeDamage[Random.Range(0, audioClipsTakeDamage.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }
}