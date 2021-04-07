using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfBlock : MonoBehaviour
{
    public GameObject comparedValueField;
    public GameObject comparingValueField;
    public InitialValue initial;
    public string comparedValue;
    public string comparingValue;

    void Update()
    {
        initial = GameObject.FindObjectOfType<InitialValue>();

        if (comparedValueField.GetComponent<Text>().text != "") comparedValue = comparedValueField.GetComponent<Text>().text;
        if (comparingValueField.GetComponent<Text>().text != "") comparingValue = comparingValueField.GetComponent<Text>().text;
    }
}
