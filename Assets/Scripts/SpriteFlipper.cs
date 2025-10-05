using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    public PlayerController controller;
    public SpriteRenderer spriteRenderer;
    
    void Update()
    {
        spriteRenderer.flipX = !controller.facingRight;
    }
}