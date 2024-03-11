using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.PackageManager;

public class Spitter_AI : MonoBehaviour // Sort later what needs to be public and what does not, and probbaly rewrite half of the names cuz those aren't professional
{
    private int _spitterHealthPoint = 3;

    [SerializeField] NavMeshAgent SpitterAgent; 
    [SerializeField] Transform player; 
    [SerializeField] GameObject Smoohta;

    [SerializeField] GameObject _targetA;
    [SerializeField] GameObject _targetB;

    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _player; 


    public Vector3 _moveTarget; 
    private bool _goingToTarget = true;
    public float GoingToRange; // why is this float
    
    // should change to a system that moves from 2 points and when player enters leaves them


    public float TimeBetweenAttacks;
    bool AttackAlready;

    //States
    public float SightRange;
    public float AttackRange;
    private bool _playerIsInMySight;
    private bool _playerInAttackRange;

    void Update()
    {
        enemeyState();
    }

    private void enemeyState()
    {
        //Check for Sight and Attack Range
        _playerIsInMySight = Physics.CheckSphere(transform.position, SightRange, _player);
        _playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, _player);

        if (!_playerIsInMySight && !_playerInAttackRange)
        {
            Patroling();
        }

        if (_playerIsInMySight && !_playerInAttackRange)
        {
            ChasePlayer();
        }

        if(_playerIsInMySight && _playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!_goingToTarget)
        {
            // SearchWhereToGO();
            // SpitterAgent.SetDestination(); should move from target A to Target B and switch
            if (_goingToTarget)
            {
                SpitterAgent.SetDestination(_moveTarget);
            }

            Vector3 DistanceToWalkPoint = transform.position - _moveTarget;

            //Reached WayPoint
            if (DistanceToWalkPoint.magnitude < 1f)
            {
                _goingToTarget = false;
            }

        }
    }
    private void SearchWhereToGO() // irelevent
    {
        //Generate a Point in Range to go there
        float randomZ = Random.Range(-GoingToRange, GoingToRange);
        float randomx = Random.Range(-GoingToRange, GoingToRange);

        _moveTarget = new Vector3 (transform.position.z + randomZ, transform.position.x + randomx);
        if (Physics.Raycast(_moveTarget, -transform.up, 2f, _ground))
        {
            _goingToTarget = true;
        }

    }    

    private void ChasePlayer() // great
    {
        SpitterAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //make sure the enemey does NOT MOVE
        SpitterAgent.SetDestination(transform.position); // why shouldnt the enemy move while attacking?
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
        Debug.Log(_spitterHealthPoint);
        _spitterHealthPoint--;
        if (_spitterHealthPoint <= 0)
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
