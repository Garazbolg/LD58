using UnityEngine;

public class Library : MonoBehaviour
{
    public static Library instance;

    public AudioClip zDoorSound;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}