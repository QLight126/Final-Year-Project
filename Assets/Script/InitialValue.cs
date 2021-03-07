using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableUsed
{
    public string variableName;
    public int variableValue;

    public variableUsed(string name, int value)
    {
        variableName = name;
        variableValue = value;
    }
}
public class InitialValue : MonoBehaviour
{
    public GameObject Order1, Order2, Order3, Order4, Order5, Order6, Order7, Order8, Order9, Order10;
    public int OriginalOrder;
    public List<GameObject> field = new List<GameObject>();
    public List<bool> isFieldUsed = new List<bool>();
    public List<variableUsed> variables = new List<variableUsed>();
    public string code = "";

    // Start is called before the first frame update
    void Start()
    {
        OriginalOrder = 0;
        field.Add(Order1); field.Add(Order2); field.Add(Order3); field.Add(Order4); field.Add(Order5);
        field.Add(Order6); field.Add(Order7); field.Add(Order8); field.Add(Order9); field.Add(Order10);
        for (var i = 0; i < field.Count; i++)
        {
            isFieldUsed.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


