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

	public GridLogic grid = null;
	private GamePhase phase = GamePhase.Starting;
	private PieceLogic piece = null;
	private float cycleTime = 1;

	#endregion

	void Start () 
    {
		StartCoroutine(GameLoop());
	}

	IEnumerator DoCycle()
	{
		if(piece == null)
			piece = new PieceLogic();

		if(grid.CanGoDown(piece))
		{
			piece.origin.y += 1;
			grid.PrintPiece(piece);
		}
		else
		{
			//TODO check end game
			//TODO consolidate piece
			//TODO check score
			piece = null;
		}

		if(onGameCycle != null)
			onGameCycle();
		
		yield return new WaitForSeconds(cycleTime);
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

	IEnumerator GameLoop()
	{
		while(phase != GamePhase.Ended)
		{
			switch(phase)
			{
			case GamePhase.Starting:
				yield return StartCoroutine(CountDown(3));
				break;
			case GamePhase.Playing:
				yield return StartCoroutine(DoCycle());
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
