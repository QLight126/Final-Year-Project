using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadCode : MonoBehaviour
{
    public InitialValue initial;
    List<GameObject> field = new List<GameObject>();
    List<bool> isFieldUsed = new List<bool>();

    public static string path = "CodeBeingExecuted.cs";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps list updated
        initial = GameObject.FindObjectOfType<InitialValue>();
        field = initial.field;
        isFieldUsed = initial.isFieldUsed;

        initial.variables.Clear();
        initial.code = "";
        for (var i = 0; i < field.Count; i++) // Get the order of the blocks and form a code
        {          
            if (isFieldUsed[i] == true)
            {
                GameObject currentBlock = field[i].transform.GetChild(0).gameObject;

                // Check which command the block represents
                switch (currentBlock.GetComponent<SortOrder>().blockType) 
                {
                    case "setVariable":
                        initial.code += "int " + currentBlock.GetComponent<SetVariable>().variableName + " = " + currentBlock.GetComponent<SetVariable>().value + ";";

                        // Check if the variable is used
                        if (!(initial.variables.Exists(x => x.variableName == currentBlock.GetComponent<SetVariable>().variableName)) && (currentBlock.GetComponent<SetVariable>().variableName != "") && (currentBlock.GetComponent<SetVariable>().value != ""))
                        {
                            initial.variables.Add(new variableUsed(currentBlock.GetComponent<SetVariable>().variableName,int.Parse(currentBlock.GetComponent<SetVariable>().value)));
                        } 
                        break;
                    case "plus":
                        switch (currentBlock.transform.GetChild(3).GetComponent<Dropdown>().value)
                        {
                            case 0:
                                initial.code += currentBlock.GetComponent<Plus>().newValue + " = " + currentBlock.GetComponent<Plus>().firstValue + " + " + currentBlock.GetComponent<Plus>().secondValue + ";";
                                break;
                            case 1:
                                initial.code += currentBlock.GetComponent<Plus>().newValue + " = " + currentBlock.GetComponent<Plus>().firstValue + " - " + currentBlock.GetComponent<Plus>().secondValue + ";";
                                break;
                            case 2:
                                initial.code += currentBlock.GetComponent<Plus>().newValue + " = " + currentBlock.GetComponent<Plus>().firstValue + " * " + currentBlock.GetComponent<Plus>().secondValue + ";";
                                break;
                            case 3:
                                initial.code += currentBlock.GetComponent<Plus>().newValue + " = " + currentBlock.GetComponent<Plus>().firstValue + " / " + currentBlock.GetComponent<Plus>().secondValue + ";";
                                break;
                        }
                        break;
                    case "write":
                        initial.code += currentBlock.GetComponent<Write>().output;
                        break;
                }
            }
            
        }
    }
}
