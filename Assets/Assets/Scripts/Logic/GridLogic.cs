using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridLogic : MonoBehaviour 
{
	#region Action

	public event Action<int> onScored; 

	#endregion

	#region Members

	public GameObject cell;
	private CellLogic[][] cells; 
	const int HEIGHT = 15;
	const int WIDTH = 10;

	private bool scored;

	#endregion

	// Use this for initialization
	void Start ()
    {
		scored = false;

		cells = new CellLogic[HEIGHT][];
		for(int i = 0; i < HEIGHT; i++)
			cells[i] = new CellLogic[WIDTH];

		// Initializing cells
		for(int r = 0; r < HEIGHT; r++)
		for(int c = 0; c < WIDTH; c++)
		{
			cells[r][c] = new CellLogic(r, c);
			GameObject go = Instantiate(cell);
			go.transform.parent = this.transform;
			cells[r][c].SetCellView(go.GetComponent<CellView>(), 1);
		}
		
		// Filling siblings up
		for(int r = 0; r < HEIGHT; r++)
		for(int c = 0; c < WIDTH; c++)
		{
			if(r > 1) cells[r][c].up = cells[r-1][c];
			if(r < HEIGHT-1) cells[r][c].dn = cells[r+1][c];
		
			if(c > 1) cells[r][c].lf = cells[r][c-1];
			if(c < WIDTH-1) cells[r][c].rt = cells[r][c+1];
		}
	}

	public bool HasScored()
	{
		return scored;
	}

	/*
	 * Check if given piece is placeable onto grid.
	 */
	public bool IsPlaceable(PieceLogic piece)
	{
		int pHeight = piece.GetHeight();
		int pWidth = piece.GetWidth();
		int or = (int)piece.GetOrigin().y;
		int oc = (int)piece.GetOrigin().x;
		for(int r = 0; r < pHeight; r++)
			for(int c = 0; c < pWidth; c++)
			{
				// if cell is NoColor it's not necessary to check it.
				if(piece.GetCell(r,c).GetColor() == ColorCell.NoColor)
					continue;
				// piece cell position on grid 
				int row = or - r;
				int col = oc + c;
				// if cell outbounds of the grid, return false.
				if(row < 0 || row >= HEIGHT || col < 0 || col >= WIDTH)
					return false;	
				// if that cell is already taken, return false.
				if(cells[row][col].GetColor() != ColorCell.NoColor)
					return false;
			}
		return true;
	}

	/*
	 * Check if given piece is able to go down.
	 */
	public bool CanGoDown(PieceLogic piece)
	{
		int pHeight = piece.GetHeight();
		int pWidth = piece.GetWidth();
		int or = (int)piece.GetOrigin().y;
		int oc = (int)piece.GetOrigin().x;
		for(int r = 0; r < pHeight; r++)
			for(int c = 0; c < pWidth; c++)
			{
				// if cell is NoColor it's not necessary to check it.
				if(piece.GetCell(r,c).GetColor() == ColorCell.NoColor)
					continue;
				// piece cell position on grid 
				int row = or + 1 - r;
				int col = oc + c;
				// if cell outbounds of the grid, continue.
				if(row < 0 || col < 0 || col > WIDTH)
					continue;
				// if is outbounds at bottom, return false.
				if(row >= HEIGHT)
					return false;	
				// if that cell is already taken, return false.
				if(cells[row][col].GetColor() != ColorCell.NoColor)
					return false;
			}
		return true;
	}

	/*
	 * Check if given piece is able to slide at x axis.
	 * direction is -1 for left and 1 for right
	 */
	public bool CanSlide(PieceLogic piece, int direction)
	{
		int pHeight = piece.GetHeight();
		int pWidth = piece.GetWidth();
		int or = (int)piece.GetOrigin().y;
		int oc = (int)piece.GetOrigin().x;
		for(int r = 0; r < pHeight; r++)
			for(int c = 0; c < pWidth; c++)
			{
				// if cell is NoColor it's not necessary to check it.
				if(piece.GetCell(r,c).GetColor() == ColorCell.NoColor)
					continue;
				// piece cell position on grid 
				int row = or - r;
				int col = oc + direction + c;

				// if cell outbounds of the grid at top, continue.
				if(row < 0 || row >= HEIGHT)
					continue;
				// if cell outbounds at sides, return false.
				if(col < 0 || col >= WIDTH)
					return false;
				
				// if that cell is already taken, return false.
				if(cells[row][col].GetColor() != ColorCell.NoColor)
					return false;
			}
		return true;
	}
				
	/*
	 * Print given piece onto grid
	 * It is used to print out the piece that is not consolidated yet.
	 */ 
	public void PrintPiece(PieceLogic piece)
	{
		// Reseting color cells
		for(int r = 0; r < HEIGHT; r++)
			for(int c = 0; c < WIDTH; c++)
				cells[r][c].ResetColor();

		int pHeight = piece.GetHeight();
		int pWidth = piece.GetWidth();
		int oc = (int)piece.GetOrigin().x;
		int or = (int)piece.GetOrigin().y;
		// Printing piece onto grid
		for(int r = 0; r < pHeight; r++)
		for(int c = 0; c < pWidth; c++)
		{
			// sanity check for when piece is not completely shown.
			if(or-r < 0)
				continue;

			if(piece.GetCell(r,c).GetColor() != ColorCell.NoColor)	
				cells[or-r][oc+c].TintColor(piece.GetCell(r,c).GetColor());
		}
	}

	/*
	 * Put given piece onto grid.
	 * The piece will be colidable when consolidated.
	 */
	public void ConsolidatePiece(PieceLogic piece)
	{
		int pHeight = piece.GetHeight();
		int pWidth = piece.GetWidth();
		int oc = (int)piece.GetOrigin().x;
		int or = (int)piece.GetOrigin().y;
		// Printing piece onto grid
		for(int r = 0; r < pHeight; r++)
		for(int c = 0; c < pWidth; c++)
		{
			// sanity check for when piece is not completely shown.
			if(or-r < 0)	continue;

			if(piece.GetCell(r,c).GetColor() != ColorCell.NoColor)
			{
				cells[or-r][oc+c].SetColor(piece.GetCell(r,c).GetColor());
			}
		}
	}

	/*
	 * Check if there is some cells that score.
	 * Player scores by forming pattern of same colors cells (>= 3 cells)
	 */
	public IEnumerator CheckScore()
	{
		bool dirty = false;
		// Clear score flag
		scored = false;

		for(int r = 0; r < HEIGHT; r++)
		{
			for(int c = 0; c < WIDTH; c++)
			{
				// if empty cell, continue.
				if(cells[r][c].GetColor() == ColorCell.NoColor)
					continue;

				//!to use array.
				List<CellLogic> scoreCells = IsCellScore(cells[r][c]);
				if(scoreCells.Count > 0)
				{
					// Update score flag
					scored = true;
					// Call score update for listeners
					if(onScored != null)
						onScored(scoreCells.Count);
					
					dirty = true;
					// Vanish scored cells
					foreach (var cell in scoreCells)
					{
						cell.Destroy();
					}	
					yield return new WaitForSeconds(.2f);
					// Update affected cells
					foreach (var cell in scoreCells)
					{
						DropPieces(cell);	
					}
					break;
				}
			}
			if(dirty)
				break;
		}

		yield return new WaitForSeconds(.1f);
		if(dirty)
		{
			yield return StartCoroutine(CheckScore());
		}
	}

	public void DropPieces(CellLogic cell)
	{
		int c = cell.GetCol();
		int or = 1;
		// check how position is must drop before lay onto any piece
		while(cell.dn != null &&cell.dn.GetColor() == ColorCell.NoColor)
		{
			cell = cell.dn;
		  	or++;
		}

		// drop the cells 
		for(int r = cell.GetRow()-or; r >= 0; r--)
		{ 
			if(cells[r+or][c].GetColor() == ColorCell.NoColor)
			{
				cells[r+or][c].SetColor(cells[r][c].GetColor());
				cells[r][c].SetColor(ColorCell.NoColor);
			}
		}

		// clear the top cells
		for(int r = or; r >= 0; r--)
		{
			cells[r][c].SetColor(ColorCell.NoColor);
		}
	}

	/*
	 * Check if given cell forms a score.
	 * 
	 */
	List<CellLogic> IsCellScore(CellLogic cell)
	{
		List<CellLogic> hCells = new List<CellLogic>();
		List<CellLogic> vCells = new List<CellLogic>();
		ColorCell clr = cell.GetColor();

		hCells.Add(cell);
		vCells.Add(cell);

		CellLogic baseCell = cell;
		// walking the neighborhood while they are the same color
		// walking to the left
		while(cell.lf != null && cell.lf.GetColor() == clr)
		{
			hCells.Add(cell.lf);
			cell = cell.lf;
		}
		cell = baseCell;
		// walking to the right
		while(cell.rt != null && cell.rt.GetColor() == clr)
		{
			hCells.Add(cell.rt);
			cell = cell.rt;
		}
		cell = baseCell;
		// walking up
		while(cell.up != null && cell.up.GetColor() == clr)
		{
			vCells.Add(cell.up);
			cell = cell.up;
		}
		cell = baseCell;
		// walking down
		while(cell.dn != null && cell.dn.GetColor() == clr)
		{
			vCells.Add(cell.dn);
			cell = cell.dn;
		}

		// if found a sequence greater than 2, then the player scored.
		List<CellLogic> scoredCells = new List<CellLogic>();
		if(vCells.Count > 2)
			scoredCells.AddRange(vCells);
		if(hCells.Count > 2)
			scoredCells.AddRange(hCells);
		return scoredCells;
	}
}
