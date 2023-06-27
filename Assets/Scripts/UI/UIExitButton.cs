using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIExitButton : UISelectableButton
{
    public void Exit()
    {
        Application.Quit();
    }
}
