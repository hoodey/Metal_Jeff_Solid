using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            StartCoroutine(UpdateText());
        }
    }

    IEnumerator UpdateText()
    {
        text.text = "Press the shift key in order to sneak past enemies!";
        yield return new WaitForSeconds(5f);
        text.text = "";
    }
}
