/*
 * Brian Tria
 * Aug 14, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class ButtonSettings : MonoSingleton <ButtonSettings> 
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

	public void OnClickSettings ()
	{
		SettingsManager.Instance.Open ();
		SettingsManager.Instance.DisplaySlidersOnly ();
	}
}
