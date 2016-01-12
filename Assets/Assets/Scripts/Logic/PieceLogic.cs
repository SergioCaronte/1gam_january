using UnityEngine;
using System.Collections;

public class PieceLogic
{
	public Vector2 origin;
	public CellLogic[][] cells;
	public int width;
	public int height;

	public PieceLogic()
	{
		origin = new Vector2(0,0);
		width = 2;
		height = 2;
		cells = new CellLogic[height][];
		for(int r = 0; r < height; r++)
		{
			cells[r] = new CellLogic[width];
			for(int c = 0; c < width; c++)
			{
				cells[r][c] = new CellLogic(r, c);
			}
		}

		cells[0][0].color = ColorCell.Red;
		cells[1][0].color = ColorCell.Blue;
		cells[0][1].color = ColorCell.Green;
	}

}
