using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockActivationScript : MonoBehaviour
{
    [SerializeField]
    GameObject hangingRock;

    public void ActivateRock()
    {
        hangingRock.SetActive(true);
    }
}
