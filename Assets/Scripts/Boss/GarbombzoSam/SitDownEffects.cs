using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SitDownEffects : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera explodeShake = null;
    [SerializeField] int camPriority = 0;
    [SerializeField] bool play = false;

    private void Update()
    {
        if (play)
            explodeShake.Priority = camPriority;
    }
}
