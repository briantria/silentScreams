/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using System.Collections.Generic;

public static class UIData
{
	public static readonly Dictionary<UIPrefabKeys, string> UI_MANAGER_PREFABS = new Dictionary<UIPrefabKeys, string>
	{
		{UIPrefabKeys.Title, "TitleScreenManager"},
		{UIPrefabKeys.GameHUD, "GameHUDManager"},
		{UIPrefabKeys.Settings, "SettingsManager"},
		{UIPrefabKeys.GameResults, "GameResultsManager"},
		{UIPrefabKeys.GamePause, "GamePauseManager"}
//		{UIPrefabKeys.GenericPrompt, "TitleScreen"},
//		{UIPrefabKeys.GenericConfirm, "TitleScreen"}
	};
}

public enum UIPrefabKeys
{
	Title,
	GameHUD,
	Settings,
	GameResults,
	GamePause,
	GenericPrompt,
	GenericConfirm
}