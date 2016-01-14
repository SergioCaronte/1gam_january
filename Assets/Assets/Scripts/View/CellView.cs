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
		if(render == null)
			print("Some problemo");
		
		switch(c)
		{
		case ColorCell.Blue: render.color = new Color(0,0,1); break;
		case ColorCell.Red: render.color = new Color(1,0,0); break;
		case ColorCell.Green: render.color = new Color(0,1,0); break;
		case ColorCell.Cyan: render.color = new Color(0,1,1); break;
		case ColorCell.Magenta: render.color = new Color(1,0,1); break;
		case ColorCell.Yellow: render.color = new Color(1,1,0); break;
		case ColorCell.NoColor: render.color = new Color(1,1,1);break;
		}
	}
}
