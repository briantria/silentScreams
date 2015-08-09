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
}

public enum GameState
{
	Inactive, // on title screen
	Idle, // waiting for user input
	Running,
	Reset,
	Result,
	End
}
