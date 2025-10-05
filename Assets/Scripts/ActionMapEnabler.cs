using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapEnabler : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public string actionMapName;

    void OnEnable()
    {
        var map = actionAsset.FindActionMap(actionMapName);
        if (map != null)
        {
            map.Enable();
        }
        else
        {
            Debug.LogWarning($"Action map '{actionMapName}' not found in the provided InputActionAsset.");
        }
    }

    void OnDisable()
    {
        var map = actionAsset.FindActionMap(actionMapName);
        if (map != null)
        {
            map.Disable();
        }
    }
}