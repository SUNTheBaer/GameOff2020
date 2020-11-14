using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649 // Disable Color never assigned warning

public class SpriteFlashing : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    [SerializeField] private bool spriteFlashColorActive = false;
    [SerializeField] private Color spriteFlashColor;

    private void Start () 
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }
    
    private void Update()
    {
        if (spriteFlashColorActive)
            flashEffectSprite(spriteFlashColor);
        else
            normalSprite();
    }

    private void flashEffectSprite(Color spriteFlashColor)
    {
        spriteRenderer.material.shader = shaderGUItext;
        spriteRenderer.color = spriteFlashColor;
    }

    private void normalSprite()
    {
        spriteRenderer.material.shader = shaderSpritesDefault;
        spriteRenderer.color = Color.white;
    }
 }
