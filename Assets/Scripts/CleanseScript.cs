using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseScript : MonoBehaviour
{
    public bool cleansed;

    private void Update()
    {
        Cleanse();
    }

    void Cleanse()
    {
        if (cleansed)
        {
            if (this.gameObject.tag == "Blockade")
            {
                transform.localScale *= 0.990f;
                Vector3 newPos = transform.position;
                newPos.y -= 1.8f * Time.deltaTime;
                transform.position = newPos;
            }
        }
    }
}
