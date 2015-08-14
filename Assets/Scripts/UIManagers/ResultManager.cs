/*
 * Brian Tria
 * Aug 14, 2015
 * 
 */


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultManager : MonoSingleton <ResultManager> 
{
	[SerializeField] private Text m_textScoreDisplay;
	[SerializeField] private Text m_textResultDisplay;
	private DisplayManager m_displayManager;

	public int Score { get; set; }

	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}

	private void DisplayScore ()
	{
		m_textScoreDisplay.text = "Score: " + Score.ToString ();
		m_textResultDisplay.text = "You Lose!";

		if(Score >= 4)
		{
			m_textResultDisplay.text = "You Win!";
		}
	}

	public void Open ()
	{
		m_displayManager.Open();
		this.transform.SetAsLastSibling ();
		DisplayScore ();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void OnClickRetry ()
	{
		Close ();
		GameStateMachine.Instance.ChangeGameState (GameState.Reset);
	}

	public void OnClickExit ()
	{
		
		UIStateMachine.Instance.ChangeUIState (UIState.OnTitleScreen);
		GameStateMachine.Instance.ChangeGameState (GameState.Exit);
	}
}
