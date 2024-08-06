using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseContinueScene : MonoBehaviour
{
    public MenuScript menuScript;
    public GameObject background;

    public void PauseContinue()
    {
        if (!menuScript.isOpen)
        {
            background.SetActive(!menuScript.isOpen);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            background.SetActive(!menuScript.isOpen);
        }
    }
}
