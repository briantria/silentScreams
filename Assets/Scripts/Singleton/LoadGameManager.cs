/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGameManager : MonoSingleton <LoadGameManager> 
{
	// completely loaded -> all needed assets were initialized on game start
	[SerializeField] private bool m_isGameCompletelyLoaded = true;
	[SerializeField] private GameObject m_objProgressbar;
	[SerializeField] private RectTransform m_rtProgressbar;
	[SerializeField] private Text m_textProgress;

	private float m_fLoadingProgress;
	private float m_fProgressbarMaxWidth;
	private Vector2 m_v2ProgressbarSizeDelta;

	public bool IsGameCompletelyLoaded { get {return m_isGameCompletelyLoaded;}}
	public bool IsLoadingAssets { get; set; }

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected void Start ()
	{
		// First to load in entire game
		StartLoading ();
		
		if(m_isGameCompletelyLoaded)
		{
			StartCoroutine("LoadAll");
		}
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

	private IEnumerator LoadAll ()
	{
		yield return new WaitForSeconds(1);
		FinishLoading ();
	}

	public void StartLoading ()
	{
		m_fLoadingProgress = 0.0f;
		m_v2ProgressbarSizeDelta = m_rtProgressbar.sizeDelta;
		m_fProgressbarMaxWidth = m_v2ProgressbarSizeDelta.x;

		IsLoadingAssets = true;
		m_objProgressbar.SetActive(true);
		StartCoroutine("SimpleLoadTextAnimation");
	}

	public void FinishLoading ()
	{
		m_fLoadingProgress = 1.0f;
		m_v2ProgressbarSizeDelta.x = m_fProgressbarMaxWidth;
		m_rtProgressbar.sizeDelta = m_v2ProgressbarSizeDelta;
		
		StopCoroutine("SimpleLoadTextAnimation");
		IsLoadingAssets = false;
		m_objProgressbar.SetActive(false);
	}
}
