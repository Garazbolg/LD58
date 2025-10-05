using System.Collections;
using UnityEngine;

public class Trap : Trigger 
{
    public override void OnEnterTrigger()
    {
        Player.instance.StartCoroutine(TriggerTrap());
    }

    public override void OnExitTrigger()
    {
    }
    
    public IEnumerator TriggerTrap()
    {
        var playerController = Player.instance.GetComponent<PlayerController>();
        var triggerer = Player.instance.GetComponent<Triggerer>();
        var playerRigidbody = Player.instance.GetComponent<Rigidbody2D>();
        playerController.enabled = false;
        triggerer.enabled = false;
        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.gravityScale = 0;
        InputController.instance.DisableInputs();
        
        yield return FadeToColor.instance.Fade(0.5f, Color.red, AnimationCurveExtra.FastIn(0, 0, 1, 1));
        playerController.transform.position = new Vector3(Mathf.Floor(playerController.lastGroundedPosition.x) + 0.5f,
            Mathf.Round(playerController.lastGroundedPosition.y), playerController.transform.position.z);
        playerController.isGrounded = true;
        yield return new WaitForSeconds(.5f);
        yield return FadeToColor.instance.Fade(0.5f, Color.red, AnimationCurveExtra.SlowIn(0, 1, 1, 0));
        
        playerController.enabled = true;
        triggerer.enabled = true;
        InputController.instance.EnableInputs();
    }
}