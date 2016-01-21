using UnityEngine;
using System.Collections;

public enum CellColor
{
	NoColor = 	0x0000001,
	Red = 		0x0000010,
	Blue = 		0x0000100,
	Green =		0x0001000,
	Yellow =	0x0010000,
	Cyan = 		0x0100000,
	Magenta =   0x1000000,
	Chroma =    0x1111110
}

public enum CellFeature
{
	Regular,
	Bomb
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
	private CellColor color = CellColor.NoColor; 
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

	public CellColor GetColor()
	{ return color; }

	public void SetColor(CellColor c)
	{	color = c;	ResetColor(); }

	#endregion

	#region Methods

	public CellLogic(int row, int col)
	{
		this.row = row;
		this.col = col;
		this.id = row.ToString() + "_" + col.ToString();
	}

	public void Reset()
	{
		SetColor(CellColor.NoColor);
	}

	public bool Match(CellColor clr)
	{
		// if argument color is Chromatic,
		// it just matches if self color is chromatic as well.
		if(clr == CellColor.Chroma)
			return color == clr;
		else
			return( (clr & color) > 0); 
	}

	public void SetCellView(CellView v, int layerOrder)
	{
		cellView = v;
		cellView.gameObject.name = id;
		cellView.gameObject.transform.localPosition = new Vector3(col, -row, 0);
		cellView.TintColor(color);
		cellView.SetLayerOrder(layerOrder);
	}
		
	public void ResetColor()
	{
		if(cellView == null)	return;
		cellView.TintColor(color);
	}

	public void TintColor(CellColor c)
	{
		cellView.TintColor(c);
	}

	public void Destroy()
	{
		SetColor(CellColor.NoColor);
	}

	#endregion

}
