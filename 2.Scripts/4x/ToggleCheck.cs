using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheck : MonoBehaviour
{
    public Toggle toggle;

    public void Check()
    {
        if(toggle.isOn.Equals(true))
        {
            Cut4ChoiceManager.instance.checkCount++;
        }
        else
        {
            Cut4ChoiceManager.instance.checkCount--;
        }
        Cut4ChoiceManager.instance.ToggleCheck();
    }
}
