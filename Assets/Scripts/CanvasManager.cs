using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Text valueToDisplayPlayerHP;
	public Text valueToDisplayPlayerName;
    public Text valueToDisplayMovementRange;
    public Text valueToDisplayPlayerSelected;
    public Text valueToDisplayEnemyHP;
    public Text valueToDisplayEnemyName;
	public static string playerHP;
	public static string playerName;
	public static string mvrngDisplayed;
	public static string playerSelected;
	public static string enemyHP;
	public static string enemyName;

	// Use this for initialization
	void Start () {
		playerHP = "None";
		playerName = "None";
		mvrngDisplayed = "None";
		playerSelected = "None";
		enemyName = "None";
		enemyHP = "None";
    }
	
	// Update is called once per frame
	void Update () {
		valueToDisplayPlayerHP.text = playerHP;
		valueToDisplayPlayerName.text = playerName;
        valueToDisplayMovementRange.text = mvrngDisplayed;
        valueToDisplayPlayerSelected.text = playerSelected;
        valueToDisplayEnemyHP.text = enemyHP;
        valueToDisplayEnemyName.text = enemyName;
    }
}
