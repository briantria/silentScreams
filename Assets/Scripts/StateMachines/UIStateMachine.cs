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
			TitleScreenManager.Instance.Open ();
			break;
		}
		case UIState.OnGameScreen:
		{
			GameHUDManager.Instance.Open ();
			break;
		}}

		if(OnChangeUIState != null)
		{
			OnChangeUIState(p_uiState);
		}
	}

	public void AddUIManager (Transform p_manager)
	{
		p_manager.SetParent(m_rtUIContainer);
		p_manager.localScale = Vector3.one;
	}
}

public enum UIState
{
	OnTitleScreen,
	OnGameScreen
}
