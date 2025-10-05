using System;
using System.Collections;
using LDtkUnity;
using UnityEngine;

public class ZDoor : Interactable
{
    public int depthOffset => GetComponent<LDtkFields>().GetInt("depthOffset");
    
    public override void OnInteract()
    {
        Player.instance.StartCoroutine(EnterZDoor());
    }
    
    public IEnumerator EnterZDoor()
    {
        InputController.instance.DisableInputs();
        AudioSource.PlayClipAtPoint(Library.instance.zDoorSound, transform.position);

        yield return FadeToColor.instance.Fade(0.2f, Color.black, AnimationCurveExtra.FastIn(0, 0, 1, 1));

        yield return new WaitForSeconds(0.1f);
        if (LDTKWorldInstance.ldtkInstance != null)
        {
            LDTKWorldInstance.ldtkInstance.SetWorldDepth(LDTKWorldInstance.ldtkInstance.currentDepth + depthOffset);
        }
        yield return new WaitForSeconds(0.1f);
        
        yield return FadeToColor.instance.Fade(0.2f, Color.black, AnimationCurveExtra.SlowIn(0,1,1,0));
        InputController.instance.EnableInputs();
    }
}