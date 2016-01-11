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
}
