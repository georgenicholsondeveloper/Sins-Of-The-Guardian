using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeScript : MonoBehaviour
{
    [SerializeField]
    Material material, material2;

    private float x;

    public bool shrink;

    void Update()
    {
        if (shrink)
        {
            material.color = new Vector4(1, x, x, 1);
            material.SetColor("_EmissionColor", new Vector4(1, x, x, 1));
            material2.color = new Vector4(1, x, x, 1);

            if (x < 1f)
                x += 0.01f;
        }
    }
}
