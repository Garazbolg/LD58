using UnityEngine;

public class Library : MonoBehaviour
{
    public static Library instance;

    public AudioClip zDoorSound;
    public AudioClip trapDeathSound;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}