using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour  //rename to HoundAI
{
    public Transform target;
    private NavMeshAgent agent;
    public Vector3 startPos;
    public bool chilling;
    
    private void Start(){
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        chilling = true;
    }

    void Update() {
        if (target) {
            agent.SetDestination(target.position); //target aquired - start the hunt
            chilling = false;
        }
        if (!chilling && !target) {
            agent.SetDestination(startPos);       // no target - reset
            chilling = true;
        }
    }
    
    //private void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag("Player")) {
    //        //Destroy(other.gameObject); //do dmg to player
    //        agent.SetDestination(startPos); //reset position 
    //    }
    //}
}
