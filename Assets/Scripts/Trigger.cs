using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public abstract void OnEnterTrigger();
    public abstract void OnExitTrigger();
}