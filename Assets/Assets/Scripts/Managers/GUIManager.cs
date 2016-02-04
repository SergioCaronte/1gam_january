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
