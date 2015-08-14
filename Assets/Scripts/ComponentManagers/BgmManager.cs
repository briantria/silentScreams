/*
 * Brian Tria
 * Aug 14, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class BgmManager : MonoSingleton <BgmManager>
{
	private AudioSource m_audioSource;
	private float m_fInitVolume;

	private AudioClip   m_mainBGM;
	private AudioClip   m_endGameBGM;
	private AudioClip[] m_arrayGameBGM;

	private const string SOUND_BGM_PATH = "Sounds/BGM/";

	protected void OnEnable ()
	{
		UIStateMachine.OnChangeUIState += UpdateUIState;
		GameStateMachine.OnChangeGameState += UpdateGameState;
	}

	protected void OnDisable ()
	{
		UIStateMachine.OnChangeUIState -= UpdateUIState;
		GameStateMachine.OnChangeGameState -= UpdateGameState;
	}

	protected override void Awake ()
	{
		base.Awake ();

		m_audioSource = this.GetComponent<AudioSource> ();
		m_audioSource.ignoreListenerVolume = true;
		m_fInitVolume = m_audioSource.volume;

		m_mainBGM    = Resources.Load (SOUND_BGM_PATH + "DanseMacabre/Danse Macabre") as AudioClip;
		m_endGameBGM = Resources.Load (SOUND_BGM_PATH + "DanseMacabre/Danse Macabre - Finale") as AudioClip;

		m_arrayGameBGM = new AudioClip[]
		{
			Resources.Load (SOUND_BGM_PATH + "DanseMacabre/Danse Macabre - Busy Strings") as AudioClip,
			Resources.Load (SOUND_BGM_PATH + "DanseMacabre/Danse Macabre - Violin Hook") as AudioClip,
			Resources.Load (SOUND_BGM_PATH + "DanseMacabre/Danse Macabre - Xylophone") as AudioClip
		};
	}

	protected void OnDestroy ()
	{
		if (m_arrayGameBGM != null)
		{
			m_arrayGameBGM = null;
		}
	}

	private void UpdateUIState (UIState p_uiState)
	{
		switch (p_uiState){
		case UIState.OnTitleScreen:
		{
			StopCoroutine ("PlayRandomGameBGM");
			m_audioSource.clip = m_mainBGM;
			m_audioSource.loop = true;

			if(!m_audioSource.isPlaying)
			{
				m_audioSource.Play ();
			}

			break;
		}}
	}

	private void UpdateGameState (GameState p_gameState)
	{
		switch (p_gameState){
		case GameState.Start:
		{
			m_audioSource.loop = false;
			m_audioSource.clip = m_arrayGameBGM [Random.Range (0, m_arrayGameBGM.Length)];

			if(!m_audioSource.isPlaying) 
			{
				m_audioSource.Play ();
			}

			StartCoroutine ("PlayRandomGameBGM");
			break;
		}
//		case GameState.Running:
//		{
//			if(m_audioSource.isPlaying) { break; }
//
//			m_audioSource.clip = m_arrayGameBGM [Random.Range (0, m_arrayGameBGM.Length)];
//			m_audioSource.Play ();
//			StartCoroutine ("PlayRandomGameBGM");
//
//			break;
//		}
		case GameState.Result:
		{
			StopCoroutine ("PlayRandomGameBGM");
			m_audioSource.loop = false;
			m_audioSource.clip = m_endGameBGM;

			if(!m_audioSource.isPlaying)
			{
				m_audioSource.Play ();
			}

			break;
		}}
	}

	private IEnumerator PlayRandomGameBGM ()
	{
		while (true)
		{
			if(!m_audioSource.isPlaying)
			{
				m_audioSource.clip = m_arrayGameBGM [Random.Range (0, m_arrayGameBGM.Length)];
				m_audioSource.Play ();
			}

			yield return new WaitForSeconds (1.0f);
		}
	}

	public void SetBGMVolume (float p_fVolume)
	{
		p_fVolume = Mathf.Clamp (p_fVolume, 0.0f, 1.0f);

		if(!m_audioSource.mute)
		{
			m_audioSource.volume = m_fInitVolume * p_fVolume;
		}
	}
}
