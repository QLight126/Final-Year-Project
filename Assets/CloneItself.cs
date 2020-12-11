using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneItself : MonoBehaviour
{
    public GameObject original;

    public void clone()
    {
        GameObject clones = Instantiate(original, transform.position, transform.rotation);
    }
   
}
