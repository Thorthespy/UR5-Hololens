using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public abstract class TerminalButton : AbstractButton {

    protected TerminalInputManager _terminalInputManager;
	// Use this for initialization
	protected override void Start()
    {
        
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AssignTerminalInputManager(TerminalInputManager tim)
    {
        _terminalInputManager = tim;
    }
}
