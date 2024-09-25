using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    #region State Definition
    enum State
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
    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    private State state = State.IDLE;

    #endregion

    #region Member Functions

    public void Investigate(GameObject other)
    {
        //other.gameObject.transform.position;
    }

    public void Patrol()
    {
        if (gameObject.transform.position != PatrolPoints[currentWaypoint].position)
        {
            agent.destination = PatrolPoints[currentWaypoint].position;
        }
        else if (currentWaypoint == PatrolPoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else { currentWaypoint++; }
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
