using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GSBombEffects : MonoBehaviour
{
    [SerializeField]  CinemachineVirtualCamera explodeShake = null;
    [SerializeField]  GameObject flashFx = null;

    [SerializeField] int camPriority = 0;
    [SerializeField] bool flash = false;

    [SerializeField] bool play = false;


    private void Update()
    {
        if(play)
            explodeShake.Priority = camPriority;

        if (flash)
            flashFx.SetActive(true);
        else
            flashFx.SetActive(false);
    }
}
