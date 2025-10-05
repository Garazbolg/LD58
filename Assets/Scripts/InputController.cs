using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController instance;
    public InputActionAsset actionAsset;
    public string defaultActionMapName = "Player";
    private InputActionMap currentActionMap;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SwitchActionMap(defaultActionMapName);
    }
    
    public void SwitchActionMap(string actionMapName)
    {
        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }
        
        currentActionMap = actionAsset.FindActionMap(actionMapName);
        if (currentActionMap != null)
        {
            currentActionMap.Enable();
        }
        else
        {
            Debug.LogWarning($"Action map '{actionMapName}' not found in the provided InputActionAsset.");
        }
    }
    
    public InputActionMap GetCurrentActionMap()
    {
        return currentActionMap;
    }
    
    public void DisableInputs()
    {
        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }
    }
    
    public void EnableInputs()
    {
        if (currentActionMap != null)
        {
            currentActionMap.Enable();
        }
    }
}