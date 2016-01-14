using UnityEngine;
using System.Collections;

public class PieceSpawnerManager : Singleton<PieceSpawnerManager>
{
	#region Members

	private PieceLogic[] bases;

	#endregion

	void Awake()
	{
		base.Awake();
		bases = new PieceLogic[] {
			//PieceLogic.Build1x1(ColorCell.Red),
			PieceLogic.Build2x2(ColorCell.Red, ColorCell.Red, ColorCell.NoColor, ColorCell.Red),
			PieceLogic.Build2x2(ColorCell.Red, ColorCell.Red, ColorCell.NoColor, ColorCell.NoColor),
			PieceLogic.Build2x2(ColorCell.Red, ColorCell.NoColor, ColorCell.NoColor, ColorCell.Red),
			PieceLogic.Build2x2(ColorCell.Red, ColorCell.NoColor, ColorCell.Red, ColorCell.NoColor)
		};

	}

	public PieceLogic GrabNewPiece()
	{
		// REPLACE IT. It must recycle the same object. 
		return bases[Random.Range(0, bases.Length)].Instantiate();
	}
}
