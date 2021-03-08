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
    public Camera ProgramCamera;
    public Camera OutputCamera;
    public static string path = "Assets/Script/CodeBeingExecuted.cpp";
    public static string Exepath = "Assets/Script/test";


    public void Scene1()
    {
        //SceneManager.LoadScene("output");
        ProgramCamera.enabled = false;
        OutputCamera.enabled = true;
    }

    public void writeCode()
    {
        initial = GameObject.FindObjectOfType<InitialValue>();
        string finalCode = "#include<iostream> \n int main(){ " + initial.code;
        

        for (var i = 0; i < initial.field.Count; i++)
        {
            if (initial.isFieldUsed[i] == true)
            {
                GameObject currentBlock = initial.field[i].transform.GetChild(0).gameObject;

                // Check which command the block represents
                if (currentBlock.GetComponent<SortOrder>().blockType == "plus") 
                {
                    for (var j = 0; j < initial.variables.Count; j++)
                    {
                        if (initial.variables[j].variableName == currentBlock.GetComponent<Plus>().newValue)
                        {
                            float firstValue;
                            float secondValue;
                            int symbol = currentBlock.transform.GetChild(3).GetComponent<Dropdown>().value; // Get the option from dropbox

                            // Decide if the input are integers or variables
                            switch (float.TryParse(currentBlock.GetComponent<Plus>().firstValue, out firstValue))
                            {
                                case true:
                                    switch (float.TryParse(currentBlock.GetComponent<Plus>().secondValue, out secondValue))
                                    {
                                        case true:
                                            switch (symbol)
                                            {
                                                case 0:
                                                    initial.variables[j].variableValue = firstValue + secondValue;
                                                    break;
                                                case 1:
                                                    initial.variables[j].variableValue = firstValue - secondValue;
                                                    break;
                                                case 2:
                                                    initial.variables[j].variableValue = firstValue * secondValue;
                                                    break;
                                                case 3:
                                                    initial.variables[j].variableValue = firstValue / secondValue;
                                                    break;
                                            }
                                            break;
                                        case false:
                                            secondValue = initial.variables.Find(x => x.variableName == currentBlock.GetComponent<Plus>().secondValue).variableValue;
                                            switch (symbol)
                                            {
                                                case 0:
                                                    initial.variables[j].variableValue = firstValue + secondValue;
                                                    break;
                                                case 1:
                                                    initial.variables[j].variableValue = firstValue - secondValue;
                                                    break;
                                                case 2:
                                                    initial.variables[j].variableValue = firstValue * secondValue;
                                                    break;
                                                case 3:
                                                    initial.variables[j].variableValue = firstValue / secondValue;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case false:
                                {
                                    switch(float.TryParse(currentBlock.GetComponent<Plus>().secondValue, out secondValue))
                                    {
                                        case true:
                                            firstValue = initial.variables.Find(x => x.variableName == currentBlock.GetComponent<Plus>().firstValue).variableValue;
                                            switch (symbol)
                                            {
                                                case 0:
                                                    initial.variables[j].variableValue = firstValue + secondValue;
                                                    break;
                                                case 1:
                                                    initial.variables[j].variableValue = firstValue - secondValue;
                                                    break;
                                                case 2:
                                                    initial.variables[j].variableValue = firstValue * secondValue;
                                                    break;
                                                case 3:
                                                    initial.variables[j].variableValue = firstValue / secondValue;
                                                    break;
                                            }
                                            break;
                                        case false:
                                            firstValue = initial.variables.Find(x => x.variableName == currentBlock.GetComponent<Plus>().firstValue).variableValue;
                                            secondValue = initial.variables.Find(x => x.variableName == currentBlock.GetComponent<Plus>().secondValue).variableValue;
                                            switch (symbol)
                                            {
                                                case 0:
                                                    initial.variables[j].variableValue = firstValue + secondValue;
                                                    break;
                                                case 1:
                                                    initial.variables[j].variableValue = firstValue - secondValue;
                                                    break;
                                                case 2:
                                                    initial.variables[j].variableValue = firstValue * secondValue;
                                                    break;
                                                case 3:
                                                    initial.variables[j].variableValue = firstValue / secondValue;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                }                                    
                            }
                        }
                    }
                }
            } 
        }
        for (var i = 0; i < initial.variables.Count; i++)
        {
            UnityEngine.Debug.Log(initial.variables[i].variableName + " = " + initial.variables[i].variableValue);
            finalCode += "std::cout<<"+initial.variables[i].variableName+"<<std::endl;";
        }
        File.WriteAllText(path, finalCode+"}");

        // StartCoroutine(PostRequest("https://api.jdoodle.com/v1/execute",finalCode));
    }

    void Update()
    {   
        initial = GameObject.FindObjectOfType<InitialValue>();
    }

    // IEnumerator PostRequest(string url, string script)
    // {
    //     WWWForm form = new WWWForm();
        
    

    //     UnityWebRequest uwr = UnityWebRequest.Post(url, form);
    //     yield return uwr.SendWebRequest();

    //     if (uwr.isNetworkError)
    //     {
    //         UnityEngine.Debug.Log("Error While Sending: " + uwr.error);
    //     }
    //     else
    //     {
    //         UnityEngine.Debug.Log("Received: " + uwr.downloadHandler.text);
    //     }
    // }
     


}
