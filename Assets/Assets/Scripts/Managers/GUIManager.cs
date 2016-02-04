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
using UnityEngine.UI;
using System.Collections;

public class GUIManager : Singleton<GUIManager> 
{

	#region Members

	public Text score;
	public Text multiplier;
	public GameObject pauseButton;
	public GameObject gameOverScreen;
	public GameObject pausedScreen;

	private bool paused = false;

	#endregion

	#region Methods
	void Start()
	{
		gameOverScreen.SetActive(false);
		pausedScreen.SetActive(false);
		GameManager.Instance.onGameOver += ShowGameOver;
	}

	public void SetScore(int score)
	{
		this.score.text = score.ToString();
	}

	public void SetMultiplier(int multiplier)
	{
		this.multiplier.text = multiplier.ToString();
	}

	public void ShowGameOver()
	{
		gameOverScreen.SetActive(true);
	}

	public void TooglePause()
	{
		paused = !paused;
		if(paused)
		{
			pauseButton.SetActive(false);
			pausedScreen.SetActive(true);
			GameManager.Instance.Pause();
		}
		else
		{
			pauseButton.SetActive(true);
			pausedScreen.SetActive(false);
			GameManager.Instance.Resume();
		}
	}

	#endregion

}
