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

	public Vector2 GetOrigin() 
	{ return origin; }

	public void SetOrigin(Vector2 o)
	{ origin = o; }

	public CellLogic GetCell(int r, int c)
	{ return cells[r][c]; }

	public int GetWidth() 
	{ return width; }

	public void SetWidth(int w)
	{ width = w; }

	public int GetHeight() 
	{ return height; }

	public void SetHeight(int h)
	{ height = h; }

	#endregion

	#region Methods

	public PieceLogic()
	{
		ResetPosition();
	}

	public void ResetPosition()
	{
		origin = new Vector2(4, INITPOS);
	}

	public void ResetCells()
	{
		cells = new CellLogic[height][];
		for(int r = 0; r < height; r++)
		{
			cells[r] = new CellLogic[width];
			for(int c = 0; c < width; c++)
			{
				cells[r][c] = new CellLogic(r,c);
			}
		}
	}

	private PieceLogic Instantiate()
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
				instance.cells[r][c].SetColor(cells[r][c].GetColor() == ColorCell.NoColor ? ColorCell.NoColor : (ColorCell)Random.Range(1, 6));
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

	public void Rotate()
	{

		/*  w 3, h 2         h 3, w 2
		 *  |1,0|1,1|1,2|    |0,0|1,0|     |2,0|2,1|
		 *  |0,0|0,1|0,2| -> |0,1|1,1|     |1,0|1,1|
		 *                   |0,2|1,2|  -> |0,0|0,1|
		 * 
		 *  |1,0|1,1|     |0,0|1,0|
		 *  |0,0|0,1|  -> |0,1|1,1|
		 */ 

		CellLogic[][] temp = new CellLogic[width][];
		for(int r = 0; r < width; r++)
		{
			temp[r] = new CellLogic[height];
			for(int c = 0; c < height; c++)
			{
				temp[r][c] = new CellLogic(r,c);
				temp[r][c].SetColor(cells[c][width-r-1].GetColor());
			}
		}

		int holdwidth = width;
		width = height;
		height = holdwidth;
		cells = temp;
	}

	public void Unrotate()
	{
		// Lazy work. 3x rotation puts the piece in original rotation.
		Rotate();
		Rotate();
		Rotate();
	}
	#endregion
}
