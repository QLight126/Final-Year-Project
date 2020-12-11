using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Execute : MonoBehaviour
{
    public Camera ProgramCamera;
    public Camera OutputCamera;

    public void Scene1()
    {
        //SceneManager.LoadScene("output");
        ProgramCamera.enabled = false;
        OutputCamera.enabled = true;
    }
}
