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
	private float cycleTime = .2f;

	#endregion

	#region Methods
	void Awake()
	{
		base.Awake();
		InputManager.instance.onLeftEvent += LeftPressed;
		InputManager.instance.onRightEvent += RightPressed;
	}

	void Start () 
    {
		StartCoroutine(GameLoop());
		piece = PieceSpawnerManager.instance.GrabNewPiece();
	}

	public void RightPressed()
	{
		if(grid.CanGoRight(piece))
		{
			piece.MoveRight();
			grid.PrintPiece(piece);
		}
	}

	public void LeftPressed()
	{
		if(grid.CanGoLeft(piece))
		{
			piece.MoveLeft();
			grid.PrintPiece(piece);
		}
	}

	// Pause warns listeners that the game has been paused. 
	public void Pause()
	{
		if(onPauseGame != null)
			onPauseGame();
	}

	// Resume warns listeners that the game has been resumed.
	public void Resume()
	{
		if(onResumeGame != null)
			onResumeGame();
	}
	#endregion

	#region Routines
	// Operates a cycle of gameplay. Update autmatic routines.
	IEnumerator DoCycle()
	{
		if(grid.CanGoDown(piece))
		{
			piece.MoveDown();
			grid.PrintPiece(piece);
		}
		else
		{
			// check end game
			// the game ends when the piece can go down anymore 
			// and is still on initial posistion.
			if(piece.getOrigin().y == PieceLogic.INITPOS)
			{
				print("End Game!");
				phase = GamePhase.Ended;
			}
			else // otherwise, consolidate piece into grid
			{
				grid.ConsolidatePiece(piece);
				// generate another piece
				piece = PieceSpawnerManager.instance.GrabNewPiece();
			}
			//TODO check if player has scored
		}

		if(onGameCycle != null)
			onGameCycle();
		
		yield return new WaitForSeconds(cycleTime);
	}
		
	// GameLoop works like a Finite State Machine.
	IEnumerator GameLoop()
	{
		while(phase != GamePhase.Ended)
		{
			switch(phase)
			{
			case GamePhase.Starting:
				yield return StartCoroutine(CountDown(1));
				break;
			case GamePhase.Playing:
				yield return StartCoroutine(DoCycle());
				break;
			case GamePhase.Paused:
				break;
			}
		}
	}

	// Count down the beginning of the game.
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
	#endregion
}
