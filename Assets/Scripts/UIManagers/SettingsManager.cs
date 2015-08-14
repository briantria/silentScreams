/*
 * Brian Tria
 * Aug 12, 2015
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoSingleton <SettingsManager> 
{
	[SerializeField] private Button m_btnResume;
	[SerializeField] private Button m_btnExit;
	[SerializeField] private Button m_btnBack;
	[SerializeField] private Slider m_bgmSlider;
	[SerializeField] private Slider m_sfxSlider;

	private DisplayManager m_displayManager;
	
	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}
	
	public void Open ()
	{
		m_displayManager.Open();
		this.transform.SetAsLastSibling ();
		m_btnResume.gameObject.SetActive (true);
		m_btnExit.gameObject.SetActive (true);
		m_btnBack.gameObject.SetActive (false);
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void DisplaySlidersOnly ()
	{
		m_btnResume.gameObject.SetActive (false);
		m_btnExit.gameObject.SetActive (false);
		m_btnBack.gameObject.SetActive (true);
	}

	public void OnClickResume ()
	{
		Close ();
		GameStateMachine.Instance.IsGamePaused = false;
	}

	public void OnClickExitToMenu ()
	{
		Close ();

		UIStateMachine.Instance.ChangeUIState (UIState.OnTitleScreen);
		GameStateMachine.Instance.ChangeGameState (GameState.Exit);
	}

	public void OnBgmSliderValueChange ()
	{
		BgmManager.Instance.SetBGMVolume (m_bgmSlider.value);
	}

	public void OnSfxSliderValueChange ()
	{
		AudioListener.volume = m_sfxSlider.value;
	}
}
