﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int moveRange;
    private bool playerMoved;
    private bool playerSelected;
    private int playerId;
    private bool playerMoveRangeDisplayed;
    public string playerName;
    private bool cursorIsOnPlayer;
	private static int playerIds = 0;

	// Use this for initialization
	void Start () {
        playerMoved = false;
        playerSelected = false;
        playerMoveRangeDisplayed = false;
        cursorIsOnPlayer = false;
		playerId = playerIds++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPlayerMoved(bool flag) {
		playerMoved = flag;
	}

	public bool getPlayerMoved() {
		return playerMoved;
	}

	public void setPlayerSelected(bool flag) {
		playerSelected = flag;
	}

	public bool getPlayerSelected() {
		return playerSelected;
	}

	public int getPlayerId() {
		return playerId;
	}

	public void setPlayerMoveRangeDisplayed(bool flag) {
		playerMoveRangeDisplayed = flag;
	}

	public bool getPlayerMoveRangeDisplayed() {
		return playerMoveRangeDisplayed;
	}

	public void setCursorIsOnPlayer(bool flag) {
		cursorIsOnPlayer = flag;
	}

	public bool getCursorIsOnPlayer() {
		return cursorIsOnPlayer;
	}
}