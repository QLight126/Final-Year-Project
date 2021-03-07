using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortOrder : MonoBehaviour
{
    public InitialValue initial;
    List<GameObject> field = new List<GameObject>();
    List<bool> isFieldUsed = new List<bool>();
    public string blockType;

    private bool isDragging = false;
    private bool isOverContainer = false;

    public GameObject container;
    public GameObject originalContainer;
    private Vector3 startPosition;
    private Vector3 offset;

    void Update()
    {
        initial = GameObject.FindObjectOfType<InitialValue>();
        field = initial.field;
        isFieldUsed = initial.isFieldUsed;
        if (isDragging)
        {
            offset=transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.position -= offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    
    public void StartDrag()
    {
        startPosition = transform.position;
        originalContainer = transform.parent.gameObject;
        isDragging = true;
//        Debug.Log("mouse "+Input.mousePosition+" block "+transform.position);
    }

    public void EndDrag()
    {
        isDragging = false;
        initial.code = "";
        initial.variables.Clear();

        if (isOverContainer && container != originalContainer) // The block is moved to a new container
        {
            if (isFieldUsed[int.Parse(container.name)-1] == true) // There is a block already placed in the container
            {
                Transform replacedBlock = container.transform.GetChild(0);
                replacedBlock.SetParent(originalContainer.transform,true);
                replacedBlock.localPosition = new Vector3(0,0,0);
                transform.SetParent(container.transform, true);// Move the block to new container
                transform.localPosition = new Vector3(0,0,0);// Set the relative position to center of the new container
                container.GetComponent<CanvasGroup>().alpha = 1f;
                container.GetComponent<Image>().color  = Color.white;
            }
            else // There is no block placed in the new container
            {
                transform.SetParent(container.transform, true);// Move the block to new container
                transform.localPosition = new Vector3(0,0,0);// Set the relative position to center of the new container
                container.GetComponent<CanvasGroup>().alpha = 1f;
                container.GetComponent<Image>().color  = Color.white;
                isFieldUsed[int.Parse(originalContainer.name)-1] = false;
                isFieldUsed[int.Parse(container.name)-1] = true;
                //Debug.Log("sorted");
            }
        }
        else
        {
            //Debug.Log("not sorted");
            transform.position = startPosition;// Send the block back to the original position
        }
        //Debug.Log("mouse "+Input.mousePosition+" block "+transform.position);

        // for (var i = 0; i < field.Count; i++) // Get the order of the blocks and form a code
        // {
        //     // field[i].GetComponent<CanvasGroup>().alpha = 1f;
        //     // field[i].GetComponent<Image>().color  = Color.white;
        //     // Debug.Log("Order " + field[i] + " is cleared");
            
        //     if (isFieldUsed[i] == true)
        //     {
        //         GameObject currentBlock = field[i].transform.GetChild(0).gameObject;
        //         //Debug.Log(currentBlock.GetComponent<SortOrder>().blockType);

        //         // Check which command the block represents
        //         switch (currentBlock.GetComponent<SortOrder>().blockType) 
        //         {
        //             case "setVariable":
        //                 initial.code += "int " + currentBlock.GetComponent<SetVariable>().variableName + " = " + currentBlock.GetComponent<SetVariable>().value + ";";

        //                 // Check if the variable is used
        //                 bool used = false;
        //                 for (var j = 0; j < initial.variables.Count-1; j++)
        //                 {
        //                     if (initial.variables[i] == currentBlock.GetComponent<SetVariable>().variableName)
        //                     {
        //                         used = true;
        //                     }        
        //                 }
        //                 if (used == false) initial.variables.Add(currentBlock.GetComponent<SetVariable>().variableName);
        //                 break;
        //             case "plus":
        //                 initial.code += currentBlock.GetComponent<Plus>().newValue + " = " + currentBlock.GetComponent<Plus>().firstValue + " + " + currentBlock.GetComponent<Plus>().secondValue + ";";
        //                 break;
        //             case "write":
        //                 initial.code += currentBlock.GetComponent<Write>().output;
        //                 break;
        //         }
        //     }
            
        // }
        Debug.Log(initial.code);
        for (var i = 0; i < initial.variables.Count; i++)
        {
            Debug.Log(initial.variables[i]);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        container = collision.gameObject;
        if (container != originalContainer)
        {
            isOverContainer = true;
            container.GetComponent<CanvasGroup>().alpha = 0.8f;
            container.GetComponent<Image>().color  = Color.grey;
//            Debug.Log("new container is " + container);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverContainer = false;
        container.GetComponent<CanvasGroup>().alpha = 1f;
        container.GetComponent<Image>().color  = Color.white;
        container = null;
//        Debug.Log("exit container");
    }
}
