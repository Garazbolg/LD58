using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool requiresInput = true;
    public Canvas interactionPrompt;
    
    public abstract void OnInteract();
    
    public void OnEnterTrigger()
    {
        if (!requiresInput)
        {
            OnInteract();
        }else
        {
            if (interactionPrompt != null) interactionPrompt.enabled = true;
        }
    }
    
    public void OnExitTrigger()
    {
        if (interactionPrompt != null) interactionPrompt.enabled = false;
    }
}