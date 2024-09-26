using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            StartCoroutine(Won());
        }
    }

    IEnumerator Won()
    {
        text.text = "You escaped!";
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
