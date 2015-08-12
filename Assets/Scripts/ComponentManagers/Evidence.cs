/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class Evidence : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer m_image;
	[SerializeField] private AudioSource m_audioSource;

	private float m_fInitVolume;
	private float m_fAudioAreaTrigger;
	private bool m_bFollowPointer;
	private Vector2 m_v2InitPosition;

	protected void OnEnable ()
	{
		GameStateMachine.OnChangeGameState += UpdateGameState;
	}

	protected void OnDisable ()
	{
		GameStateMachine.OnChangeGameState -= UpdateGameState;
	}

	protected void Awake ()
	{
		m_v2InitPosition = this.transform.position;
		m_audioSource.mute = false;
		m_fInitVolume = m_audioSource.volume;
		
		float imgSizeX = m_image.bounds.size.x * m_image.transform.localScale.x;
		float imgSizeY = m_image.bounds.size.y * m_image.transform.localScale.x;
		m_fAudioAreaTrigger = Mathf.Max(imgSizeX, imgSizeY) * 5;
	}

	protected void Update () 
	{
		if (GameStateMachine.Instance.IsGamePaused) { return; }
		if (!m_bFollowPointer) { return; }

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float sqrMagnitude = (m_v2InitPosition - mousePosition).sqrMagnitude;

		if(sqrMagnitude <= m_fAudioAreaTrigger)
		{
			m_audioSource.mute = false;

			float 	computedVolume  = 1.0f - (sqrMagnitude / m_fAudioAreaTrigger); // closer to object; louder sound
					computedVolume *= m_fInitVolume * AudioListener.volume;
					computedVolume  = Mathf.Clamp(computedVolume, 0.0f, 1.0f);
			m_audioSource.volume = computedVolume;

			if(!m_audioSource.isPlaying)
			{
				m_audioSource.Play();
			}
		}
		else
		{
			m_audioSource.Stop();
			m_audioSource.mute = true;
		}
	}

	private void UpdateGameState (GameState p_gameState)
	{
		switch(p_gameState){
		case GameState.Start:
		case GameState.Running:
		{
			m_bFollowPointer = true;

			break;
		}
		case GameState.Idle:
		case GameState.Inactive:
		case GameState.Reset:
		case GameState.Result:
		case GameState.Exit:
		{
			m_bFollowPointer = false;
			m_audioSource.mute = false;

			break;
		}}
	}
}
