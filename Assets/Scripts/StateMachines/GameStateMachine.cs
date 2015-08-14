/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class GameStateMachine : MonoSingleton <GameStateMachine> 
{
	public GameState State { get; set; }
	public bool IsGamePaused { get; set; }

	public delegate void UpdateGameState (GameState p_uiState);
	public static event UpdateGameState OnChangeGameState;

	public void ChangeGameState (GameState p_gameState)
	{
		switch(p_gameState){
		case GameState.Start:
		{
			IsGamePaused = false;
			CrimeSceneManager.Instance.Open ();
			LevelManager.Instance.Open ();
			LevelManager.Instance.OpenCurrentLevel ();
			break;
		}
		case GameState.EvidenceBox:
		{
			EvidenceBoxManager.Instance.Open ();
			break;
		}
		case GameState.Exit:
		{
			IsGamePaused = false;
			EvidenceBoxManager.Instance.Reset ();
			EvidenceBoxManager.Instance.Close ();
			LevelManager.Instance.CloseCurrentLevel ();
			LevelManager.Instance.Close ();
			CrimeSceneManager.Instance.Close ();
			break;
		}
		default: // GameState.Running
		{
			EvidenceBoxManager.Instance.Close ();

			break;
		}}

		if(OnChangeGameState != null)
		{
			OnChangeGameState (p_gameState);
		}
	}

	public void AddGameObject (Transform p_gameManager)
	{
		p_gameManager.SetParent(this.transform);
		p_gameManager.localPosition = Vector3.zero;
		p_gameManager.localScale = Vector3.one;
	}
}

public enum GameState
{
	Inactive, // on title screen
	Idle, // waiting for user input
	Start,
	Running,
	EvidenceBox,
	Reset,
	Result,
	Exit
}
