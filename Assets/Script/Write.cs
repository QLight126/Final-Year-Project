using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Write : MonoBehaviour
{
    public GameObject outputField;
    public string output;

    // Update is called once per frame
    void Update()
    {
        if  (outputField.GetComponent<Text>().text != "") output = outputField.GetComponent<Text>().text;
    }
}
