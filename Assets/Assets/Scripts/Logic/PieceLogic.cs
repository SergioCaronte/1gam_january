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
		// initialing array of cells
		// we pre aloc memory for 3x3, 
		// because is the max size of a piece.
		cells = new CellLogic[3][];
		for(int r = 0; r < 3; r++)
		{
			cells[r] = new CellLogic[3];
			for(int c = 0; c < 3; c++)
				cells[r][c] = new CellLogic(r,c);
		}

		ResetPosition();
	}

	public void ResetPosition()
	{
		origin = new Vector2(4, INITPOS);
	}

	public void ResetCells()
	{
		for(int r = 0; r < 3; r++)
			for(int c = 0; c < 3; c++)
				cells[r][c].Reset();
	}

	public void Copy(PieceLogic root)
	{
		SetWidth(root.GetWidth());
		SetHeight(root.GetHeight());
		ResetCells();
		for(int r = 0; r < height; r++)
			for(int c = 0; c < width; c++)
			{
				cells[r][c].SetColor(root.GetCell(r,c).GetColor());
				cells[r][c].SetFeature(root.GetCell(r,c).GetFeature());
			}
	}

	public void SetView(GameObject cell, GameObject parent)
	{
		for(int r = 0; r < 3; r++)
		{
			for(int c = 0; c < 3; c++)
			{
				GameObject go = GameObject.Instantiate(cell) as GameObject;
				go.transform.parent = parent.transform;
				cells[r][c].SetCellView(go.GetComponent<CellView>(), 3);
			}
		}
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

		CellLogic[][] temp = new CellLogic[3][]; 
		for(int r = 0; r < width; r++)
		{
			temp[r] = new CellLogic[3];
			for(int c = 0; c < height; c++)
			{
				temp[r][c] = new CellLogic(r,c);
				temp[r][c].SetColor(cells[c][width-r-1].GetColor());
				temp[r][c].SetFeature(cells[c][width-r-1].GetFeature());
			}
		}

		for(int r = 0; r < width; r++)
		{
			for(int c = 0; c < height; c++)
			{
				cells[r][c].SetColor(temp[r][c].GetColor());
				cells[r][c].SetFeature(temp[r][c].GetFeature());
			}
		}

		int holdwidth = width;
		width = height;
		height = holdwidth;

		//cells = temp;
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
