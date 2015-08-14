using UnityEngine;
using System.Collections;

public class GameHUDManager : MonoSingleton <GameHUDManager>
{
	private DisplayManager m_displayManager;
	private bool m_bToggleEvidenceBox;
	
	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
		m_bToggleEvidenceBox = false;
	}
	
	public void Open ()
	{
		m_displayManager.Open();
	}
	
	public void Close ()
	{
		m_bToggleEvidenceBox = false;
		m_displayManager.Close ();
	}

	public void Reset ()
	{
		m_bToggleEvidenceBox = false;
	}

	public void OnClickEvidenceBox ()
	{
		m_bToggleEvidenceBox = !m_bToggleEvidenceBox;

		if(m_bToggleEvidenceBox)
		{
			GameStateMachine.Instance.ChangeGameState (GameState.EvidenceBox);
		}
		else
		{
			GameStateMachine.Instance.ChangeGameState (GameState.Running);
		}
	}
}
