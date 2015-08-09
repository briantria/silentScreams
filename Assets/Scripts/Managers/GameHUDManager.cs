using UnityEngine;
using System.Collections;

public class GameHUDManager : MonoSingleton <GameHUDManager>, IDisplaySwitch
{
	#region IDisplaySwitch implementation
	public void Open ()
	{
		this.gameObject.SetActive (true);
	}
	public void Close ()
	{
		this.gameObject.SetActive (false);
	}
	#endregion

	public void OnClickPause ()
	{

	}
}
