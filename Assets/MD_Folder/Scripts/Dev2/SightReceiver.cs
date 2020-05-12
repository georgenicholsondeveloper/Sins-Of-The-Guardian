using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SightReceiver : MonoBehaviour //on anything that can be seen (player & enemy)
{
}
  /*  private List<EnemyCheck> inSight;               
    public List<string> monstersInSight;

    void Start()
    {
        if (inSight == null)
            inSight = new List<EnemyCheck>(FindObjectsOfType<EnemyCheck>());
        
        if (gameObject.GetComponent<NavMeshAgent>())
        {
            EnemyCheck eyeCheck = GetComponentInChildren<EnemyCheck>();
            eyeCheck.amHound = true;
            //eyeCheck.daHound = gameObject;
            foreach (EnemyCheck eSight in inSight)
            {
                if (eSight == eyeCheck)
                    inSight.Remove(eSight);
            }
        }
            
    }

    void Update()
    {

        foreach (EnemyCheck eSight in inSight)
        {
            if (eSight.inEnemySight(transform.position)) //am I(the object with this script) in any of the eSight (EnemyChecks)
            {
                if (gameObject.CompareTag("Player")) //am I the player
                    eSight.inESignt = true;          //player is in enemy sight Script

                else if (gameObject.CompareTag("Enemy"))
                {
                    //if (monstersInSight.Count == 0)
                    //    monstersInSight.Add(eSight.gameObject.transform.parent.gameObject.name);

                    //else
                    //{
                    //    bool inList = false;
                    //    foreach (string monster in monstersInSight)
                    //    {
                    //        if (monster == eSight.gameObject.transform.parent.gameObject.name)
                    //        {
                    //            inList = true;
                    //            break;
                    //        }
                    //    }
                    //    if (!inList)
                    //        monstersInSight.Add(eSight.gameObject.transform.parent.gameObject.name);
                    //}
                }
            }
            else
            {
                eSight.inESignt = false;
                //if (gameObject.CompareTag("Player"))
                //    eSight.inESignt = false;
                //else if (gameObject.CompareTag("Enemy"))
                //{
                //    foreach (string monster in monstersInSight)
                //    {
                //        if (monster == eSight.gameObject.transform.parent.gameObject.name)
                //        {

                //            monster.Remove(eSight.gameObject.transform.parent.gameObject.name.ToString);
                //            break;
                //        }
                //    }
                //}
            }
        }
    }
}
*/