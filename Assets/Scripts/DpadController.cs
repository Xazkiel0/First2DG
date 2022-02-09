using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DpadController : MonoBehaviour
{
    public PlayerAction playerStuff;

    public void dpadButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        switch (name)
        {
            case "Top":
                playerStuff.addCombo(0);
                break;
            case "Right":
                playerStuff.addCombo(1);
                break;
            case "Bot":
                playerStuff.addCombo(2);
                break;
            case "Left":
                playerStuff.addCombo(3);
                break;
            default:
                break;
        }


    }
}
