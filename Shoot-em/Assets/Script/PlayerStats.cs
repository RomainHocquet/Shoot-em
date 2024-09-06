using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    // [HideInInspector]
    public float healthPoint;//Should not be set in unity
    public GameManager gameManager;
    public Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthPoint;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(float damageTaken, PlayerStats attackingPlayer)
    {
        healthPoint -= damageTaken;
        if (healthPoint < 0)
        {
            die(attackingPlayer);
        }
        SetHealth(healthPoint, false);
    }


    public void SetHealth(float newHealthPoint, bool isPercentages)
    {
        
        if (isPercentages == false)
        {
            healthbar.fillAmount = newHealthPoint / maxHealth;
            this.healthPoint = newHealthPoint;
        }
        else
        {
            healthbar.fillAmount = newHealthPoint;
            this.healthPoint = newHealthPoint * maxHealth / 100;
        }
    }


    public void die(PlayerStats attackingPlayer)
    {
        gameManager.AddScoreKill(attackingPlayer);

        //Mage the player respawn (probably)
        gameManager.PlayerDied(this.gameObject);


    }
}
