/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoSingleton<TitleScreenManager>, IDisplaySwitch
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

	public void OnClickPlay ()
	{
		Close ();
		GameHUDManager.Instance.Open ();
	}
}
