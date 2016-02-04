/*
* Tetris Crush Saga
* Copyright (C) 2016 Sergio Nunes da Silva Junior (@snsjr)
* One Game A Month Challenge - January 2016
*
* This program is free software; you can redistribute it and/or modify it
* under the terms of the GNU General Public License as published by the Free
* Software Foundation; either version 2 of the License.
*
* This program is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
* more details.
*
* author: Sergio Nunes da Silva Junior (@snsjr)
* contact: sjuniorhp@gmail.com 
*/

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
				if(piece.GetCell(r,c).Match(CellColor.NoColor))
					continue;
				// piece cell position on grid 
				int row = or - r;
				int col = oc + c;
				// if cell outbounds of the grid, return false.
				if(row < 0 || row >= HEIGHT || col < 0 || col >= WIDTH)
					return false;	
				// if that cell is already taken, return false.
				if(!cells[row][col].Match(CellColor.NoColor))
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
				if(piece.GetCell(r,c).Match(CellColor.NoColor))
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
				if(!cells[row][col].Match(CellColor.NoColor))
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
				if(piece.GetCell(r,c).Match(CellColor.NoColor))
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
				if(!cells[row][col].Match(CellColor.NoColor))
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

			if(!piece.GetCell(r,c).Match(CellColor.NoColor))
			{	
				cells[or-r][oc+c].TintColor(piece.GetCell(r,c).GetColor());
				cells[or-r][oc+c].SetFeature(piece.GetCell(r,c).GetFeature());	
			}
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

			if(!piece.GetCell(r,c).Match(CellColor.NoColor))
			{
				cells[or-r][oc+c].SetColor(piece.GetCell(r,c).GetColor());
				cells[or-r][oc+c].SetFeature(piece.GetCell(r,c).GetFeature());
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
				// if empty cell or chromatic, continue.
				if(cells[r][c].Match(CellColor.NoColor) || cells[r][c].Match(CellColor.Chroma))
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
						DestroyCell(cell, scoreCells);
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
		// while pieces have changed, we check for new scores
		if(dirty)
		{
			yield return StartCoroutine(CheckScore());
		}
	}

	public void DestroyCell(CellLogic cell, List<CellLogic> scoreCells)
	{
		cell.Destroy();
	}

	public void DropPieces(CellLogic cell)
	{
		int c = cell.GetCol();
		int or = 1;
		// check how position is must drop before lay onto any piece
		while(cell.dn != null &&cell.dn.Match(CellColor.NoColor))
		{
			cell = cell.dn;
		  	or++;
		}

		// drop the cells 
		for(int r = cell.GetRow()-or; r >= 0; r--)
		{ 
			if(cells[r+or][c].Match(CellColor.NoColor))
			{
				cells[r+or][c].SetColor(cells[r][c].GetColor());
				cells[r][c].SetColor(CellColor.NoColor);
			}
		}

		// clear the top cells
		for(int r = or; r >= 0; r--)
		{
			cells[r][c].SetColor(CellColor.NoColor);
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
		CellColor clr = cell.GetColor();

		hCells.Add(cell);
		vCells.Add(cell);

		CellLogic baseCell = cell;
		// walking the neighborhood while they are the same color
		// walking to the left
		while(cell.lf != null && cell.lf.Match(clr))
		{
			hCells.Add(cell.lf);
			cell = cell.lf;
		}
		cell = baseCell;
		// walking to the right
		while(cell.rt != null && cell.rt.Match(clr))
		{
			hCells.Add(cell.rt);
			cell = cell.rt;
		}
		cell = baseCell;
		// walking up
		while(cell.up != null && cell.up.Match(clr))
		{
			vCells.Add(cell.up);
			cell = cell.up;
		}
		cell = baseCell;
		// walking down
		while(cell.dn != null && cell.dn.Match(clr))
		{
			vCells.Add(cell.dn);
			cell = cell.dn;
		}

		// if found a sequence greater than 2, then the player scored.
		List<CellLogic> scoredCells = new List<CellLogic>();
		if(vCells.Count > 2)
		{
			scoredCells.AddRange(vCells);
			foreach(var c in vCells)
			{
				CheckBombCell(c, scoredCells); 
			}
		}
		if(hCells.Count > 2)
		{
			scoredCells.AddRange(hCells);
			foreach(var c in hCells)
			{
				CheckBombCell(c, scoredCells); 
			}
		}
		return scoredCells;
	}

	void CheckBombCell(CellLogic cell, List<CellLogic> scoreCells)
	{
		// If it was a bomb cell, neighborhood is destroyed
		if(cell.GetFeature() == CellFeature.Bomb)
		{
			// left cell
			if(cell.lf != null)
			{
				if(!cell.lf.Match(CellColor.NoColor) && !scoreCells.Contains(cell.lf))
					scoreCells.Add(cell.lf);
				// up cell of left cell
				if(cell.lf.up != null && !cell.lf.up.Match(CellColor.NoColor) && !scoreCells.Contains(cell.lf.up))
					scoreCells.Add(cell.lf.up);
				// down cell of left cell
				if(cell.lf.dn != null && !cell.lf.dn.Match(CellColor.NoColor) && !scoreCells.Contains(cell.lf.dn))
					scoreCells.Add(cell.lf.dn);				
			}

			if(cell.rt != null)
			{
				if(!cell.rt.Match(CellColor.NoColor) && !scoreCells.Contains(cell.rt))
					scoreCells.Add(cell.rt);
				// up cell of right cell
				if(cell.rt.up != null && !cell.rt.up.Match(CellColor.NoColor) && !scoreCells.Contains(cell.rt.up))
					scoreCells.Add(cell.rt.up);
				// down cell of right cell
				if(cell.rt.dn != null && !cell.rt.dn.Match(CellColor.NoColor) && !scoreCells.Contains(cell.rt.dn))
					scoreCells.Add(cell.rt.dn);				
			}

			if(cell.dn != null)
			{
				if(!cell.dn.Match(CellColor.NoColor) && !scoreCells.Contains(cell.dn))
					scoreCells.Add(cell.dn);
				// left cell of down cell
				if(cell.dn.lf != null && !cell.dn.lf.Match(CellColor.NoColor) && !scoreCells.Contains(cell.dn.lf))
					scoreCells.Add(cell.dn.lf);
				// right cell of down cell
				if(cell.dn.rt != null && !cell.dn.rt.Match(CellColor.NoColor) && !scoreCells.Contains(cell.dn.rt))
					scoreCells.Add(cell.dn.rt);				
			}

			if(cell.up != null)
			{
				if(!cell.up.Match(CellColor.NoColor) && !scoreCells.Contains(cell.up))
					scoreCells.Add(cell.up);
				// left cell of up
				if(cell.up.lf != null && !cell.up.lf.Match(CellColor.NoColor) && !scoreCells.Contains(cell.up.lf))
					scoreCells.Add(cell.up.lf);
				// right cell of up
				if(cell.up.rt != null && !cell.up.rt.Match(CellColor.NoColor) && !scoreCells.Contains(cell.up.rt))
					scoreCells.Add(cell.up.rt);				
			}
		}
	}
}
