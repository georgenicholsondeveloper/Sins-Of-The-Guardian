using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreakScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Rock Break")
        {
            Destroy(gameObject);
        }
    }
}
