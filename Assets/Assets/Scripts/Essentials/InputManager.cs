/*
* Tetris Crush Saga
* Copyright (C) 2016 Sergio Nunes da Silva Junior (@snsjr)
* One Game A Month Challenge - January 2016
*
* This program is free software; you can redistribute it and/or modify it
* under the terms of the GNU General Public License as published by the Free
* Software Foundation; either version 2 of the License.
*
* This program is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
* more details.
*
* author: Sergio Nunes da Silva Junior (@snsjr)
* contact: sjuniorhp@gmail.com 
*/

using UnityEngine;
using System.Collections;
using System;


public class InputManager : Singleton<InputManager>
{
	#region Events

	public event Action onRightEvent;
	public event Action onLeftEvent;
	public event Action onDownEvent;
	public event Action onUpEvent;

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
		
		if(Input.GetKeyDown(KeyCode.Space) && onDownEvent != null)
			onDownEvent();

		if(Input.GetKeyDown(KeyCode.UpArrow) && onUpEvent != null)
			onUpEvent();
	}
	#endregion
}