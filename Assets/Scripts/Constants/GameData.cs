/*
 * Brian Tria
 * Aug 10, 2015
 * 
 * Stored data needed to load the game
 * 
 */

using System.Collections.Generic;

public static class GameData
{
	public static readonly Dictionary<GameManagerKeys, string> GAME_MANAGER_PREFABS = new Dictionary<GameManagerKeys, string>
	{
		{GameManagerKeys.CrimeScene, "CrimeSceneManager"},
		{GameManagerKeys.Smartphone, "SmartphoneManager"},
		{GameManagerKeys.EvidenceBox, "EvidenceBoxManager"},
		{GameManagerKeys.Level, "LevelManager"},
	};
}

public enum GameManagerKeys
{
	CrimeScene,
	Smartphone,
	EvidenceBox,
	Level
}
