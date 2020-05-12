using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player.name)
        {
            player.GetComponent<DamageScript>().Damage(5);
            gameObject.SetActive(false);
        }
    }

}

