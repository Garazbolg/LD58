using System;
using LDtkUnity;
using UnityEngine;

public class LDTKWorldInstance : MonoBehaviour
{
    public LDtkComponentWorld ldtkWorld;
    public int startingDepth = 1;
    public int currentDepth = 1;
    
    public static LDTKWorldInstance ldtkInstance;
    
    private void Awake()
    {
        if (ldtkInstance == null)
        {
            ldtkInstance = this;
        }
    }
    
    void Start()
    {
        SetWorldDepth(startingDepth);
    }

    public void SetWorldDepth(int depth)
    {
        currentDepth = depth;
        foreach (var level in ldtkWorld.Levels)
        {
            level.gameObject.SetActive(level.WorldDepth == currentDepth);
        }
    }

    private void OnValidate()
    {
        if (ldtkWorld != null)
        {
            SetWorldDepth(currentDepth);
        }
    }
}