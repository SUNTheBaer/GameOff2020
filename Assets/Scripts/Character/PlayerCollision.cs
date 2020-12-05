using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private CinemachineVirtualCamera deathCam = null;
    //[SerializeField] private GameObject deathText = null;
    [SerializeField] private GameObject deathTransition = null;

    [HideInInspector] public bool isDamagable = true;
    [SerializeField] private float invulTime = 0;

    private void Start()
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMax(playerScript.maxHealth);
    }

    public IEnumerator TakeDamage(float damage)
    {
        if (isDamagable)
        {
            isDamagable = false;

            if (playerScript.zeeShield.chipBlocked)
                playerScript.currentHealth -= damage * playerScript.zeeShield.chipBlockMultiplier;
            else
                playerScript.currentHealth -= damage;

            playerScript.healthBar.SetCurrent(playerScript.currentHealth);

            if (playerScript.currentHealth <= 0)
                StartCoroutine(Perish());
            
            if (playerScript.zeeShield.holdingShield)
                yield return new WaitForSeconds(playerScript.zeeShield.blockInvincibility);
            else
                yield return new WaitForSeconds(invulTime);
            
            isDamagable = true;
        }
    }
    
    private IEnumerator Perish()
    {
        playerScript.inputManager.inputs.Disable();
        deathCam.Priority = 11;
        //playerScript.ChangeAnimationState("Death");
        
        yield return new WaitForSeconds(0.5f); //A bit into the animation

        //deathText.SetActive(true); //bloody death text

        yield return new WaitForSeconds(1.5f); //hold
        
        deathTransition.SetActive(true); //fade

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //change to load to hub
    }
}