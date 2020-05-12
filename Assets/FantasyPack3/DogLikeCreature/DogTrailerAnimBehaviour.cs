using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTrailerAnimBehaviour : MonoBehaviour
{
    public GameObject MotherDog, Kid1, Kid2, momTarget, k1Target, k2Target, c2, LWolf, lWTarget;
    public Animator mom, k1, k2, wl;
    Vector4 corCol = new Vector4(0.55506f, 0.17889f, 0.17889f, 1);
    public int animStateMom = 0, k1animState, k2animState, leWof = 0;
    private float cdTimer, kdTimer = 1000000, speed = 1.8f;
    private int iK1 = 0, iK2 = 0, iL = 0;


    void Start()
    {
        mom = MotherDog.transform.GetChild(0).GetComponent<Animator>();
        k1 = Kid1.transform.GetChild(0).GetComponent<Animator>();
        k2 = Kid2.transform.GetChild(0).GetComponent<Animator>();
        wl = LWolf.transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        MomMonsterController();
        KidController();
    }

    private void KidController()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            k1animState = 1;
            k2animState = 1;
        }
        switch (k1animState)
        {
            case 1: //start kid1 anim
                k1.SetBool("Walk", false);
                kdTimer = Time.time + 3.5f;
                k1animState++;
                break;
            case 2: // stop looking at k2
                if (Time.time > kdTimer - 1.6f)
                {
                    k1.SetBool("turn_left", false);
                    k1animState++;
                }
                break;
            case 3: //start run
                if (Time.time > kdTimer - 0.7f)
                {
                    k1.SetBool("Walk", true);
                    k1animState++;
                }
                break;
            case 4:
                if (Time.time > kdTimer - 0.45f)
                {
                    k1animState++;
                }
                break;
            case 5: //running
                var targetRotation = Quaternion.LookRotation(k1Target.transform.GetChild(iK1).transform.position - Kid1.transform.position);
                Kid1.transform.rotation = Quaternion.Slerp(Kid1.transform.rotation, targetRotation, 3.1f * Time.deltaTime);
                Kid1.transform.position = Vector3.MoveTowards(Kid1.transform.position, k1Target.transform.GetChild(iK1).transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(Kid1.transform.position, k1Target.transform.GetChild(iK1).transform.position) < 0.1f)
                {
                    iK1++;
                    if (iK1 == k1Target.transform.childCount)
                        k1animState++;
                }
                break;  
        }
        switch (leWof)
        {
            case 1:
                var targetRotations = Quaternion.LookRotation(lWTarget.transform.GetChild(iL).transform.position - LWolf.transform.position);
                LWolf.transform.rotation = Quaternion.Slerp(LWolf.transform.rotation, targetRotations, 3.1f * Time.deltaTime);
                LWolf.transform.position = Vector3.MoveTowards(LWolf.transform.position, lWTarget.transform.GetChild(iL).transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(LWolf.transform.position, lWTarget.transform.GetChild(iL).transform.position) < 0.3f)
                {
                    print("asda");
                    iL++;
                    if (iL == lWTarget.transform.childCount)
                        leWof++;
                }
                break;
            case 2:
                wl.SetBool("Walk", false);
                break;
        }
        

        switch (k2animState)
        {
            case 1:
                if (Time.time > kdTimer - 3f)
                {
                    k2.SetBool("Walk", false);
                    k2animState++;
                    cdTimer = Time.time + 3.70f;
                }
                break;
            case 2:
                if (Time.time > cdTimer)
                {
                    k2.SetBool("Walk", true);
                    k2animState++;
                }
                break;
            case 3:
                var targetRotation = Quaternion.LookRotation(k2Target.transform.GetChild(iK2).transform.position - Kid2.transform.position);
                Kid2.transform.rotation = Quaternion.Slerp(Kid2.transform.rotation, targetRotation, 3.1f * Time.deltaTime);
                Kid2.transform.position = Vector3.MoveTowards(Kid2.transform.position, k2Target.transform.GetChild(iK2).transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(Kid2.transform.position, k2Target.transform.GetChild(iK2).transform.position) < 0.1f)
                {
                    iK2++;
                    if (iK2 == k2Target.transform.childCount)
                        k2animState++;
                }
                break;
        }
    }

    private void MomMonsterController()
    {
        if (Input.GetKeyDown(KeyCode.X))
            animStateMom = 1;
        if (Input.GetKeyDown(KeyCode.C))
        {
            mom.SetBool("turn_left", true);
            mom.SetBool("turn_right", false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            mom.SetBool("turn_left", false);
            mom.SetBool("turn_right", false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            mom.SetBool("turn_left", false);
            mom.SetBool("turn_right", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            wl.SetBool("Walk", true);
            leWof = 1;
        }


        switch (animStateMom)
        {
            case 1: //Wakes up                            
                mom.SetBool("turn_left", true);
                animStateMom++;
                cdTimer = Time.time + 1.7f;
                break;
            case 2: //Done Waking up 
                if (Time.time > cdTimer - 1)
                {
                    mom.SetBool("turn_left", false);
                    animStateMom++;
                }
                break;
            case 3:
                if (Time.time > cdTimer)
                {
                    animStateMom++;
                    
                    cdTimer = Time.time + 1;
                }
                break;
            case 4:
                if (Time.time > cdTimer) 
                {
                    animStateMom++;
                    mom.SetBool("Walk", true);
                }
                break;
            case 5:
                Vector3 momPos = MotherDog.transform.position;
                Vector3 targetPos = new Vector3(momTarget.transform.position.x, MotherDog.transform.position.y, momTarget.transform.position.z);
                MotherDog.transform.position = Vector3.MoveTowards(momPos, targetPos, 1.2f * Time.deltaTime);
                var targetRotation = Quaternion.LookRotation(targetPos - momPos);
                mom.transform.rotation = Quaternion.Slerp(mom.transform.rotation, targetRotation, 3.1f * Time.deltaTime);
                if (Vector3.Distance(MotherDog.transform.position, momTarget.transform.position) < 0.5f)
                {
                    animStateMom++;
                    mom.SetBool("Walk", false);
                }
                break;
            case 6:
                animStateMom++;
                var targetRotation1 = Quaternion.LookRotation(c2.transform.position - MotherDog.transform.position);
                mom.transform.rotation = Quaternion.Slerp(mom.transform.rotation, targetRotation1, 3.1f * Time.deltaTime);
                break;
        }

    }
}

