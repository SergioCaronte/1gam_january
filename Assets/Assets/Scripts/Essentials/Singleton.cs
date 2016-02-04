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

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;

	public static T Instance
	{
		get
		{
			if(!instance)
			{
				instance = (T) FindObjectOfType(typeof(T));

				if(!instance)
				{
					Debug.LogWarning("An instance of " + typeof(T) + 
					               " is needed in the scene, but there is none.");
				}
			}

			return instance;
		}
	}

	protected void Awake()
	{
		if(Instance != this)
		{
			print ("Desrtoying Singleton of " + typeof(T).ToString() + " beacause already there is an instance in the scene");
			Destroy(this.gameObject);
		}
	}
}
