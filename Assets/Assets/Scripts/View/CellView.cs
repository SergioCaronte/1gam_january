using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour 
{
	#region Members

	private SpriteRenderer render = null;

	#endregion

	void Awake()
	{
		render = gameObject.GetComponent<SpriteRenderer>();
	}

	public void ResetColor(ColorCell c)
	{
		TintColor(c);
	}

	public void TintColor(ColorCell c)
	{
		// sanity check
		if(render == null)	return;
		
		switch(c)
		{
		case ColorCell.Blue: render.color = new Color(0,0,1,1); break;
		case ColorCell.Red: render.color = new Color(1,0,0,1); break;
		case ColorCell.Green: render.color = new Color(0,1,0,1); break;
		case ColorCell.Cyan: render.color = new Color(0,1,1,1); break;
		case ColorCell.Magenta: render.color = new Color(1,0,1); break;
		case ColorCell.Yellow: render.color = new Color(1,1,0,1); break;
		case ColorCell.NoColor: render.color = new Color(1,1,1,0);break;
		}
	}

	public void SetLayerOrder(int order)
	{
		// sanity check
		if(render == null)	return;

		render.sortingOrder = order;
	}
}
