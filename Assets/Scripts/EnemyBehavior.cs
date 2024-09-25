using System.Collections;
using System.Collections.Generic;
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
    public int currentWaypoint = 0;
    private NavMeshAgent agent;
    public State state = State.IDLE;

    #endregion

    #region Member Functions

    public void Investigate(GameObject other)
    {
        //other.gameObject.transform.position;
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
        /*if (Mathf.Abs(gameObject.transform.position.x - PatrolPoints[currentWaypoint].position.x) > .2f && Mathf.Abs(gameObject.transform.position.z - PatrolPoints[currentWaypoint].position.z) <= .2f)
        {
            agent.destination = PatrolPoints[currentWaypoint].position;
        }*/
        
        Vector3 destPos = new Vector3(PatrolPoints[currentWaypoint].position.x, transform.position.y, PatrolPoints[currentWaypoint].position.z);

        if (transform.position == destPos)
        {
            UpdatePatrolPoint();
        }

        agent.SetDestination(destPos);

    }

    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    #endregion

}
