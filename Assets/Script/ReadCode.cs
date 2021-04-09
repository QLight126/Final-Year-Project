using System;
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
                    case "if":
                        switch (currentBlock.transform.GetChild(2).GetComponent<Dropdown>().value)
                        {
                            case 0:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " = " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                            case 1:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " != " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                            case 2:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " > " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                            case 3:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " < " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                            case 4:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " >= " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                            case 5:
                                initial.code += "if (" + currentBlock.GetComponent<IfBlock>().comparedValue + " <= " + currentBlock.GetComponent<IfBlock>().comparingValue + " ){ ";
                                break;
                        }
                        break;
                    case "elseIf":
                        switch (currentBlock.transform.GetChild(2).GetComponent<Dropdown>().value)
                        {
                            case 0:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " = " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                            case 1:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " != " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                            case 2:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " > " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                            case 3:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " < " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                            case 4:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " >= " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                            case 5:
                                initial.code += "}else if (" + currentBlock.GetComponent<ElseIfBlock>().comparedValue + " <= " + currentBlock.GetComponent<ElseIfBlock>().comparingValue + " ){ ";
                                break;
                        }
                        break;
                    case "else":
                        initial.code += "}else{";
                        break;
                    case "endIf":
                        initial.code += "}";
                        break;
                    case "setVariable":
                        initial.code += "float " + currentBlock.GetComponent<SetVariable>().variableName + " = " + currentBlock.GetComponent<SetVariable>().value + ";";
                        // Check if the input is numeral
                        if (!(CheckIfNumeral(currentBlock.GetComponent<SetVariable>().value)))
                        {
                            // If not numeral, reset the value field
                            currentBlock.transform.Find("Value").GetComponent<InputField>().Select();
                            currentBlock.transform.Find("Value").GetComponent<InputField>().text = "";
                            currentBlock.GetComponent<SetVariable>().value = "";
                            initial.numeralWarning.SetActive(true); // Pop up a window warning the user to enter number
                        }
                        break;
                    case "plus":
                        // Determine which operation is chosen
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
                }
            }
            
        }
    }

    public bool CheckIfNumeral(string value)
    {
        float number;
        if (!(Single.TryParse(value, out number)) && (value != "") && !(value.Substring(value.Length-1) == "."))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}