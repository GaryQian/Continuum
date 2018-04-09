using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;
	public GameObject panel;
	private Image redPanel;
	public float damageFlashDuration;
	public Image energyBarImage; 

	void Awake() {
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		redPanel = panel.GetComponent<Image> ();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void FlashRedOnDamage() {
		redPanel.enabled = true;
		Invoke ("CancelRedFlash", damageFlashDuration);
	}

	void CancelRedFlash() {
		redPanel.enabled = false;
	}
}
