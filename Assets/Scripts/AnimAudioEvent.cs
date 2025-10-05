using UnityEngine;

public class AnimAudioEvent : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void PlayClip()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }
}