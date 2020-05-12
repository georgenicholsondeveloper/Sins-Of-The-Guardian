using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            if (targettedEnemy.GetComponentInChildren<Dog_ctrl>())
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
                targettedEnemy.GetComponentInChildren<BaseEAI>().enabled = false;
                targettedEnemy.GetComponentInChildren<NavMeshAgent>().enabled = false;
                dogManipulationScript = targettedEnemy.GetComponentInChildren<Dog_ctrl>();
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
                targettedEnemy.GetComponentInChildren<BaseEAI>().enabled = true;
                targettedEnemy.GetComponentInChildren<NavMeshAgent>().enabled = true;
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
