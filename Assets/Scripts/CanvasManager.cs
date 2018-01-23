using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public Text valueToDisplayMovementRange;
    public Text valueToDisplayPlayerSelected;
    public Text valueToDisplayEnemyHP;
    public Text valueToDisplayEnemyName;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        valueToDisplayMovementRange.text = PlayerCursor.mvrngDisplayed;
        valueToDisplayPlayerSelected.text = PlayerCursor.playerSelected;
        valueToDisplayEnemyHP.text = PlayerCursor.enemyHP;
        valueToDisplayEnemyName.text = PlayerCursor.enemyName;
    }
}
