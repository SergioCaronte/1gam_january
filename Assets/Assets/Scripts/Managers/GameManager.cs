using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public event Action onGameOver;

	#endregion 

	#region Members

	public GridLogic grid = null;
	private GamePhase phase = GamePhase.Starting;
	private PieceLogic piece = null;
	private float cycleTime = .5f;
	private bool doingCycle = false;

	private int points;
	private int multiplier;

	#endregion

	#region Methods
	void Awake()
	{
		base.Awake();
		InputManager.instance.onLeftEvent += LeftPressed;
		InputManager.instance.onRightEvent += RightPressed;
		InputManager.instance.onDownEvent += DownPressed;
		InputManager.instance.onUpEvent += UpPressed;
		grid.onScored += OnScore;
	}

	void Start () 
    {
		StartCoroutine(GameLoop());
		piece = PieceSpawnerManager.instance.GrabNewPiece();
		OnScore(0);
		OnMultiplier(1);
	}

	// Event called when right is pressed
	public void RightPressed()
	{
		if(grid.CanSlide(piece, 1))
		{
			piece.MoveRight();
			grid.PrintPiece(piece);
		}
	}

	// Event called when left is pressed
	public void LeftPressed()
	{
		if(grid.CanSlide(piece, -1))
		{
			piece.MoveLeft();
			grid.PrintPiece(piece);
		}
	}

	// Event called when up is pressed
	public void UpPressed()
	{
		if(!doingCycle)
		{
			piece.Rotate();
			if(!grid.IsPlaceable(piece))
				piece.Unrotate();
			grid.PrintPiece(piece);
		}
	}

	// Event called when down is pressed
	public void DownPressed()
	{
		cycleTime = .01f;
	}

	// Event called when player scores
	public void OnScore(int cells)
	{
		points += (int)Math.Pow(2, cells) * multiplier;
		GUIManager.instance.SetScore(points);
	}

	// Update the multiplier factor
	public void OnMultiplier(int multi)
	{
		multiplier = multi;
		GUIManager.instance.SetMultiplier(multiplier);
	}

	// Pause warns listeners that the game has been paused. 
	public void Pause()
	{
		phase = GamePhase.Paused;
		if(onPauseGame != null)
			onPauseGame();
	}

	// Resume warns listeners that the game has been resumed.
	public void Resume()
	{
		phase = GamePhase.Playing;
		if(onResumeGame != null)
			onResumeGame();
	}
	#endregion

	#region Routines
	// Operates a cycle of gameplay. Update autmatic routines.
	IEnumerator DoCycle()
	{
		doingCycle = true;
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
			if(piece.GetOrigin().y == PieceLogic.INITPOS)
			{
				print("End Game!");
				phase = GamePhase.Ended;
				if(onGameOver != null)
					onGameOver();
			}
			else // otherwise, consolidate piece into grid
			{
				cycleTime = .5f;
				// put the piece onto grid.
				grid.ConsolidatePiece(piece);
				// check if player has scored. 
				yield return StartCoroutine(grid.CheckScore());
				// check if it's occured a score indeed
				// if so, add multiplier, otherwise reset it.
				if(grid.HasScored())
					OnMultiplier(multiplier+1);
				else
					OnMultiplier(1);
				// generate another piece.
				piece = PieceSpawnerManager.instance.GrabNewPiece();
				// reset cycletime in case of player pressing down.
			}
		}

		if(onGameCycle != null)
			onGameCycle();

		doingCycle = false;
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
				yield return new WaitForEndOfFrame();
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
