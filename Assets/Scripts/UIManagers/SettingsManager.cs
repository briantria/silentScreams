using UnityEngine;
using System.Collections;

public class SettingsManager : MonoSingleton <SettingsManager> 
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
		this.transform.SetAsLastSibling ();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void OnClickResume ()
	{
		Close ();
	}

	public void OnClickExitToMenu ()
	{
		Close ();

		UIStateMachine.Instance.ChangeUIState (UIState.OnTitleScreen);
		GameStateMachine.Instance.ChangeGameState (GameState.Exit);
	}
}
