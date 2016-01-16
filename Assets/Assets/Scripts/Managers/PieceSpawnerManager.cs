using UnityEngine;
using System.Collections;

public class PieceSpawnerManager : Singleton<PieceSpawnerManager>
{
	#region Members

	private PieceLogic piece;
	private int rangeMin;
	private int rangeMax;
	#endregion

	void Awake()
	{
		base.Awake();
		piece = new PieceLogic();
		rangeMin = 1;
		rangeMax = 5;
	}

	public PieceLogic GrabNewPiece()
	{
		piece.ResetPosition();

		//return Piece0();

		switch(Random.Range(0, 8))
		{
		case 0: return Piece1();
		case 1: return Piece2();	
		case 2: return Piece3();	
		case 3: return Piece4();	
		case 4: return Piece5();	
		case 5: return Piece6();	
		case 6: return Piece7();	
		case 7: return Piece8();	
		}
		return Piece2();
	}

	public PieceLogic Piece0()
	{
		piece.SetWidth(1);
		piece.SetHeight(1);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*  _ _
	 * |_|_|_
	 *   |_|_|
	 */
	public PieceLogic Piece1()
	{
		piece.SetWidth(3);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(0,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,2).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		return piece;
	}

	/*    _ _
	 *  _|_|_|
	 * |_|_|
	 */
	public PieceLogic Piece2()
	{
		piece.SetWidth(3);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,2).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		return piece;
	}

	/*  _ _
	 * |_|_|
	 * |_|_|
	 */
	public PieceLogic Piece3()
	{
		piece.SetWidth(2);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(1,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*    _
	 *  _|_|_
	 * |_|_|_|
	 */
	public PieceLogic Piece4()
	{
		piece.SetWidth(3);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,2).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*  _ _
	 * |_|_|
	 * |_|
	 * |_|
	 */
	public PieceLogic Piece5()
	{
		piece.SetWidth(2);
		piece.SetHeight(3);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(2,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(2,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*  _ _
	 * |_|_|
	 *   |_|
	 *   |_|
	 */
	public PieceLogic Piece6()
	{
		piece.SetWidth(2);
		piece.SetHeight(3);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(2,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(0,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(2,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*  _ _
	 * |_|_|
	 * |_|
	 */
	public PieceLogic Piece7()
	{
		piece.SetWidth(2);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,1).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}

	/*  _ 
	 * |_|
	 * |_|
	 */
	public PieceLogic Piece8()
	{
		piece.SetWidth(1);
		piece.SetHeight(2);
		// Initializing cells.
		piece.ResetCells();
		// Picking randomly a color for the cell
		piece.GetCell(0,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));
		piece.GetCell(1,0).SetColor((ColorCell)Random.Range(rangeMin, rangeMax));

		return piece;
	}
}
