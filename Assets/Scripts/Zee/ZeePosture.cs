using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeePosture : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float postureRegenRate = 0;
    [SerializeField] private float postureCostOnBlock = 0;
    private float t;
    private float currentPosture;
    [SerializeField] private float initialRegen = 0;
    [SerializeField] private float fastRegen = 0;
    private float currentRegen = 0;
    [SerializeField] private float maxPosture = 0;
    [SerializeField] private float postureBreakTime = 0;
    [HideInInspector] public bool brokenPosture;

    private void Start()
    {
        currentPosture = maxPosture;
        currentRegen = initialRegen;
    }

    private void Update()
    {
        if (playerScript.zeeShield.successfulBlock)
        {
            playerScript.zeeShield.successfulBlock = false;
            currentPosture -= postureCostOnBlock;
            currentRegen = initialRegen;
            t = 0;
        }

        t += Time.deltaTime;

        if (t > currentRegen && (currentPosture < maxPosture))
        {
            currentPosture += postureRegenRate;
            t = 0;

            if (currentPosture < maxPosture)
                currentRegen = fastRegen;
            else
                currentRegen = initialRegen;
        }

        if (currentPosture == 0)
        {
            StartCoroutine(BrokenPosture());
        }
    }

    private IEnumerator BrokenPosture()
    {
        brokenPosture = true;

        yield return new WaitForSeconds(postureBreakTime);

        brokenPosture = false;
        playerScript.inputManager.inputs.Player.Shield.Disable();

        yield return new WaitUntil(() => currentPosture == maxPosture);
        
        playerScript.inputManager.inputs.Player.Shield.Enable();
    }
}
