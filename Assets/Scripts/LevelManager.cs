using System;
using UnityEngine;

// this class controls the flow of the game
public class LevelManager : MonoBehaviour
{
	private PlayerCursor cursor;
	public PlayerController[] players;
	private bool playerSelectedFlag;
	private bool playerAttackRangeDisplayed;
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

	public void setPlayerAttackRangeDisplayed(bool flag) {
		playerAttackRangeDisplayed = flag;
	}

	public bool getPlayerAttackRangeDisplayed() {
		return playerAttackRangeDisplayed;
	}

	public int getEnemyIdAtPosition(Vector3 pos) {
		foreach (EnemyController enemy in enemies) {
			if (enemy.transform.position == pos) {
				return enemy.getEnemyId();
			}
		}
		return -1;
	}

	public int getPlayerIdAtPosition(Vector3 pos) {
		foreach (PlayerController player in players) {
			if (player.transform.position == pos) {
				return player.getPlayerId();
			}
		}
		return -1;		
	}

	public string getPlayerNameAtPosition(Vector3 pos) {
		foreach (PlayerController player in players) {
			if (player.transform.position == pos) {
				return player.playerName;
			}
		}
		return "";			
	}

	// sets cursorIsOnPlayer flag to false for all players except the given player id 
	public void SetCursorIsOnPlayerToFalseExcept(int playerId) {
		foreach (PlayerController player in players) {
			if (player.getPlayerId() != playerId) {
				player.setCursorIsOnPlayer(false);
			}
		}
	}

	//sets cursorIsOnPlayer flag to false for all players
	public void SetCursorIsOnPlayerToFalseAllPlayers() {
		foreach (PlayerController player in players) {
			player.setCursorIsOnPlayer(false);
		}
	}
}


