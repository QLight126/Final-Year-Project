using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneItself : MonoBehaviour
{
    public InitialValue initial;
    public GameObject original;
    public Transform Area;
    List<GameObject> field = new List<GameObject>();
    List<bool> isFieldUsed = new List<bool>();

    public void clone()
    {
            int position = 100;
            // Search for the first unused container
            for (var i = 0; i < field.Count; i++)
            {
                if (initial.isFieldUsed[i] == false)
                {
                    position = i;
                    break;
                }
            }
            GameObject clones = Instantiate(original, new Vector3(0,-20,0), Quaternion.identity);
            clones.GetComponent<SortOrder>().originalContainer = field[position];
            clones.transform.SetParent(field[position].transform,false);
            initial.isFieldUsed[position] = true;
    }

    void Update()
    {
        initial = GameObject.FindObjectOfType<InitialValue>();
        field = initial.field;
        isFieldUsed = initial.isFieldUsed;
    }
}
