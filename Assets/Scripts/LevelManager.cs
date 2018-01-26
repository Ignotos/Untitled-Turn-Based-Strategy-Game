using System;
using UnityEngine;

// this class controls the flow of the game
public class LevelManager : MonoBehaviour
{
	private PlayerCursor cursor;
	public PlayerController[] players;
	private bool playerAttackRangeDisplayed;
	public EnemyController[] enemies;
	public PlayerMovesStack stack;
	private bool cursorIsOnEnemy;

	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<PlayerController>();
		Debug.Log("Number of players: " + players.Length);
		enemies = FindObjectsOfType<EnemyController>();
		Debug.Log("Number of enemies: " + enemies.Length);
		cursor = FindObjectOfType<PlayerCursor>();
		stack = new PlayerMovesStack();
	}

	// Update is called once per frame
	void Update () {
        if (!allPlayersTurnEnded())
        {
            if (isPlayerSelected())
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursor.UnselectAllPlayers();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    players[getSelectedPlayerId()].setEndPlayerTurn(true);
                    cursor.HideMovementRange();
                    players[getSelectedPlayerId()].setPlayerSelected(false);
                    Debug.Log("Ended player turn.");
                    CanvasManager.playerSelected = "None";
                }
                if (!playerAttackRangeDisplayed)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        MoveSelectedPlayer();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                cursor.TryMoveCursor(Vector2.right);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                cursor.TryMoveCursor(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursor.TryMoveCursor(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursor.TryMoveCursor(Vector2.down);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cursor.TrySelectPlayer();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                stack.Pop();
            }
            if (cursorIsOnEnemy && playerAttackRangeDisplayed)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Enemy attacked! -1HP to enemy");
                    AttackEnemy();
                    players[getSelectedPlayerId()].setPlayerAttacked(true);
                    players[getSelectedPlayerId()].setEndPlayerTurn(true);
                    cursor.UnselectAllPlayers();
                    cursor.HideMovementRange();
                    playerAttackRangeDisplayed = false;
                }
            }
            // pressing the D key prints some debug information to the console.
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Cursor on player? " + cursor.getCursorOnPlayerFlag());
                foreach (PlayerController player in players)
                {
                    Debug.Log("Player ID: " + player.getPlayerId());
                }
            }
        }
        else
        {
            Debug.Log("All player have ended their turn. Enemy phase.");
            DoEnemyPhase();
        }
	}

    public void DoEnemyPhase()
    {
        foreach(EnemyController enemy in enemies)
        {
            enemy.DoEnemyTurn();
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

	public void setPlayerAttackRangeDisplayed(bool flag) {
		playerAttackRangeDisplayed = flag;
	}

	public bool getPlayerAttackRangeDisplayed() {
		return playerAttackRangeDisplayed;
	}

	public bool getCursorIsOnEnemy() {
		return cursorIsOnEnemy;
	}

	public void setCursorIsOnEnemy(bool flag) {
		cursorIsOnEnemy = flag;
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

	public void AttackEnemy() {
		enemies[getEnemyIdAtPosition(cursor.transform.position)].enemyHP--;
		CanvasManager.enemyHP = enemies[getEnemyIdAtPosition(cursor.transform.position)].enemyHP.ToString();
	}

    public bool allPlayersTurnEnded()
    {
        foreach (PlayerController player in players)
        {
            if (!player.getEndPlayerTurn())
            {
                return false;
            }
        }
        return true;
    }

    public bool isPlayerSelected()
    {
        foreach (PlayerController player in players)
        {
            if (player.getPlayerSelected())
            {
                return true;
            }
        }
        return false;
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


