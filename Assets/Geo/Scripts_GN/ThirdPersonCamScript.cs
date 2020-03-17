using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamScript : MonoBehaviour
{
    [SerializeField]
    private Transform followStart, followEnd;

    private Vector3 characterOffset = Vector3.zero;

    public bool isInFog;
    public Transform cameraOrbit, target;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);

        if (isInFog)
        {
            cameraOrbit.transform.rotation = Quaternion.Euler(cameraOrbit.transform.rotation.x + 180f, cameraOrbit.transform.rotation.y, cameraOrbit.transform.rotation.z);
            isInFog = false;
        }

        if (FindObjectOfType<WrathMask>().aiming)
            target.position = Vector3.Lerp(target.position, followEnd.position, 2f * Time.unscaledDeltaTime);
        else
            target.position = Vector3.Lerp(target.position, followStart.position, 4f * Time.unscaledDeltaTime);


        cameraOrbit.position = target.position;
        transform.LookAt(target.position);
    }
}
