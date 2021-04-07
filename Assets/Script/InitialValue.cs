using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject deleteBlock;
    public GameObject numeralWarning, blankWarning, setVariableWarning, endIfWarning, compileWarning;
    public List<GameObject> field = new List<GameObject>();
    public List<bool> isFieldUsed = new List<bool>();
    public List<variableUsed> variables = new List<variableUsed>();
    public string code = "";
    public string path;

    // Initial the field
    void Start()
    {
        path = Application.dataPath;
        field.Add(Order1); field.Add(Order2); field.Add(Order3); field.Add(Order4); field.Add(Order5); field.Add(Order6); 
        field.Add(Order7); field.Add(Order8); field.Add(Order9); field.Add(Order10); field.Add(Order11); field.Add(Order12);
        for (var i = 0; i < field.Count; i++)
        {
            isFieldUsed.Add(false);
        }
        
    }

    // Move blocks one container forward
    public void AutoSort()
    {
        for (var i = 1; i < field.Count; i++)
        {
            if ((isFieldUsed[i] == true) && (isFieldUsed[i-1] == false))
            {
                Transform movingBlock = field[i].transform.GetChild(0);
                movingBlock.SetParent(field[i-1].transform,true);
                movingBlock.localPosition = new Vector3(90,20,0);
                isFieldUsed[i] = false;
                isFieldUsed[i-1] = true;
            }
        }
    }
}


