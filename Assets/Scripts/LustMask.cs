using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustMask : MonoBehaviour
{
    public bool activateAbility = false, lustActive;

    [SerializeField]
    private Camera playerCam;

    private float cooldown;
    private GameObject targettedEnemy;
    private Dog_ctrl dogManipulationScript;
    private bool enemySelected;


    void Update()
    {
        SelectEnemy();
        ControlEnemy();
        MaskCooldown();
    }

    void ControlEnemy()
    {
        if (enemySelected && lustActive)
        {
            if (targettedEnemy.GetComponent<Dog_ctrl>())
                dogManipulationScript.Manipulating = true;
        }
    }

    void SelectEnemy()
    {
        if (activateAbility)
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward * 30f, out RaycastHit hit) && (hit.transform.parent.tag == "Enemy" || hit.transform.tag == "Enemy"))
            {
                targettedEnemy = hit.transform.parent.gameObject;
                dogManipulationScript = targettedEnemy.GetComponent<Dog_ctrl>();
                enemySelected = true;
                playerCam.gameObject.SetActive(false);
                lustActive = true;             
                cooldown = Time.time + 5f;
                HaltAnimation();
            }
            activateAbility = false;
        }
    }

    void HaltAnimation()
    {
        Animator anim = GetComponentInChildren<Animator>();

        anim.SetFloat("Speed", 0);
    }

    void MaskCooldown()
    {
        if (lustActive)
        {
            if(Time.time > cooldown)
            {
                dogManipulationScript.Manipulating = false;
                if(Time.time > cooldown + 0.4f)
                  lustActive = false;
                dogManipulationScript.transform.gameObject.GetComponentInChildren<Camera>().enabled = false;
                playerCam.gameObject.SetActive(true);               
            }
        }
    }

    public void ActivateAbility(bool shouldActivate)
    {
        if (shouldActivate)
            activateAbility = true;
        else
            activateAbility = false;
    }
}
