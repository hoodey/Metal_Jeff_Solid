using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    [SerializeField] private float speed;
    [SerializeField] private Transform[] PatrolPoints;
    [SerializeField] private bool patrols;

    private State state;

    #endregion

    #region Member Functions

    public void Investigate(GameObject other)
    {
        //other.gameObject.position
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                //Begin patrolling if enemy patrols
                break;
            case State.PATROL:
                break;
            case State.INVESTIGATE:
                break;
            case State.PURSUE:
                break;
            default:
                break;
        }
    }
}
