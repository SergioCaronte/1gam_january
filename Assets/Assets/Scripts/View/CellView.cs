using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour 
{
	#region Members

	public SpriteRenderer render = null;

	#endregion

	void Awake()
	{
	}
		
	public void ResetColor(CellColor c)
	{
		TintColor(c);
	}

	public void TintColor(CellColor c)
	{
		// sanity check
		if(render == null)	return;
		
		switch(c)
		{
		case CellColor.Blue: render.color = new Color(0,0,1,1); break;
		case CellColor.Red: render.color = new Color(1,0,0,1); break;
		case CellColor.Green: render.color = new Color(0,1,0,1); break;
		case CellColor.Cyan: render.color = new Color(0,1,1,1); break;
		case CellColor.Magenta: render.color = new Color(1,0,1); break;
		case CellColor.Yellow: render.color = new Color(1,1,0,1); break;
		case CellColor.Chroma: render.color = new Color(1,1,1,1); break;
		case CellColor.NoColor: render.color = new Color(0,0,0,0);break;
		}
	}

	public void SetLayerOrder(int order)
	{
		// sanity check
		if(render == null)	return;

		render.sortingOrder = order;
	}

	public void SetFeature(CellFeature f)
	{
		ResetFeatures();
		if(f == CellFeature.Bomb)
			render.material.SetFloat("_IsBomb", 1);
	}

	void ResetFeatures()
	{
		render.material.SetFloat("_IsBomb", 0);
	}
}
