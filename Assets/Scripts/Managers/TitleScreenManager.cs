/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoSingleton<TitleScreenManager>
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

	public void OnClickPlay ()
	{
		Close ();
		UIStateMachine.Instance.ChangeUIState (UIState.OnGameScreen);
		GameStateMachine.Instance.ChangeGameState (GameState.Start);
	}
}
