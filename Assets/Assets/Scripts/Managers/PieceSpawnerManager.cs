using UnityEngine;
using System.Collections;

public class PieceSpawnerManager : Singleton<PieceSpawnerManager>
{
	#region Members
	static ColorCell[] COLORS = { ColorCell.Red, ColorCell.Blue, ColorCell.Green, 
		ColorCell.Cyan, ColorCell.Yellow, ColorCell.Magenta, ColorCell.Chroma};

	public GameObject cellView;
	public GameObject nextPieceHolder;

	private PieceLogic piece;
	private PieceLogic nextPiece;
	private int rangeMin;
	private int rangeMax;
	#endregion

	void Awake()
	{
		base.Awake();
		piece = new PieceLogic();
		nextPiece = new PieceLogic();
		nextPiece.SetView(Instantiate(cellView), nextPieceHolder);
		rangeMin = 1;
		rangeMax = 5;

		GrabNewPiece();
	}

	public PieceLogic GrabNewPiece()
	{
		piece.ResetPosition();
		piece.Copy(nextPiece);

		//return Piece0();

		switch(Random.Range(0, 8))
		{
		case 0:  Piece1(); break;
		case 1:  Piece2(); break;	
		case 2:  Piece3(); break;	
		case 3:  Piece4(); break;	
		case 4:  Piece5(); break;	
		case 5:  Piece6(); break;	
		case 6:  Piece7(); break;	
		case 7:  Piece8(); break;	
		}

		return piece;
	}

	public void Piece0()
	{
		nextPiece.SetWidth(1);
		nextPiece.SetHeight(1);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ _
	 * |_|_|_
	 *   |_|_|
	 */
	public void Piece1()
	{
		nextPiece.SetWidth(3);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(0,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,2).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*    _ _
	 *  _|_|_|
	 * |_|_|
	 */
	public void Piece2()
	{
		nextPiece.SetWidth(3);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,2).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ _
	 * |_|_|
	 * |_|_|
	 */
	public void Piece3()
	{
		nextPiece.SetWidth(2);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(1,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*    _
	 *  _|_|_
	 * |_|_|_|
	 */
	public void Piece4()
	{
		nextPiece.SetWidth(3);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,2).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ _
	 * |_|_|
	 * |_|
	 * |_|
	 */
	public void Piece5()
	{
		nextPiece.SetWidth(2);
		nextPiece.SetHeight(3);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(2,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(2,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ _
	 * |_|_|
	 *   |_|
	 *   |_|
	 */
	public void Piece6()
	{
		nextPiece.SetWidth(2);
		nextPiece.SetHeight(3);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(2,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(0,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(2,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ _
	 * |_|_|
	 * |_|
	 */
	public void Piece7()
	{
		nextPiece.SetWidth(2);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,1).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}

	/*  _ 
	 * |_|
	 * |_|
	 */
	public void Piece8()
	{
		nextPiece.SetWidth(1);
		nextPiece.SetHeight(2);
		// Initializing cells.
		nextPiece.ResetCells();
		// Picking randomly a color for the cell
		nextPiece.GetCell(0,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
		nextPiece.GetCell(1,0).SetColor(COLORS[Random.Range(rangeMin, rangeMax)]);
	}
}
