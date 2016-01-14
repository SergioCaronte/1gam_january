using UnityEngine;
using System.Collections;

public class PieceLogic
{
	#region Members

	public static int INITPOS = -1;

	Vector2 origin;
	CellLogic[][] cells;
	int width;
	int height;

	#endregion

	#region Getters and Setters

	public Vector2 getOrigin() 
	{ return origin; }

	public CellLogic getCell(int r, int c)
	{ return cells[r][c]; }

	public int getWidth() 
	{ return width; }

	public int getHeight() 
	{ return height; }

	#endregion

	#region Methods

	public PieceLogic()
	{
		origin = new Vector2(4, INITPOS);
	}

	public PieceLogic Instantiate()
	{
		PieceLogic instance = new PieceLogic();
		instance.width = width;
		instance.height = height;
		instance.cells = new CellLogic[height][];
		for(int r = 0; r < height; r++)
		{
			instance.cells[r] = new CellLogic[width];
			for(int c = 0; c < width; c++)
			{
				instance.cells[r][c] = new CellLogic(r,c);
				instance.cells[r][c].color = cells[r][c].color == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6);
			}
		}
		return instance;
	}
		
	public void MoveLeft()
	{
		origin.x -= 1;
	}

	public void MoveRight()
	{
		origin.x += 1;
	}

	public void MoveDown()
	{
		origin.y += 1;
	}

	public void TurnRight()
	{
		// TODO
	}

	public void TurnLeft()
	{
		// TODO
	}
	#endregion

	#region Static Methods
	/*  _
	 * |1|
	 */
	public static PieceLogic Build1x1(ColorCell c1)
	{
		PieceLogic p = new PieceLogic();
		p.width = 1;
		p.height = 1;
		p.cells = new CellLogic[1][];
		p.cells[0] = new CellLogic[1];
		p.cells[0][0] = new CellLogic(0,0);
		p.cells[0][0].color = c1;

		return p;
	}

	/*  _ _
	 * |1|2|
	 * |3|4|
	 */
	public static PieceLogic Build2x2(ColorCell c1, ColorCell c2, ColorCell c3, ColorCell c4)
	{
		PieceLogic p = new PieceLogic();
		p.width = 2;
		p.height = 2;
		// Initializing cells.
		p.cells = new CellLogic[2][];
		p.cells[0] = new CellLogic[2];
		p.cells[1] = new CellLogic[2];
		// Picking randomly a color for the cell
		// But if it is NoColor, we keep it.
		p.cells[0][0] = new CellLogic(0,0);
		p.cells[0][0].color = c1 == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6);
		p.cells[0][1] = new CellLogic(0,1);
		p.cells[0][1].color = c2 == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6);
		p.cells[1][0] = new CellLogic(1,0);
		p.cells[1][0].color = c3 == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6);
		p.cells[1][1] = new CellLogic(1,1);
		p.cells[1][1].color = c4 == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6);

		return p;
	}

	/*  _ _ _
	 * |1|2|3|
	 * |4|5|6|
	 * |7|8|9|
	 */
	public static PieceLogic Build3x3(ColorCell[] c)
	{
		PieceLogic p = new PieceLogic();
		p.width = 2;
		p.height = 2;
		p.cells = new CellLogic[3][];
		p.cells[0] = new CellLogic[3];
		p.cells[0][0].color = c[0];
		p.cells[0][1].color = c[1];
		p.cells[0][2].color = c[2];
		p.cells[1][0].color = c[3];
		p.cells[1][1].color = c[4];
		p.cells[1][2].color = c[5];
		p.cells[2][0].color = c[6];
		p.cells[2][1].color = c[7];
		p.cells[2][2].color = c[8];

		return p;
	}

	#endregion

}
