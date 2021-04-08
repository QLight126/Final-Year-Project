using System.IO;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Execute : MonoBehaviour
{
    public InitialValue initial;
    public bool isCompiled = false;
    public string path = "/Script/run.bat";
    public string codePath = "/Script/CodeBeingExecuted.cpp";
    public string Exepath = "";

    void Start()
    {
        path = Application.dataPath.Replace("scratch_Data", "Assets") + "/Script/run.bat";
        codePath = Application.dataPath.Replace("scratch_Data", "Assets") + "/Script/CodeBeingExecuted.cpp";
        File.WriteAllText(path, "g++ " + codePath + " -o test");
        Exepath = Application.dataPath.Replace("/Assets", "");
        Exepath = Application.dataPath.Replace("/scratch_Data", "");
    }
    public void compile()
    {
        UnityEngine.Debug.Log(path);
        initial = GameObject.FindObjectOfType<InitialValue>();
        string finalCode = "#include<iostream> \n int main(){ " + initial.code;
        
        // Detect whether all text boxs are filled in or not. If not, pop out warning
        bool isFilledAll = true;
        foreach (GameObject textBox in GameObject.FindGameObjectsWithTag("TextBox"))
        {
            if (initial.field.Contains(textBox.transform.parent.parent.gameObject))
            {
                if (textBox.GetComponent<InputField>().text == "")
                {
                    isFilledAll = false;
                }
            }
        }
        if (isFilledAll == false)
        {
            initial.blankWarning.SetActive(true);
            return;
        }
        
        // Check if called variables are set
        bool isVariableSet = true;
        for (var i = 0; i < initial.field.Count;i++)
        {
            if (initial.field[i].transform.childCount > 0)
            {
                GameObject currentBlock = initial.field[i].transform.GetChild(0).gameObject;
                switch (currentBlock.GetComponent<SortOrder>().blockType)
                {
                    case "setVariable":
                        setVariable(currentBlock);
                        break;
                    default:
                        foreach (GameObject textBox in GameObject.FindGameObjectsWithTag("TextBox"))
                        {
                            if (initial.field.Contains(textBox.transform.parent.parent.gameObject))
                            {
                                if (float.TryParse(textBox.GetComponent<InputField>().text, out float number) == false)
                                {
                                    if (!initial.variables.Exists(x => x.variableName == textBox.GetComponent<InputField>().text))
                                    {
                                        isVariableSet = false;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        if (isVariableSet == false)
        {
            initial.setVariableWarning.SetActive(true);
            return;
        }

        // Check if all if blocks are assigned with an end if block
        int ifCount = 0;
        for (var i = 0; i < initial.field.Count;i++)
        {
            if (initial.field[i].transform.childCount > 0)
            {
                GameObject currentBlock = initial.field[i].transform.GetChild(0).gameObject;
                switch (currentBlock.GetComponent<SortOrder>().blockType)
                {
                    case "if":
                        ifCount++;
                        break;
                    case "endIf":
                        ifCount--;
                        break;
                }
            }
        }
        if (ifCount != 0)
        {
            initial.endIfWarning.SetActive(true);
            return;
        }
        
        
        for (var i = 0; i < initial.variables.Count; i++)
        {
            UnityEngine.Debug.Log(initial.variables[i].variableName + " = " + initial.variables[i].variableValue);
            finalCode += "std::cout<< \"" + initial.variables[i].variableName + " = \" <<" + initial.variables[i].variableName + "<<std::endl;";
        }
        File.WriteAllText(codePath, finalCode+"}");

        Process.Start("powershell.exe", path);
        isCompiled = true;
    }


    void Update()
    {   
        initial = GameObject.FindObjectOfType<InitialValue>();
    }

    public void runCode()
    {
        if (isCompiled == true)
        {
            Process.Start("powershell.exe", "-NoExit -Command " + Exepath + "/test.exe");
            isCompiled = false;
        }
        else
        {
            initial.compileWarning.SetActive(true);
            return;
        }
    }

    public void setVariable(GameObject currentBlock)
    {
        // Add the variable to the list
        if (!(initial.variables.Exists(x => x.variableName == currentBlock.GetComponent<SetVariable>().variableName)) && (currentBlock.GetComponent<SetVariable>().variableName != "") && (currentBlock.GetComponent<SetVariable>().value != "") && (currentBlock.GetComponent<SetVariable>().value.Substring(currentBlock.GetComponent<SetVariable>().value.Length-1) != "."))
        {
            initial.variables.Add(new variableUsed(currentBlock.GetComponent<SetVariable>().variableName,float.Parse(currentBlock.GetComponent<SetVariable>().value)));
        } 
    }
}
