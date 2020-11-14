using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashing : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    [SerializeField] private bool spriteFlashColorActive = false;
    [SerializeField] private Color spriteFlashColor = Color.white;

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
