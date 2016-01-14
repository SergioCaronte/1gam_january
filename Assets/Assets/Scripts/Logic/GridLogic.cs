using UnityEngine;
using System.Collections;

public class GridLogic : MonoBehaviour 
{
	#region Members

	public GameObject cell;
	private CellLogic[][] cells; 
	const int HEIGHT = 15;
	const int WIDTH = 10;

	#endregion

	// Use this for initialization
	void Start ()
    {
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
			cells[r][c].SetCellView(go.GetComponent<CellView>());
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

	public bool CanGoDown(PieceLogic piece)
	{
		int pHeight = piece.getHeight();
		int pWidth = piece.getWidth();
		int or = (int)piece.getOrigin().y;
		int oc = (int)piece.getOrigin().x;
		for(int r = 0; r < pHeight; r++)
			for(int c = 0; c < pWidth; c++)
		{
			// if cell is NoColor it's not necessary to check it.
			if(piece.getCell(r,c).color == ColorCell.NoColor)
				continue;
			// piece cell position on grid 
			// *+1 because we are verifying 
			// the possibility of going down.
			int row = or + 1 - r;
			int col = oc + c;
			// if cell outbounds of the grid, continue.
			if(row < 0 || col < 0 || col > WIDTH)
				continue;
			// if is outbounds at bottom, return false.
			if(row >= HEIGHT)
				return false;	
			// if that cell is already taken, return false.
			if(cells[row][col].color != ColorCell.NoColor)
				return false;
		}
		return true;
	}

	public bool CanGoLeft(PieceLogic piece)
	{
		// Fix it!
		if(piece.getOrigin().x < 1)
			return false;
		return true;
	}

	public bool CanGoRight(PieceLogic piece)
	{
		// Fix it!
		if(piece.getOrigin().x + piece.getWidth() >= WIDTH)
			return false;
		return true;
	}
		
	public void PrintPiece(PieceLogic piece)
	{
		// Reseting color cells
		for(int r = 0; r < HEIGHT; r++)
			for(int c = 0; c < WIDTH; c++)
				cells[r][c].ResetColor();

		int pHeight = piece.getHeight();
		int pWidth = piece.getWidth();
		int oc = (int)piece.getOrigin().x;
		int or = (int)piece.getOrigin().y;
		// Printing piece onto grid
		for(int r = 0; r < pHeight; r++)
		for(int c = 0; c < pWidth; c++)
		{
			// sanity check for when piece is not completely shown.
			if(or-r < 0)
				continue;

			if(piece.getCell(r,c).color != ColorCell.NoColor)
				cells[or-r][oc+c].TintColor(piece.getCell(r,c).color);
		}
	}

	public void ConsolidatePiece(PieceLogic piece)
	{
		int pHeight = piece.getHeight();
		int pWidth = piece.getWidth();
		int oc = (int)piece.getOrigin().x;
		int or = (int)piece.getOrigin().y;
		// Printing piece onto grid
		for(int r = 0; r < pHeight; r++)
		for(int c = 0; c < pWidth; c++)
		{
			// sanity check for when piece is not completely shown.
			if(or-r < 0)	continue;

			if(piece.getCell(r,c).color != ColorCell.NoColor)
			{
				cells[or-r][oc+c].SetColor(piece.getCell(r,c).color);
			}
		}
	}
}
