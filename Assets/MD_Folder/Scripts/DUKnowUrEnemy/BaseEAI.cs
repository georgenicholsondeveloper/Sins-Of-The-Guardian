using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEAI : MonoBehaviour
{
    public GameObject[] damageSphere;
    public GameObject Bullet, head, sight;
    public float cutOff = 45, senseSight = 125, sightInMeters = 20;
    public Transform[] patrolPoints, flyPoint;
    public bool ChasingSiren, isCorupted, alerted;
    public string myAIType;

    private GameObject[] otherCorupted;
    private GameObject player, eye;
    private NavMeshAgent self;
    private Animator anim;
    private Vector3 startPos, ghostPlayer;
    private int pIdx;
    private float attackCD = 0, alertGouge = 0, navSpeed, restTime, disLimit;
    //[SerializeField]
    private bool canPatrol, patroling, lastScan, ajust, reajust, attack, alertCall;
    

    private readonly float shortSightLimit = 2.5f , gaugeLimit = 30, restFor = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  //player ref
        startPos = transform.position; // case where no patroll point
        eye = transform.GetChild(0).gameObject; // for raycasting 
        anim = transform.GetChild(1).GetComponent<Animator>(); //animator component
        otherCorupted = GameObject.FindGameObjectsWithTag("Enemy"); // get all enemies for alert call
        if (GetComponent<NavMeshAgent>() != null) //Siren = no NavMeshAgent
        {
            self = GetComponent<NavMeshAgent>();
            navSpeed = self.speed;
        }
        AssignEnemyType(); //enemy type = hound/grunt/siren
        CheckPatrolPoints(); //patrol points assigned
        DamageSphereOnOff(false); //turn off damage sphere
        
    }

    private void AssignEnemyType()
    {
        myAIType = transform.parent.name;
        if (myAIType.Contains("Grunt") || myAIType.Contains("Grunk"))
        {
            myAIType = "Grunt";
            disLimit = 0.1f;
        }
        else if (myAIType.Contains("Hound"))
        {
            myAIType = "Hound";
            disLimit = 0.7f;
        }
        else if (myAIType.Contains("Siren"))
        {
            myAIType = "Siren";
            transform.LookAt(sight.transform);
        }

        else Debug.LogWarning(" Is ~# " + gameObject.name + " #~ a enemy unit, if yes please add Grunt/Hound/Siren to name ");
    }

    private void CheckPatrolPoints()
    {
        if (patrolPoints == null)
        {
            canPatrol = false;
            AnimReset();
            anim.SetBool("Idle", true);
        }
        else if (patrolPoints.Length > 1)
        {
            canPatrol = false;
            pIdx = 0;
            if (myAIType == "Siren")
            {
                transform.LookAt(patrolPoints[pIdx]);
            }
            else if (myAIType == "Grunt" || myAIType == "Hound")
            {
                if (Vector3.Distance(transform.position, patrolPoints[pIdx].position) > disLimit)
                {
                    patroling = true;
                    self.speed = navSpeed / 3; 
                    self.SetDestination(patrolPoints[pIdx].position);
                    AnimReset();
                    anim.SetBool("Walk", true);
                }
            }

        }

        else { canPatrol = true; patroling = true; pIdx = 0; }
    }

    private void DamageSphereOnOff(bool state)
    {
        foreach (GameObject obj in damageSphere)
            obj.SetActive(state);
    }

    private void SendAlert()
    {
        if (alerted && !alertCall && isCorupted)
        {
            if (myAIType == "Grunt" || myAIType == "Hound")
            {
                foreach (GameObject corupted in otherCorupted)
                {
                    AlertWave(corupted, 8);
                }
            }
            else if (myAIType == "Siren")
            {
                foreach (GameObject corupted in otherCorupted)
                {
                    AlertWave(corupted, 20);
                }
            }
            alertCall = false;
        }
    }

    private void AlertWave(GameObject obj, float dist)
    {
        if (Vector3.Distance(obj.transform.position, gameObject.transform.position) <= dist && !obj.GetComponent<BaseEAI>().alerted)
        {
            if (obj.GetComponent<BaseEAI>().myAIType == "Siren")
                obj.GetComponent<BaseEAI>().AirAlerted();
            else
                obj.GetComponent<BaseEAI>().GroundAlerted();
        }
    }

    public void GroundAlerted()
    {
        alerted = true;
        patroling = false;
        canPatrol = true;
        lastScan = false;
        restTime = 0;
        self.speed = navSpeed;
        AnimReset();
        anim.SetBool("Run", true);
        ghostPlayer = player.transform.position;
    }

    public void AirAlerted()
    {
        alerted = true;
        patroling = false;
        canPatrol = true;
        lastScan = false;
        restTime = 0;
        attack = true;
        ghostPlayer = player.transform.position;
    }

    void Update()
    {
        //print(Vector3.Distance(gameObject.transform.position, player.transform.position) + " " +gameObject.transform.parent.name);
        if (isCorupted)
            CastSightRay();
        AIMovement();
        //PatrolArea();
        //Debug.DrawRay(transform.position, Vector3.forward * 10, Color.blue);
    }

    public void CastSightRay()
    {   //if player is in long / short sight 
        if (inLongEnemySight(player.transform.position) && Vector3.Distance(transform.position, player.transform.position) < sightInMeters ||
            inShortEnemySight(player.transform.position) && Vector3.Distance(transform.position, player.transform.position) < shortSightLimit)
        {
            eye.transform.LookAt(player.transform);
            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position, eye.transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.DrawRay(eye.transform.position, eye.transform.TransformDirection(Vector3.forward) * Vector3.Distance(transform.position, player.transform.position), Color.blue);
                //print(hit.collider);

                if (hit.collider.CompareTag("Player"))
                {
                    ghostPlayer = player.transform.position;
                    if (!alerted)
                    {
                        alertGouge += (1 - (Time.deltaTime * Vector3.Distance(transform.position, player.transform.position)));
                        if (alertGouge > gaugeLimit)
                        {
                            if (myAIType == "Grunt" || myAIType == "Hound")
                            {
                                GroundAlerted();
                            }
                            else if (myAIType == "Siren")
                            {
                                AirAlerted();
                            }
                            SendAlert();
                            print("xx");
                        }
                    }
                    else
                    {
                        attack = false;
                    }
                }
            }
        }
    }

    private void AIMovement()
    {
        if (alerted)
        {
            if (myAIType == "Grunt" || myAIType == "Hound")
            {
                
                transform.LookAt(ghostPlayer);
                self.speed = navSpeed;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                if (Time.time > attackCD)
                {
                    self.SetDestination(ghostPlayer);
                    if (Vector3.Distance(player.transform.position, transform.position) < 1.8f)
                    {
                        AttackPlayer();
                    }

                    else if (Vector3.Distance(transform.position, ghostPlayer) < disLimit * 10 && !lastScan ||
                         self.pathEndPosition != ghostPlayer && !lastScan && Vector3.Distance(transform.position, self.pathEndPosition) < disLimit * 6)
                    {
                        print("Quitting Chase");
                        AnimReset();
                        anim.SetBool("Idle", true);
                        restTime = Time.time + restFor;
                        self.speed = navSpeed / 3;
                        alertGouge = 0;
                        lastScan = true;
                        reajust = false;
                    }

                    else if (lastScan && Time.time > restTime)
                    {
                        print("reset");
                        lastScan = false;
                        alerted = false;
                        alertCall = false;
                        alertGouge = 0;
                        patroling = true;
                    }
                }
                else if (ajust && reajust)
                {
                    if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 2f)
                    {
                        anim.SetBool("Idle", true);
                        
                        //anim.SetBool("Run", false);
                    }
                    else
                    {
                        anim.SetBool("Run", true);
                        /// anim.SetBool("Idle", false);
                    }
                }
                // .8 for attack timing??
                else if (Time.time > attackCD - 1.5 && !ajust)
                {
                    AnimReset();
                    ajust = true;
                    reajust = true;
                    DamageSphereOnOff(false);
                }
            }

            if (myAIType == "Siren")
            {
                transform.LookAt(ghostPlayer);
                if (Time.time > attackCD - 1.6f && attack)
                {
                    attack = false;
                    AnimReset();
                    anim.SetBool("Idle", true);
                    AttackPlayer();
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < sightInMeters && Time.time > attackCD /*&& inLongEnemySight(player.transform.position)*/)
                {
                    AnimReset();
                    anim.SetBool("Attack", true);
                    attack = true;
                    attackCD = Time.time + 2.3f;
                }
                if (!ChasingSiren)
                {
                    if (!inLongEnemySight(player.transform.position) && Vector3.Distance(transform.position, player.transform.position) < sightInMeters && !lastScan)
                    {
                        print("lastScan");
                        lastScan = true;

                    }
                }
                else if (ChasingSiren)
                {
                    if (!inLongEnemySight(player.transform.position) && Vector3.Distance(transform.position, player.transform.position) < sightInMeters)
                    {
                        //later
                    }
                }
            }
        }

        if (patroling)
            PatrolArea();
    }

    private void PatrolArea()
    {

        if (myAIType == "Grunt" || myAIType == "Hound")
        {
            if (canPatrol && Time.time > restTime)
            {
                canPatrol = false;
                AnimReset();
                if (patrolPoints.Length > 1)
                {
                    anim.SetBool("Walk", true);
                }
                self.speed = navSpeed / 3;
                self.SetDestination(patrolPoints[pIdx].position);
            }

            else if (!canPatrol && Vector3.Distance(transform.position, patrolPoints[pIdx].position) < disLimit)
            {
                AnimReset();
                anim.SetBool("Idle", true);
                restTime = Time.time + restFor;
                pIdx++;
                if (pIdx + 1 > patrolPoints.Length)
                    pIdx = 0;
                canPatrol = true;
            }
        }
        else if (myAIType == "Siren")
        {
            if (canPatrol && Time.time > restTime)
            {
                canPatrol = false;
                AnimReset();
                anim.SetBool("Idle", true);

                //restTime = restFor + Time.time;
            }

            else if (!canPatrol && sight.transform.position == patrolPoints[pIdx].position)
            {
                restTime = restFor + Time.time;
                canPatrol = true;
                pIdx++;
                if (pIdx + 1 > patrolPoints.Length)
                    pIdx = 0;
            }

            else if (sight.transform.position != patrolPoints[pIdx].position && !canPatrol)
            {
                transform.LookAt(sight.transform);
                sight.transform.position = Vector3.MoveTowards(sight.transform.position, patrolPoints[pIdx].position, 4 * Time.deltaTime);
            }
        }
        
    }

    private void AttackPlayer()
    {
        if (myAIType == "Siren")
        {
            Vector3 startPos = transform.position + transform.forward * 1.5f;
            GameObject bul = Instantiate(Bullet, startPos, transform.rotation);
            bul.GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position).normalized * 20;
            bul.AddComponent<bulletBehaviour>();
        }
        else if (myAIType == "Grunt" || myAIType == "Hound")
        {
            AnimReset();
            anim.SetBool("Attack", true);
            attackCD = Time.time + 2;
            print("Attacking!!!!");
            ajust = false;
            reajust = false;
            DamageSphereOnOff(true);
        }
    }

    private void SirenAttack() //rewrite Attack playerinto siren /hound / groun attack functions
    {
       
    }

    public bool inLongEnemySight(Vector3 inputPoint)  //Sight Creator ###
    {
        float cosAngle = Vector3.Dot((inputPoint - gameObject.transform.position).normalized, gameObject.transform.forward);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle < cutOff;
    }

    public bool inShortEnemySight(Vector3 inputPoint)
    {
        float cosAngle = Vector3.Dot((inputPoint - gameObject.transform.position).normalized, gameObject.transform.forward);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle < senseSight;
    }

    private void AnimReset()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
    }
}
