/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class ButtonPause : MonoSingleton <ButtonPause>
{
	private DisplayManager m_displayManager;
	
	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}
	
	public void Open ()
	{
		m_displayManager.Open();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void OnClickPause ()
	{
		GameStateMachine.Instance.IsGamePaused = true;
		SettingsManager.Instance.Open ();
	}
}
