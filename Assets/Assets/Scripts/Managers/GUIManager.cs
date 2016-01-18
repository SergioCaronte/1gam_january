using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : Singleton<GUIManager> 
{

	#region Members

	public Text score;
	public Text multiplier;

	#endregion

	#region 

	public void SetScore(int score)
	{
		this.score.text = score.ToString();
	}

	public void SetMultiplier(int multiplier)
	{
		this.multiplier.text = multiplier.ToString();
	}

	#endregion

}
