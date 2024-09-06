using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Dictionary to store player scores, keyed by PlayerStats
    public Dictionary<PlayerStats, int> playerScores = new Dictionary<PlayerStats, int>();

    public int playerLives = 3;
    public float respawnTime = 5.0f;
    public int killScoreValue = 1;
    public List<GameObject> spawnPoints;

    private void Start()
    {
        // Initialize game state, start timers, etc.
    }


    public void AddScore(PlayerStats scoringPlayer, int points)
    {
        // Check if the player is already in the dictionary
        if (playerScores.ContainsKey(scoringPlayer))
        {
            // If the player exists, update their score
            playerScores[scoringPlayer] += points;
        }
        else
        {
            // If the player does not exist, add them to the dictionary with an initial score
            playerScores[scoringPlayer] = points;
        }

        // UpdateScoreUI(scoringPlayer);
    }

    public void AddScoreKill(PlayerStats scoringPlayer)
    {
        AddScore(scoringPlayer, killScoreValue);
    }

    public void PlayerDied(GameObject deadPlayer)
    {
        RespawnPlayer(deadPlayer.gameObject);
    }
    private void RespawnPlayer(GameObject respawningPlayer)
    {
        CharacterController characterController = respawningPlayer.GetComponent<CharacterController>();

        // Generate a random index
        int randomIndex = Random.Range(0, spawnPoints.Count);
        // Temporarily disable the CharacterController to change the position safely
        characterController.enabled = false;
        // Set the player's position to the randomly chosen spawn point's position
        respawningPlayer.transform.position = spawnPoints[randomIndex].transform.position;
        characterController.enabled = true;

        PlayerStats respawningPlayStats = respawningPlayer.GetComponent<PlayerStats>();
        respawningPlayStats.SetHealth(100,true);
    }

    private void EndGame()
    {
        // Handle end of game, such as showing a game over screen
    }

    private void Update()
    {
        // Example timer handling
        // Handle other time-based events or updates here
    }
}