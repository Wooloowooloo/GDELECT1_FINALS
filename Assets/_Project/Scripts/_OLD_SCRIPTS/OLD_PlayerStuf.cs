using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_PlayerStuf : MonoBehaviour
{
    [SerializeField] int healthTotal;

    public int currentHealth;
    private Vector3 respawnPoint;

    public bool gotHit = false;
    public bool isDead = false;

    private void Update()
    {
        //int updateHealth = DreamState.playerHealth;
        //healthTotal = updateHealth;
    }

    public void PlayerDamaged()
    {
        Debug.Log("Got Hit");
        currentHealth--;

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        gotHit = true;
    }

    public void PlayerRespawn()
    {
        if (isDead == true)
        {
            Debug.Log("Deds");

            transform.position = respawnPoint;
            Debug.Log("Respawn " + transform.position);

            currentHealth = healthTotal;
            Debug.Log("Current Health: " + currentHealth);

            isDead = false;
        }    
    }
}
