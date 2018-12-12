using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class DeleteButton : TerminalButton
{
    public override void OnInputClicked(InputClickedEventData eventData)
    {
        _terminalInputManager.RemoveWaypoint();
    }

    public override void OnInputDown(InputEventData eventData)
    {

    }

    public override void OnInputUp(InputEventData eventData)
    {

    }
}
