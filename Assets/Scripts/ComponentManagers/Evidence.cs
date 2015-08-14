/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class Evidence : MonoBehaviour 
{
	[SerializeField] private int m_iScore;
	[SerializeField] private SpriteRenderer m_image;
	[SerializeField] private AudioSource m_audioSource;

	private float m_fInitVolume;
	private float m_fAudioAreaTrigger;
	private bool m_bFollowPointer;
	private Vector2 m_v2InitPosition;
	private Vector2 m_v2CurrPosition;

	public int Score { get { return m_iScore; }}
	public bool Selected { get; set; }

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
		m_v2InitPosition = m_v2CurrPosition = this.transform.position;
		m_audioSource.mute = false;
		m_fInitVolume = m_audioSource.volume;
		
		float imgSizeX = m_image.bounds.size.x * m_image.transform.localScale.x;
		float imgSizeY = m_image.bounds.size.y * m_image.transform.localScale.x;
		m_fAudioAreaTrigger = Mathf.Max(imgSizeX, imgSizeY) * 5;

		Selected = false;
	}

	protected void Update () 
	{
		//Debug.Log("EVIDENCE!");

		if (GameStateMachine.Instance.IsGamePaused) { return; }
		//Debug.Log("NOT PAUSED!");
		//Debug.Log("m_bFollowPointer: " + m_bFollowPointer);
		if (!m_bFollowPointer) { return; }

		//Debug.Log("FOLLOW POINTER");

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float sqrMagnitude = (m_v2CurrPosition - mousePosition).sqrMagnitude;

		if(sqrMagnitude <= m_fAudioAreaTrigger)
		{
			//Debug.Log("PLAY!");
			m_audioSource.mute = false;

			float 	computedVolume  = 1.0f - (sqrMagnitude / m_fAudioAreaTrigger); // closer to object; louder sound
					computedVolume *= m_fInitVolume * AudioListener.volume;
					computedVolume  = Mathf.Clamp(computedVolume, 0.0f, 1.0f);
			m_audioSource.volume = computedVolume;

			if(!m_audioSource.isPlaying)
			{
				m_audioSource.Play();
			}

			CheckSelectionStatus ();
		}
		else
		{
			//Debug.Log("NO PLAY!");
			m_audioSource.Stop();
			m_audioSource.mute = true;
		}
	}

	private void CheckSelectionStatus ()
	{
		if (Input.GetMouseButtonUp (0))
		{
			m_bFollowPointer = false;
			m_audioSource.mute = true;
			m_audioSource.Stop();

			Selected = !Selected;

			if (Selected)
			{
				EvidenceBoxManager.Instance.AddEvidence (this.transform);
				m_image.sortingOrder = 1;
			}
			else
			{
				EvidenceBoxManager.Instance.RemoveEvidence (this.transform);
				this.transform.localPosition = m_v2InitPosition;
				m_image.sortingOrder = 0;
			}
			
			m_v2CurrPosition = this.transform.localPosition;
		}
	}

	private void UpdateGameState (GameState p_gameState)
	{
		switch(p_gameState){
		case GameState.Start:
		case GameState.Running:
		{
			m_bFollowPointer = !Selected;
			//Debug.Log("[RUNNING] m_bFollowPointer: " + m_bFollowPointer);
			break;
		}
		case GameState.EvidenceBox:
		{
			m_bFollowPointer = Selected;
			//Debug.Log("[EVIDENCE] m_bFollowPointer: " + m_bFollowPointer);
			break;
		}
		case GameState.Idle:
		case GameState.Inactive:
		case GameState.Result:
		{
			m_bFollowPointer = false;
			m_audioSource.mute = false;
			//Debug.Log("[RESULT] m_bFollowPointer: " + m_bFollowPointer);
			break;
		}
		case GameState.Reset:
		{
			m_bFollowPointer = true;
			break;
		}
		case GameState.Exit:
		{
			//Reset ();
			m_bFollowPointer = false;
			m_audioSource.mute = false;
			//Debug.Log("[RESET / EXIT] m_bFollowPointer: " + m_bFollowPointer);
			break;
		}}
	}

	public void Reset ()
	{
		EvidenceBoxManager.Instance.RemoveEvidence (this.transform);
		this.transform.localPosition = m_v2CurrPosition = m_v2InitPosition;
		m_image.sortingOrder = 0;
		Selected = false;
	}
}
