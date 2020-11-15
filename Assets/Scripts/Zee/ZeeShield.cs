using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeShield : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float blockInvincibility = 0;
    [SerializeField] private float manaGained = 0;
    [SerializeField] private float manaCost = 0;

    public IEnumerator coroutine;
    public int blockPhase = 0;

    private void Update()
    {
        print(blockPhase);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossAttack" && blockPhase > 0)
        {
            if (blockPhase == 1)
                JustBlock();
            else if (blockPhase == 2)
                NormalBlock();
            else if (blockPhase == 3)
                ChipBlock();

            StartCoroutine(GeneralBlock());
        }
    }

    public IEnumerator StartShieldCoroutine()
    {
        print("start shield coroutine");
        playerScript.playerCollision.isDamagable = false;
        playerScript.currentMana -= manaCost;
        playerScript.manaBar.SetCurrent(playerScript.currentMana);
        
        blockPhase = 1;
        yield return new WaitForSeconds(1.0f);
        if (blockPhase == 0)
            yield break;
        blockPhase = 2;
        yield return new WaitForSeconds(1.0f);
        if (blockPhase == 0)
            yield break;
        blockPhase = 3;
        yield return new WaitUntil(() => blockPhase == 0);
    }

    private IEnumerator GeneralBlock()
    {
        print("start general block");
        StopCoroutine(coroutine);
        blockPhase = 0;
        yield return new WaitForSeconds(blockInvincibility);
        playerScript.playerCollision.isDamagable = true;
    }

    private void JustBlock()
    {
        print("start just block");
        playerScript.currentMana += manaGained;
        playerScript.manaBar.SetCurrent(playerScript.currentMana);
    }

    private void NormalBlock()
    {
        print("start normal block");
    }

    private void ChipBlock()
    {
        print("start chip block");
    }
}
