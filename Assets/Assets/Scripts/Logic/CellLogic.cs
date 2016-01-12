using UnityEngine;
using System.Collections;

public enum ColorCell
{
	NoColor,
	Red,
	Blue,
	Green,
	Yellow,
	Cyan,
	Magenta
}

public class CellLogic
{
	#region Members
	public string id = string.Empty;
	// Points to the left sibling cell
	public CellLogic lf = null;
	// Points to the right sibling cell
	public CellLogic rt = null;
	// Points to the up sibling cell
	public CellLogic up = null;
	// Points to the down sibling cell
	public CellLogic dn = null;
	// Cell color
	public ColorCell color = ColorCell.NoColor; 
	// Cell row position
	private int row;
	// Cell column position
	private int col;
	// Points to graphic representation of the cell
	private CellView cellView = null;
	#endregion

	public CellLogic(int row, int col)
	{
		this.row = row;
		this.col = col;
		this.id = row.ToString() + "_" + col.ToString();
	}

	public void SetCellView(CellView v)
	{
		cellView = v;
		cellView.gameObject.name = id;
		cellView.gameObject.transform.position = new Vector3(col, -row, 0);

		var render = cellView.gameObject.GetComponent<SpriteRenderer>();

		switch(color)
		{
		case ColorCell.Blue: render.color = new Color(0,0,1); break;
		case ColorCell.Red: render.color = new Color(1,0,0); break;
		case ColorCell.Green: render.color = new Color(0,1,0); break;
		case ColorCell.Cyan: render.color = new Color(0,1,1); break;
		case ColorCell.Magenta: render.color = new Color(1,0,1); break;
		case ColorCell.Yellow: render.color = new Color(1,1,0); break;
		case ColorCell.NoColor: render.color = new Color(1,1,1,.5f);break;
		}
	}

}
