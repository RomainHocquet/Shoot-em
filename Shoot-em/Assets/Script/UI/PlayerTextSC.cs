using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextSC : MonoBehaviour
{

    private List<GameObject> unusedPlayerText = new List<GameObject>(); // List of used PlayerText objects

    [SerializeField]
    private List<GameObject> playerText = new List<GameObject>(); // List of unused PlayerText objects

    // Start is called before the first frame update
    void Awake()
    {
        //Copy playerText to unusedPlayerText
        unusedPlayerText.AddRange(playerText);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Function to get an available (unused) PlayerText spot
    public GameObject getAvailablePlayerTextSpot()
    {
        
        if (unusedPlayerText.Count > 0)
        {
            GameObject availableSpot = unusedPlayerText[0];

            // Move it to the used list;
            unusedPlayerText.Remove(availableSpot);
            availableSpot.SetActive(true);

            return availableSpot;
        }

        return null; // Return null if no available spots exist
    }

    public GameObject GetPlayer(int playerNumber)
    {
        Debug.Log("" + playerNumber);
        return playerText[playerNumber];
    }
}










