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

	public delegate void UpdateGameState (GameState p_uiState);
	public static event UpdateGameState OnChangeGameState;

	public void ChangeGameState (GameState p_gameState)
	{
		switch(p_gameState){
		case GameState.Start:
		{
			CrimeSceneManager.Instance.Open ();
			break;
		}
		default: // GameState.Running
		{
			break;
		}}

		if(OnChangeGameState != null)
		{
			OnChangeGameState (p_gameState);
		}
	}

	public void AddGameObject (Transform m_gameManager)
	{
		m_gameManager.SetParent(this.transform);
		m_gameManager.localPosition = Vector3.zero;
		m_gameManager.localScale = Vector3.one;
	}
}

public enum GameState
{
	Inactive, // on title screen
	Idle, // waiting for user input
	Start,
	Running,
	Reset,
	Result,
	Exit
}
