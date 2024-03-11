using System.Collections; // useless?
using System.Collections.Generic; // useless?
using UnityEngine.UI; // useless?
using UnityEngine;
using UnityEngine.AI;

public class Spitter_AI : MonoBehaviour // Sort later what needs to be public and what does not, and probbaly rewrite half of the names cuz those aren't professional
{
    public bool clicked = false; // delete

    public int SpitterHealthPoint = 3;

    [SerializeField] NavMeshAgent SpitterAgent; // That's ME
    [SerializeField] Transform player; // There you are Player
    [SerializeField] GameObject Smoohta;

    public LayerMask IsDisGround;
    public LayerMask WhatIsPlayer; // what is ground? What is Player?


    // Patroling / Searching/ Wasting my life for noting :'c
    public Vector3 GoingTo; // where do I go?
    bool AmGoingThere; // Am I going?
    public float GoingToRange; // How Far is it where I need to go?


    // Attacking/ Murder/ Death, Destruction, Domination!
    public float TimeBetweenAttacks;
    bool AttackAlready;

    //States
    public float SightRange;
    public float AttackRange;
    public bool PlayerIsInMySight;
    public bool PlayerInAttackRange;

    void Update()
    {
        enemeyState();
    }

    private void enemeyState()
    {
        //Check for Sight and Attack Range
        PlayerIsInMySight = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!PlayerIsInMySight && !PlayerInAttackRange)
        {
            Patroling();
        }

        if (PlayerIsInMySight && !PlayerInAttackRange)
        {
            ChasePlayer();
        }

        if(PlayerIsInMySight && PlayerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!AmGoingThere)
        {
            SearchWhereToGO();
        }
        if (AmGoingThere)
        {
            SpitterAgent.SetDestination(GoingTo);
        }
            
        Vector3 DistanceToWalkPoint = transform.position - GoingTo;

        //Reached WayPoint
        if (DistanceToWalkPoint.magnitude <1f) 
        { 
            AmGoingThere = false;
        }

    }
    private void SearchWhereToGO()
    {
        //Generate a Point in Range to go there
        float randomZ = Random.Range(-GoingToRange, GoingToRange);
        float randomx = Random.Range(-GoingToRange, GoingToRange);

        GoingTo = new Vector3 (transform.position.z + randomZ, transform.position.x + randomx);
        if (Physics.Raycast(GoingTo, -transform.up, 2f, IsDisGround))
        {
            AmGoingThere = true;
        }

    }    

    private void ChasePlayer()
    {
        SpitterAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //make sure the enemey does NOT MOVE
        SpitterAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!AttackAlready)
        {
            ////////////////////////////////////////////////
            // WRITE HERE WHAT KIND OF AN ATTACK IT IS!!!
            // V ATTACK CODE HERE V
            Instantiate(Smoohta, transform.position, transform.rotation);
            /////////////////////////////////////////////////
            AttackAlready = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        AttackAlready= false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(SpitterHealthPoint);
        SpitterHealthPoint--;
        if (SpitterHealthPoint <= 0)
        {
           Invoke(nameof(DestroySpitter), 0.5f);
        }
    }
    private void DestroySpitter()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() // SHOULD BE REMOVED
    {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, AttackRange);
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, SightRange);

    }

    
    


}
