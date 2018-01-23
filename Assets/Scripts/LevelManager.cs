using System;
using UnityEngine;

// this class controls the flow of the game
public class LevelManager : MonoBehaviour
{
	private PlayerCursor cursor;
	public PlayerController[] players;
	private bool playerSelectedFlag;
	public EnemyController[] enemies;
	public PlayerMovesStack stack;

	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<PlayerController>();
		Debug.Log("Number of players: " + players.Length);
		enemies = FindObjectsOfType<EnemyController>();
		Debug.Log("Number of enemies: " + enemies.Length);
		playerSelectedFlag = false;
		cursor = FindObjectOfType<PlayerCursor>();
		stack = new PlayerMovesStack();
	}

	// Update is called once per frame
	void Update () {
		if (playerSelectedFlag) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				cursor.UnselectAllPlayers();
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				//Debug.Log("Move player to this tile.");
				MoveSelectedPlayer();
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			cursor.TryMoveCursor(Vector2.right);                
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			cursor.TryMoveCursor(Vector2.left);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			cursor.TryMoveCursor(Vector2.up);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			cursor.TryMoveCursor(Vector2.down);
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			cursor.TrySelectPlayer();
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			stack.Pop();
		}
		// pressing the D key prints some debug information to the console.
		if (Input.GetKeyDown(KeyCode.D)) {
			Debug.Log("Cursor on player? " + cursor.getCursorOnPlayerFlag());
			foreach (PlayerController player in players) {
				Debug.Log ("Player ID: " + player.getPlayerId());
			}
		}
	}

	public int getSelectedPlayerId() {
		foreach (PlayerController player in players) {
			if (player.getPlayerSelected()) {
				return player.getPlayerId();
			}
		}
		return -1;
	}

	public void MoveSelectedPlayer() {
		stack.Push(getSelectedPlayerId(), players[getSelectedPlayerId()].transform.position);
		cursor.MoveSelectedPlayer();
	}

	public void setPlayerSelectedFlag(bool flag) {
		playerSelectedFlag = flag;
	}

	public bool getPlayerSelectedFlag() {
		return playerSelectedFlag;
	}
}


