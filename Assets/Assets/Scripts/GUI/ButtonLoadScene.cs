using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour 
{
	public string sceneName;

	public void Clicked()
	{
		SceneManager.LoadScene(sceneName);
	}
}