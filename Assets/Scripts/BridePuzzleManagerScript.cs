using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridePuzzleManagerScript : MonoBehaviour
{
    public int interactionNumber = 0, secondInteraction = 0;

    [SerializeField]
    GameObject[] bridges;


    void Update()
    {
        InteractionManager();
    }

    void InteractionManager()
    {
        if(interactionNumber != 0)
        {
            bridges[interactionNumber - 1].transform.Rotate(0, 25 * Time.deltaTime, 0);
            bridges[secondInteraction -1].transform.Rotate(0, 25 * Time.deltaTime, 0);
        }

    }
}
