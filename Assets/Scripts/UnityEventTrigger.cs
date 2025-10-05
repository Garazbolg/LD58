public class UnityEventTrigger : Trigger
{
    public UnityEngine.Events.UnityEvent onEnter;
    public UnityEngine.Events.UnityEvent onExit;

    public override void OnEnterTrigger()
    {
        onEnter.Invoke();
    }

    public override void OnExitTrigger()
    {
        onExit.Invoke();
    }
}