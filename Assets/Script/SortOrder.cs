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
        else
        {
            container = transform.parent.gameObject;
        }
    }

    
    public void StartDrag()
    {
        startPosition = transform.position;
        originalContainer = transform.parent.gameObject;
        isDragging = true;
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
                replacedBlock.localPosition = new Vector3(90,20,0);
                transform.SetParent(container.transform, true);// Move the block to new container
                transform.localPosition = new Vector3(90,20,0);// Set the relative position to center of the new container
                container.GetComponent<CanvasGroup>().alpha = 1f;
                container.GetComponent<Image>().color  = Color.white;
            }
            else // There is no block placed in the new container
            {
                transform.SetParent(container.transform, true);// Move the block to new container
                transform.localPosition = new Vector3(90,20,0);// Set the relative position to center of the new container
                container.GetComponent<CanvasGroup>().alpha = 1f;
                container.GetComponent<Image>().color  = Color.white;
                isFieldUsed[int.Parse(originalContainer.name)-1] = false;
                isFieldUsed[int.Parse(container.name)-1] = true;
            }
        }
        else
        {
            transform.position = startPosition;// Send the block back to the original position
        }

        for (var i = 0; i < field.Count; i++)
        {
            field[i].GetComponent<CanvasGroup>().alpha = 1f;
            field[i].GetComponent<Image>().color  = Color.white;
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverContainer = false;
        container.GetComponent<CanvasGroup>().alpha = 1f;
        container.GetComponent<Image>().color  = Color.white;
        // container = null;
    }
}
