using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    #region State Definition
    public enum State
    {
        IDLE,
        PATROL,
        INVESTIGATE,
        PURSUE
    }

    #endregion

    #region Member Variables

    [SerializeField] private float eSpeed;
    [SerializeField] private Transform[] PatrolPoints;
    [SerializeField] private bool patrols;
    [SerializeField] private Transform Player;
    [SerializeField] float visionDistance;
    public int currentWaypoint = 0;
    public State state = State.IDLE;
    private NavMeshAgent agent;
    private Vector3 spawnPos;
    private bool PlayerInSight = false;

    #endregion

    #region Member Functions

    public void Investigate(GameObject other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc.Sneaking == false)
            {
                state = State.INVESTIGATE;
                Vector3 newDest = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
                agent.SetDestination(newDest);
                StartCoroutine(ReturnToNormal());
            }
        }
    }

    IEnumerator ReturnToNormal()
    {
        if (state == State.INVESTIGATE)
        {
            yield return new WaitForSeconds(5f);
            agent.SetDestination(spawnPos);
            state = State.IDLE;
        }

        if (state == State.PURSUE && !PlayerInSight)
        {
            yield return new WaitForSeconds(5f);
            agent.SetDestination(spawnPos);
            state = State.IDLE;
        }
    }

    public void UpdatePatrolPoint()
    {
        if (currentWaypoint == PatrolPoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }
    }
    public void Patrol()
    {   
        Vector3 destPos = new Vector3(PatrolPoints[currentWaypoint].position.x, transform.position.y, PatrolPoints[currentWaypoint].position.z);

        if (transform.position == destPos)
        {
            UpdatePatrolPoint();
        }

        agent.SetDestination(destPos);

    }

    public void Pursue(Transform t)
    {
        Vector3 newDest = new Vector3(t.position.x, transform.position.y, t.position.z);
        agent.SetDestination(newDest);
        StartCoroutine(ReturnToNormal());
    }

    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {




        switch (state)
        {
            case State.IDLE:
                //Begin patrolling if enemy patrols
                if (patrols)
                    state = State.PATROL;
                break;
            case State.PATROL:
                Patrol();
                break;
            case State.INVESTIGATE:
                break;
            case State.PURSUE:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;
        Vector3 forwardDirection = transform.forward;

        float dot = Vector3.Dot(forwardDirection, directionToPlayer);

        if (dot > 0.5f)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance);
            if (hit.transform.gameObject == Player.gameObject)
            {
                Debug.Log(gameObject.name + " sees the player!");
                state = State.PURSUE;
                Pursue(hit.transform);
                PlayerInSight = true;
            }
            else
            {
                PlayerInSight = false;
            }
        }
    }

    #endregion

}
