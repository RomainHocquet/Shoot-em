
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Dictionary to store player scores, keyed by PlayerStats
    public Dictionary<PlayerStats, int> playerScores = new Dictionary<PlayerStats, int>();
    public List<GameObject> playerList;
    // private Dictionary<GameObject, Color> playerColors;
    private Dictionary<GameObject, Color> playerColors = new Dictionary<GameObject, Color>();

    //List of the colors a player can take
    private List<Color> colorList = new List<Color>
        {
            new Color(1.0f, 0.0f, 0.0f), // Bright Red
            new Color(0.0f, 0.5f, 1.0f), // Bright Blue
            new Color(0.0f, 1.0f, 0.0f), // Bright Green
            new Color(1.0f, 1.0f, 0.0f), // Bright Yellow
            new Color(1.0f, 0.0f, 1.0f), // Bright Magenta
            new Color(0.0f, 1.0f, 1.0f), // Bright Cyan
            new Color(1.0f, 0.5f, 0.0f), // Bright Orange
            new Color(1.0f, 0.8f, 0.0f), // Bright Gold
            new Color(1.0f, 0.4f, 0.7f), // Bright Pink
            new Color(0.8f, 1.0f, 0.0f)  // Bright Lime
        };


    public int playerLives = 3;
    public float respawnTime = 5.0f;
    public int killScoreValue = 1;
    public List<GameObject> spawnPoints;
    private int numberMaxOfPlayer;
    public GameObject playerTexts; //GameObject containing the list of players Text

    public GameObject[] weaponsSpawnPoints;
    public GameObject weaponPickUpContainer;
    private void Awake()
    {
        numberMaxOfPlayer = colorList.Count;
    }

    public void AddPlayer(PlayerStats playerStats)
    {

        GameObject player = playerStats.gameObject;

        playerList.Add(player);
        // Check if there are any colors left in the color list
        if (colorList.Count > 0)
        {
            Color assignedColor = colorList[0];
            playerStats.playerNumber = numberMaxOfPlayer - colorList.Count;

            playerColors.Add(player, assignedColor);
            colorList.RemoveAt(0);
            if (playerStats != null)
            {
                playerStats.UpdateColor(assignedColor);
            }
            AddTextField(playerStats);
        }
        else
        {
            Debug.LogWarning("No color left to give to new player");
        }

    }


    private void AddTextField(PlayerStats newPlayerStats)
    {
        PlayerTextSC playerTextSC = playerTexts.GetComponent<PlayerTextSC>();
        
        GameObject playerText = playerTextSC.getAvailablePlayerTextSpot();


        Image playerTextBackGround = playerText.GetComponent<Image>();
        Color backgroundColor = playerTextBackGround.color;
        backgroundColor = newPlayerStats.myColor;
        // Change alpha to 14 (out of 255)
        float newAlpha = 14f / 255f;
        backgroundColor.a = newAlpha;
        playerTextBackGround.color = backgroundColor;

        TextMeshPro textComponent = playerText.GetComponentInChildren<TextMeshPro>();
        // textComponent.text = "Player " + newPlayerStats.playerNumber + "\n score : 0";
        string finalText = "Player " + (newPlayerStats.playerNumber + 1) + "\n";
        finalText += "Score : 0" + "\n";
        // finalText += "Ammo : X" + "\n";
        // finalText += "Bonus :  Sample Bonus name" + "\n";
        textComponent.text = finalText;

    }

    private void UpdatePlayerText(PlayerStats newPlayerStats)
    {

        PlayerTextSC playerTextSC = playerTexts.GetComponent<PlayerTextSC>();
        GameObject playerText = playerTextSC.GetPlayer(newPlayerStats.playerNumber);
        TextMeshPro textComponent = playerText.GetComponentInChildren<TextMeshPro>();

        string finalText = "Player " + (newPlayerStats.playerNumber + 1) + "\n";
        finalText += "Score : " + playerScores[newPlayerStats] + "\n";
        // finalText += "Ammo : X" + "\n";
        // finalText += "Bonus :  Sample Bonus name" + "\n";
        textComponent.text = finalText;
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

        UpdatePlayerText(scoringPlayer);

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
        respawningPlayStats.SetHealth(100, true);
    }

    //TODO: add a timer to the weapon spawn
    public void WeaponGetPickedUp()
    {

        SpawnAWeapon();
    }
    private void SpawnAWeapon()
    {
        Debug.Log("spawnning a weapon");
        int weaponPositionIndex = Random.Range(0, weaponsSpawnPoints.Length);
        Instantiate(weaponPickUpContainer, weaponsSpawnPoints[weaponPositionIndex].transform);
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