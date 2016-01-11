using UnityEngine;
using System.Collections;
using System;


public class InputManager : Singleton<InputManager>
{
	#region Events

	public event Action onRightEvent;
	public event Action onLeftEvent;
	public event Action onDownEvent;

	#endregion

	#region Members

	#endregion

	#region Methods
	void Start()
	{}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.RightArrow) && onRightEvent != null)
			onRightEvent();
		
		if(Input.GetKeyDown(KeyCode.LeftArrow) && onLeftEvent != null)
			onLeftEvent();
		
		if(Input.GetKeyDown(KeyCode.DownArrow) && onDownEvent != null)
			onDownEvent();
	}
	#endregion
}