using UnityEngine;
using System.Collections;

public class EvidenceBoxManager : MonoSingleton <EvidenceBoxManager> 
{
	[SerializeField] private Transform m_evidenceContainer;
	private DisplayManager m_displayManager;
	
	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}
	
	private void UpdateEvidencePositions ()
	{

	}
	
	public void Open ()
	{
		m_displayManager.Open();
		this.transform.SetAsLastSibling ();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void AddEvidence (Transform p_evidence)
	{
		p_evidence.SetParent (m_evidenceContainer);
		p_evidence.localScale = Vector3.one;

		UpdateEvidencePositions ();
	}

	public void RemoveEvidence (Transform p_evidence)
	{
		LevelManager.Instance.AddEvidence (p_evidence);
		UpdateEvidencePositions ();
	}
}
