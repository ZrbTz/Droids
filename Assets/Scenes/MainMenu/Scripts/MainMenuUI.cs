using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : StartMenuUI
{
    public void QuitApplication()
    {
        Application.Quit(0);
    }
}
