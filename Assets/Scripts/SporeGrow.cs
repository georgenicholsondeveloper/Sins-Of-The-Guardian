using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeGrow : MonoBehaviour
{
    bool grow, activateRed;
    public bool shrink;
    float x = 1;
    Vector3 startVector, currentVector;
    [SerializeField]
    Material material;

    private void Start()
    {

        startVector = transform.localScale * 3;
        currentVector = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            grow = true;

        if (grow)
        {
            if (transform.localScale.x <= startVector.x)
            {
                currentVector.x += 0.007f;
                currentVector.z += 0.007f;
                currentVector.y += 0.007f;
            }
            else
                activateRed = true;
        }


        if (grow)
        {
            
                material.color = new Vector4(1, x, x, 1);
                if(x > -1f)
                    x -= 0.005f;

        }




        transform.localScale = currentVector;
    }


    public void Shrink()
    {
       

        print("hwueh");
    }

}
