using UnityEngine;
using System.Collections;

public enum ColorCell
{
	NoColor = 0,
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
	private ColorCell color = ColorCell.NoColor; 
	// Cell row position
	private int row;
	// Cell column position
	private int col;
	// Points to graphic representation of the cell
	private CellView cellView = null;
	#endregion

	#region Getters & Setters

	public int GetRow() 
	{ return row; }

	public int GetCol()
	{ return col; }

	public ColorCell GetColor()
	{ return color; }

	public void SetColor(ColorCell c)
	{	color = c;	ResetColor(); }

	#endregion

	#region Methods

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
		cellView.TintColor(color);
	}
		
	public void ResetColor()
	{
		if(cellView == null)	return;
		cellView.TintColor(color);
	}

	public void TintColor(ColorCell c)
	{
		cellView.TintColor(c);
	}

	public void Destroy()
	{
		SetColor(ColorCell.NoColor);
	}

	#endregion

}
