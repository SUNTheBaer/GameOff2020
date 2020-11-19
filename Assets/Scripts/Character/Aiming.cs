using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameObject player = null;

    private Vector2 lastMousePosition;
    private Vector2 aimPosition;
    private Vector2 playerPosition;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float angle;

    private bool onController;
    private bool onMouse;
    
    private void Update()
    {
        if (lastMousePosition == playerScript.inputManager.mouseAimPosition)
            onMouse = false;
        else
        {
            onMouse = true;
            playerScript.inputManager.onController = false;
        }
        
        onController = playerScript.inputManager.onController;
        
        if (onController)
            ControllerAim();
        
        if (onMouse)
            MouseAim();
    }

    private void LateUpdate()
    {
        lastMousePosition = playerScript.inputManager.mouseAimPosition;
    }

    private void ControllerAim()
    {
        direction = playerScript.inputManager.padAimPosition;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90; //-90 due to how the sprite is oriented
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MouseAim()
    {
        playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        aimPosition = playerScript.inputManager.mouseAimPosition - playerPosition;
        direction = aimPosition;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90; //-90 due to how the sprite is oriented
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
