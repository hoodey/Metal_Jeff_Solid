using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> OnEnterRadius;
    [SerializeField] private UnityEvent OnExitRadius;
    [SerializeField] private UnityEvent<GameObject> OnStayRadius;
    private void OnTriggerEnter(Collider other)
    {
        OnEnterRadius?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitRadius?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        OnStayRadius?.Invoke(other.gameObject);
    }
}
