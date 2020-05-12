using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEnemies : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (transform.parent.GetComponent<CrawAI>().SCREAM)
            {
                other.gameObject.GetComponent<TargetPlayer>().target = transform.parent.GetComponent<CrawAI>().PlayerPos;
            }
            transform.parent.GetComponent<CrawAI>().SCREAM = false;
        }
    }
}
