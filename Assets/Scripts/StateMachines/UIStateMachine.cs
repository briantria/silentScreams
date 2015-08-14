/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStateMachine : MonoSingleton <UIStateMachine> 
{
	[SerializeField] private RectTransform m_rtUIContainer;

	public delegate void UpdateUIState (UIState p_uiState);
	public static event UpdateUIState OnChangeUIState;

	public UIState State { get; set; }

	protected override void Awake ()
	{
		base.Awake ();
	}

	public void ChangeUIState (UIState p_uiState)
	{
		switch(p_uiState){
		case UIState.OnTitleScreen:
		{
			ResultManager.Instance.Close ();
			GameHUDManager.Instance.Close ();
			ButtonPause.Instance.Close ();
			TitleScreenManager.Instance.Open ();
			break;
		}
		case UIState.OnGameScreen:
		{
			TitleScreenManager.Instance.Close();
			GameHUDManager.Instance.Open ();
			ButtonPause.Instance.Open ();
			break;
		}
		case UIState.OnResultsScreen:
		{
			ResultManager.Instance.Open ();
			break;
		}}

		if(OnChangeUIState != null)
		{
			OnChangeUIState(p_uiState);
		}
	}

	public void AddUIManager (RectTransform p_manager)
	{
		p_manager.SetParent(m_rtUIContainer);
		p_manager.localScale = Vector3.one;
		p_manager.offsetMax = Vector2.zero;
		p_manager.offsetMin = Vector2.zero;
	}
}

public enum UIState
{
	OnTitleScreen,
	OnGameScreen,
	OnResultsScreen,
	OnSettings
}
