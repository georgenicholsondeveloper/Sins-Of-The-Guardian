using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlScript : MonoBehaviour
{
    public GameObject cameraOrbit;

    private float rotateSpeed;
    private bool useMouse;
    float v, h;

    void Update()
    {
        if (FindObjectOfType<WrathMask>().aiming)
            rotateSpeed = 15f;
        else
            rotateSpeed = 20f;

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            useMouse = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (useMouse)
        {
            v = -Input.GetAxis("Mouse Y") * (rotateSpeed / 4);
            h = Input.GetAxis("Mouse X") * (rotateSpeed / 4);
        }
        else
        {
            v = Input.GetAxis("JVertical") * rotateSpeed;
            h = Input.GetAxis("JHorizontal") * rotateSpeed;
        }
            Vector3 input = new Vector3(h, v, 0);

        if (input.sqrMagnitude > 0.02f)
        {
          //  if (cameraOrbit.transform.eulerAngles.z + input.y <= 0.1f || cameraOrbit.transform.eulerAngles.z + input.y >= 179.9f)
           //     input.y = 0;

            cameraOrbit.transform.eulerAngles = new Vector3(cameraOrbit.transform.eulerAngles.x + input.y, cameraOrbit.transform.eulerAngles.y + h, cameraOrbit.transform.eulerAngles.z);
        }
    }
}


