/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using System.Collections.Generic;

public static class UIData
{
	public static readonly Dictionary<UIManagerKeys, string> UI_MANAGER_PREFABS = new Dictionary<UIManagerKeys, string>
	{
		{UIManagerKeys.Title, "TitleScreenManager"},
		{UIManagerKeys.GameHUD, "GameHUDManager"},
		{UIManagerKeys.Settings, "SettingsManager"},
//		{UIManagerKeys.GameResults, "GameResultsManager"},
//		{UIManagerKeys.GamePause, "GamePauseManager"},
//		{UIPrefabKeys.GenericPrompt, "TitleScreen"},
//		{UIPrefabKeys.GenericConfirm, "TitleScreen"}
	};

	public static readonly Dictionary<UICommonButtonKeys, string> UI_BUTTON_PREFABS = new Dictionary<UICommonButtonKeys, string>
	{
		{UICommonButtonKeys.Pause, "BtnPause"},
//		{UICommonButtonKeys.Settings, "BtnSettings"},
//		{UICommonButtonKeys.Back, "BtnBack"}
	};
}

public enum UICommonButtonKeys
{
	Pause,
	Settings,
	Back
}

public enum UIManagerKeys
{
	Title,
	GameHUD,
	Settings,
	GameResults,
	GamePause,
	GenericPrompt,
	GenericConfirm
}