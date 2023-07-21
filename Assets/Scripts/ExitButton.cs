using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void exitApps()
    {
        Debug.Log("application already exit");
        Application.Quit();
    }
}
