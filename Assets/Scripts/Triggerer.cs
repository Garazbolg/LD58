using UnityEngine;

public class Triggerer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!enabled) return;
        var trigger = other.GetComponent<Trigger>();
        if (trigger != null)
        {
            trigger.OnEnterTrigger();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(!enabled) return;
        var trigger = other.GetComponent<Trigger>();
        if (trigger != null)
        {
            trigger.OnExitTrigger();
        }
    }
}