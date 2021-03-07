using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVariable : MonoBehaviour
{
    public GameObject variableNameField;
    public GameObject valueField;
    public InitialValue initial;
    public string variableName;
    public string value;

    // Update is called once per frame
    void Update()
    {
        initial = GameObject.FindObjectOfType<InitialValue>();

        if (variableNameField.GetComponent<Text>().text != "") variableName = variableNameField.GetComponent<Text>().text;
        //if (valueField.GetComponent<Text>().text != "")value = int.Parse(valueField.GetComponent<Text>().text);
        if (valueField.GetComponent<Text>().text != "") value = valueField.GetComponent<Text>().text;
    }
}
