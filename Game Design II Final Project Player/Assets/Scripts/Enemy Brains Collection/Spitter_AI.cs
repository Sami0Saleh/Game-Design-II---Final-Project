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

    [SerializeField] MeshRenderer _targetA;
    [SerializeField] MeshRenderer _targetB;

    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _player;

    Vector3 DistanceToWalkPoint;
    public bool _goingToTA = true;
    
    // should change to a system that moves from 2 points and when player enters leaves them


    public float TimeBetweenAttacks;
    bool AttackAlready;

    //States
    public float SightRange;
    public float AttackRange;
    private bool _playerIsInMySight = false;
    private bool _playerInAttackRange = false;

    private void Start()
    {
        
       _targetA.enabled = false;
       _targetB.enabled = false;
    }
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
        else if (_playerIsInMySight && !_playerInAttackRange)
        {
            ChasePlayer();
        }
        else if(_playerIsInMySight && _playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        Debug.Log("Im Patroling");
        if (_goingToTA)
        {
            
            SpitterAgent.SetDestination(_targetA.gameObject.transform.position);
            DistanceToWalkPoint = transform.position - _targetA.gameObject.transform.position;
        }
        else if (!_goingToTA)
        {
            SpitterAgent.SetDestination(_targetB.gameObject.transform.position);
            DistanceToWalkPoint = transform.position - _targetB.gameObject.transform.position;
        }

        if (DistanceToWalkPoint.magnitude < 2f)
        {
            _goingToTA = !_goingToTA;
        }

    }

    private void ChasePlayer() // great
    {
        Debug.Log("Im Chasing");
        SpitterAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("Im attacking");
        //make sure the enemey does NOT MOVE
       
        SpitterAgent.SetDestination(player.position); transform.LookAt(player);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFist")
        {
            Debug.Log("Been Attacked");
            TakeDamage(1);
        }
    }
}
