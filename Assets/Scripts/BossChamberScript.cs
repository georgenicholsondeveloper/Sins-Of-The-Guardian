using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChamberScript : MonoBehaviour
{
    public bool activated;
    public int activeNumber = 0;

    [SerializeField]
    Transform newPos;

    void Update()
    {
        if (activated)
        {
            if(activeNumber == 1)
              transform.position = newPos.position;
            if (activeNumber == 2)
                Destroy(gameObject);
        }
    }
}
