using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plus : MonoBehaviour
{
    public GameObject newValueField;
    public GameObject firstValueField;
    public GameObject secondValueField;

    public string newValue;
    public string firstValue;
    public string secondValue;

    // Update is called once per frame
    void Update()
    {
        
        if (newValueField.GetComponent<Text>().text != "") newValue = newValueField.GetComponent<Text>().text;
        if (firstValueField.GetComponent<Text>().text != "") firstValue = firstValueField.GetComponent<Text>().text;
        if (secondValueField.GetComponent<Text>().text != "") secondValue = secondValueField.GetComponent<Text>().text;
    }
}
