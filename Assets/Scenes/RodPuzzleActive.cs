using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodPuzzleActive : MonoBehaviour
{
    [SerializeField]
    Animator rock;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rock.enabled = true;
        }
    }
}
