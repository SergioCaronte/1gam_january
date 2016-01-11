using UnityEngine;
using System.Collections;
using System;

enum GamePhase
{
	Starting,
	Playing,
	Paused,
	Ended
}

public class GameManager : Singleton<GameManager>
{
	#region Events

	public event Action onGameCycle;
	public event Action<int> onCountDown;
	public event Action onPauseGame;
	public event Action onResumeGame;

	#endregion 

	#region Members

	private GamePhase phase = GamePhase.Starting;

	#endregion

	void Start () 
    {
		StartCoroutine(GameCycle());
	}

	public void Pause()
	{
		if(onPauseGame != null)
			onPauseGame();
	}

	public void Resume()
	{
		if(onResumeGame != null)
			onResumeGame();
	}

	IEnumerator GameCycle()
	{
		while(phase != GamePhase.Ended)
		{
			switch(phase)
			{
			case GamePhase.Starting:
				yield return StartCoroutine(CountDown(3));
				break;
			case GamePhase.Playing:
				if(onGameCycle != null)
					onGameCycle();
				yield return new WaitForSeconds(1);
				break;
			case GamePhase.Paused:
				break;
			}
		}
	}

	IEnumerator CountDown(int time)
	{
		while(time > 0)
		{
			if(onCountDown != null)
				onCountDown(time);
			time--;
			yield return new WaitForSeconds(1);
		}
		phase = GamePhase.Playing;
	}

}
