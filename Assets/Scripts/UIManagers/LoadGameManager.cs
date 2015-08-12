/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadGameManager : MonoSingleton <LoadGameManager> 
{
	[SerializeField] private GameObject m_objProgressbar;
	[SerializeField] private RectTransform m_rtProgressbar;
	[SerializeField] private Text m_textProgress;

	private float m_fLoadingProgress;
	private float m_fProgressbarMaxWidth;
	private Vector2 m_v2ProgressbarSizeDelta;

	public bool IsLoadingAssets { get; set; }

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected void Start ()
	{
		// First to load in entire game
		StartLoading ();
		StartCoroutine("LoadAssets");
		StartCoroutine("DisplayTitleScreen");
	}

	private IEnumerator DisplayTitleScreen ()
	{
		while(m_fLoadingProgress < 1.0f)
		{
			yield return new WaitForEndOfFrame();
		}

		UIStateMachine.Instance.ChangeUIState(UIState.OnTitleScreen);
	}

	private IEnumerator SimpleLoadTextAnimation ()
	{
		int iLoadTextState = 0;

		while(true)
		{
			switch(iLoadTextState){
			case 0:
			{
				m_textProgress.text = "Loading";
				break;
			}
			case 1:
			{
				m_textProgress.text = "Loading.";
				break;
			}
			case 2:
			{
				m_textProgress.text = "Loading..";
				break;
			}
			default:
			{
				iLoadTextState = -1;
				m_textProgress.text = "Loading...";
				break;
			}}

			iLoadTextState++;
			yield return new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator LoadAssets ()
	{
		float 	objTotalCount  = UIData.UI_MANAGER_PREFABS.Count;
				objTotalCount += UIData.UI_BUTTON_PREFABS.Count;
				objTotalCount += GameData.GAME_MANAGER_PREFABS.Count;
				objTotalCount += GameData.TOTAL_LEVEL_COUNT;

		float progressRate = 1.0f / objTotalCount;

		#region LOAD UI PREFABS
		foreach (KeyValuePair<UIManagerKeys, string> overlayName in UIData.UI_MANAGER_PREFABS)
		{
			m_fLoadingProgress += progressRate;
			m_v2ProgressbarSizeDelta.x = m_fLoadingProgress * m_fProgressbarMaxWidth;
			m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;

			Object loadObj = Resources.Load ("Prefabs/UIManagers/" + overlayName.Value);

			if(loadObj != null)
			{
				GameObject obj = Instantiate (loadObj) as GameObject;
				UIStateMachine.Instance.AddUIManager (obj.GetComponent<RectTransform>());
				obj.GetComponent<DisplayManager>().Close ();
			}
			else
			{
				Debug.LogError (overlayName.Value + " prefab missing.");
			}

			yield return new WaitForSeconds (0.05f);
		}
		#endregion

		#region LOAD COMMON BUTTONS
		foreach (KeyValuePair<UICommonButtonKeys, string> buttonName in UIData.UI_BUTTON_PREFABS)
		{
			m_fLoadingProgress += progressRate;
			m_v2ProgressbarSizeDelta.x = m_fLoadingProgress * m_fProgressbarMaxWidth;
			m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;
			
			Object loadObj = Resources.Load ("Prefabs/Buttons/" + buttonName.Value);
			
			if(loadObj != null)
			{
				GameObject obj = Instantiate (loadObj) as GameObject;
				UIStateMachine.Instance.AddUIManager (obj.GetComponent<RectTransform>());
				obj.GetComponent<DisplayManager>().Close ();
			}
			else
			{
				Debug.LogError (buttonName.Value + " prefab missing.");
			}
			
			yield return new WaitForSeconds (0.05f);
		}
		#endregion

		#region LOAD GAME MANAGERS
		foreach (KeyValuePair<GameManagerKeys, string> gameManagerName in GameData.GAME_MANAGER_PREFABS)
		{
			m_fLoadingProgress += progressRate;
			m_v2ProgressbarSizeDelta.x = m_fLoadingProgress * m_fProgressbarMaxWidth;
			m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;
			
			Object loadObj = Resources.Load ("Prefabs/GameManagers/" + gameManagerName.Value);
			
			if(loadObj != null)
			{
				GameObject obj = Instantiate (loadObj) as GameObject;
				GameStateMachine.Instance.AddGameObject (obj.transform);
				obj.GetComponent<DisplayManager>().Close ();
			}
			else
			{
				Debug.LogError (gameManagerName.Value + " prefab missing.");
			}
			
			yield return new WaitForSeconds (0.05f);
		}
		#endregion

		#region LOAD LEVELS
		for (int levelIDX = 0; levelIDX < GameData.TOTAL_LEVEL_COUNT; ++levelIDX)
		{
			m_fLoadingProgress += progressRate;
			m_v2ProgressbarSizeDelta.x = m_fLoadingProgress * m_fProgressbarMaxWidth;
			m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;

			Object loadObj = Resources.Load ("Prefabs/Levels/Level_" + levelIDX);

			if(loadObj != null)
			{
				GameObject objLevel = Instantiate (loadObj) as GameObject;
				LevelManager.Instance.AddLevel (objLevel.transform);
			}
			else
			{
				Debug.LogError ("Level_" + levelIDX + " prefab missing.");

				// levels shouldn't skip
				break;
			}
		}
		#endregion

		FinishLoading ();
	}

	public void StartLoading ()
	{
		m_fLoadingProgress = 0.0f;
		m_v2ProgressbarSizeDelta = m_rtProgressbar.sizeDelta;
		m_fProgressbarMaxWidth = m_v2ProgressbarSizeDelta.x;

		m_v2ProgressbarSizeDelta.x = 0.0f;
		m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;

		IsLoadingAssets = true;
		m_objProgressbar.SetActive (true);
		StartCoroutine ("SimpleLoadTextAnimation");
	}

	public void FinishLoading ()
	{
		m_fLoadingProgress = 1.0f;
		m_v2ProgressbarSizeDelta.x = m_fProgressbarMaxWidth;
		m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;
		
		StopCoroutine ("SimpleLoadTextAnimation");
		IsLoadingAssets = false;
		m_objProgressbar.SetActive (false);
	}
}
