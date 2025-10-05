using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayer;
    public float interactRadius = 1f;
    public Transform interactPoint;
    public InputActionReference interactAction;
    public float cooldownTime = 0.5f;
    
    private float lastInteractTime = -Mathf.Infinity;
    
    private Interactable currentInteractable;

    private void Update()
    {
        Interactable interactable = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(interactPoint.position, interactRadius, interactableLayer);
        foreach (var hit in hits)
        {
            interactable = hit.GetComponent<Interactable>();
            if (interactable != null)
            {
                break; // Interact with the first found interactable
            }
        }
        
        if (currentInteractable != interactable)
        {
            ExitTrigger(currentInteractable);
            currentInteractable = interactable;
            lastInteractTime = -Mathf.Infinity; // Reset cooldown when switching interactables
            EnterTrigger(currentInteractable);
        }
        
        if (currentInteractable != null && interactAction.action.WasPerformedThisFrame() && Time.time >= lastInteractTime + cooldownTime)
        {
            currentInteractable.OnInteract();
            lastInteractTime = Time.time;
        }
    }

    private void EnterTrigger(Interactable interactable)
    {
        if(interactable != null)
        {
            interactable?.OnEnterTrigger();
        }
    }

    private void ExitTrigger(Interactable interactable)
    {
        if(interactable != null)
        {
            interactable?.OnExitTrigger();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
        }
    }
}