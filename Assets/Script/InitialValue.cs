using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableUsed
{
    public string variableName;
    public float variableValue;

    public variableUsed(string name, float value)
    {
        variableName = name;
        variableValue = value;
    }
}
public class InitialValue : MonoBehaviour
{
    public GameObject Order1, Order2, Order3, Order4, Order5, Order6, Order7, Order8, Order9, Order10, Order11, Order12;
    public GameObject numeralWarning;
    public List<GameObject> field = new List<GameObject>();
    public List<bool> isFieldUsed = new List<bool>();
    public List<variableUsed> variables = new List<variableUsed>();
    public string code = "";

    // Initial the field
    void Start()
    {
        field.Add(Order1); field.Add(Order2); field.Add(Order3); field.Add(Order4); field.Add(Order5); field.Add(Order6); 
        field.Add(Order7); field.Add(Order8); field.Add(Order9); field.Add(Order10); field.Add(Order11); field.Add(Order12);
        for (var i = 0; i < field.Count; i++)
        {
            isFieldUsed.Add(false);
        }
    }
}


