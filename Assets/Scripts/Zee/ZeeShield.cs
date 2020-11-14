using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeShield : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float manaGained = 0;
    private bool attackBlocked = false;
    public bool blockingCoroutine = false;
    [SerializeField] private float blockInvincibility = 0;
    public int blockPhase = 0;
    public IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossAttack")
        {
            if (blockPhase == 1)
                StartCoroutine(JustBlock());
            else if (blockPhase == 2)
                StartCoroutine(NormalBlock());
            else if (blockPhase == 3)
                StartCoroutine(ChipBlock());
        }
    }

    private void Update() {
        print(blockPhase);
    }

    public IEnumerator StartShieldCoroutine()
    {
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

    private IEnumerator JustBlock()
    {
        //print("justblocking");
        StopCoroutine(coroutine);
        blockPhase = 0;
        //playerScript.currentMana += manaGained;
        //playerScript.manaBar.SetCurrent(playerScript.currentMana);
        yield return new WaitForSeconds(blockInvincibility);
        //playerScript.playerCollision.damagable = true;
    }

    private IEnumerator NormalBlock()
    {
        print("normalblocking");
        StopCoroutine(coroutine);
        blockPhase = 0;
        yield return new WaitForSeconds(0.0f);
    }

    private IEnumerator ChipBlock()
    {
        print("chipblocking");
        StopCoroutine(coroutine);
        blockPhase = 0;
        yield return new WaitForSeconds(0.0f);
    }
}
