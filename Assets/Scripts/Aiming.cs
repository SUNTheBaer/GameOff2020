using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public InputManager inputManager;
    private Vector3 aimPos;

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        // Debug.Log(inputManager.aimPosition);

        aimPos = inputManager.aimPosition;
        var dir = aimPos - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + -90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
