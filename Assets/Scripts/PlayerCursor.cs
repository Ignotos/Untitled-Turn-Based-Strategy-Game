using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCursor : MonoBehaviour {

    private Vector2 cursorPosition2D;
    private Vector2 lastCursorPosition2D;
    private bool cursorOnPlayerFlag;
	private LevelManager level;
	public static Vector2 overlapBoxSize; //used to determine if cursor is overlapping with another object

    // Use this for initialization
    void Start () {
        cursorPosition2D = transform.position;
        lastCursorPosition2D = transform.position;
        overlapBoxSize = new Vector2(0.5f, 0.5f);
        cursorOnPlayerFlag = false;
		level = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {

    }

	public void MoveIfTileInRange(Vector3 moveDir, string spriteTag) {
		Vector2 moveDir2D = moveDir;
		if (Physics2D.OverlapBox(cursorPosition2D + moveDir2D, overlapBoxSize, 0))
		{
			// check if the tile to the right is within the movement range
			if (level.getMap().TileIsInRange(cursorPosition2D + moveDir2D, spriteTag))
			{
				transform.Translate(moveDir);
				cursorPosition2D = transform.position;
				if (CursorIsOnEnemy()) 
				{
					DisplayEnemyInfo(level.getEnemyIdAtPosition(transform.position));
					Debug.Log("Cursor is on enemy. Press space to attack!");
				} 
				else 
				{
					HideEnemyInfo();
				}
			}
		}
	}

    public void TryMoveCursor(Vector2 moveDir)
    {
		if (level.isPlayerSelected()) {
			string spriteTag = "";
			if (level.getPlayerAttackRangeDisplayed()) {
				spriteTag = "AttackRange";
			} 
			else {
				spriteTag = "MoveRange";	
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				MoveIfTileInRange(Vector3.right, spriteTag);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				MoveIfTileInRange(Vector3.left, spriteTag);
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				MoveIfTileInRange(Vector3.up, spriteTag);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				MoveIfTileInRange(Vector3.down, spriteTag);
			}		
		}
        else if (Physics2D.OverlapBox(cursorPosition2D + moveDir, overlapBoxSize, 0))
        {
            lastCursorPosition2D = transform.position;
            transform.Translate(moveDir);
            cursorPosition2D = transform.position;
            Debug.Log("New cursor position: (" + cursorPosition2D.x + ", " + cursorPosition2D.y + ")");
			if (CursorIsOnPlayer()) {
				level.players[level.getPlayerIdAtPosition(transform.position)].setCursorIsOnPlayer(true);
				level.SetCursorIsOnPlayerToFalseExcept(level.getPlayerIdAtPosition(transform.position));
				if (!level.players[level.getPlayerIdAtPosition(transform.position)].getPlayerMoved())
				{
					DisplayPlayerMoveRange(level.getPlayerIdAtPosition(transform.position));
					CanvasManager.mvrngDisplayed = level.getPlayerNameAtPosition(transform.position);
					cursorOnPlayerFlag = true;
				}
				else
				{
					if (!level.players[level.getPlayerIdAtPosition(transform.position)].getPlayerAttacked()) {
						Debug.Log("Player has already moved. Displaying Attack range");
						DisplayAttackRange();
					}
				}
				DisplayPlayerInfo(level.getPlayerIdAtPosition(transform.position));
			} 
			else {
				level.SetCursorIsOnPlayerToFalseAllPlayers();
				if (!level.isPlayerSelected())
				{
					level.getMap().HideMovementRange();
					CanvasManager.mvrngDisplayed = "None";
				}
				cursorOnPlayerFlag = false;
				HidePlayerInfo();
				//Debug.Log("Cursor on player? " + cursorOnPlayerFlag);
				if (CursorIsOnEnemy())
				{
					DisplayEnemyInfo(level.getEnemyIdAtPosition(transform.position));
				}
				else
				{
					HideEnemyInfo();
				}
			}
        }
    }

	public void HidePlayerInfo() {
		CanvasManager.playerHP = "None";
		CanvasManager.playerName = "None";
	}

	public void DisplayPlayerInfo(int playerId) {
		CanvasManager.playerHP = level.players[playerId].playerHP.ToString();
		CanvasManager.playerName = level.players[playerId].playerName;
	}

	public bool CursorIsOnPlayer() {
		foreach (PlayerController player in level.players)
		{
			if (player.transform.position == transform.position)
			{
				return true;
			}
		}
		return false;
	}


	public void DisplayEnemyInfo(int enemyId) {
		CanvasManager.enemyHP = level.enemies[enemyId].enemyHP.ToString();
		CanvasManager.enemyName = level.enemies[enemyId].enemyName;
		if (!level.getPlayerAttackRangeDisplayed()) {
			DisplayEnemyMoveRange(enemyId);
		}
	}

	public void HideEnemyInfo() {
		CanvasManager.enemyHP = "None";
		CanvasManager.enemyName = "None";
		if (!level.isPlayerSelected()) {
			level.getMap().HideMovementRange();		
		}
	}

	public bool CursorIsOnEnemy() {
		foreach (EnemyController enemy in level.enemies) {
			if (enemy.transform.position == transform.position) {
				level.setCursorIsOnEnemy(true);
				return true;
			}
		}
		level.setCursorIsOnEnemy(false);
		return false;
	}

    public void DisplayPlayerMoveRange(int playerId)
    {
		level.getMap().HideMovementRange();
        foreach (PlayerController player in level.players)
        {
			if (player.getPlayerId() == playerId)
            {
				level.getMap().DisplayMovementRange(player.transform, player.moveRange);
				player.setPlayerMoveRangeDisplayed(true);
            }
            else
            {
				player.setPlayerMoveRangeDisplayed(false);
            }
        }
    }

	public void DisplayEnemyMoveRange(int enemyId)
    {
		foreach (EnemyController enemy in level.enemies) {
			if (enemy.getEnemyId() == enemyId) {
				level.getMap().DisplayMovementRange(enemy.transform, enemy.enemyMoveRange);
				break;
			}
		}
    }

    public void SelectPlayer(int playerId)
    {
        foreach (PlayerController player in level.players)
        {
			if (player.getPlayerId() != playerId)
            {
				player.setPlayerSelected(false);
            }
            else
            {
				player.setPlayerSelected(true);
				CanvasManager.playerSelected = player.playerName;
            }
        }
    }

    public void UnselectAllPlayers()
    {
        foreach (PlayerController player in level.players)
        {
			player.setPlayerSelected(false);
			player.setPlayerAttackRangeDisplayed (false);
        }
		level.getMap().HideMovementRange();
		CanvasManager.playerSelected = "None";
		CanvasManager.mvrngDisplayed = "None";
        Debug.Log("Player unselected");
    }

    public void MoveSelectedPlayer()
    {
        int playerId = -1;
        foreach (PlayerController player in level.players)
        {
			if (player.getPlayerSelected())
            {
                player.transform.position = transform.position;
				playerId = player.getPlayerId();
				player.setPlayerMoved(true);
                break;
            }
        }
        Debug.Log("Player moved to selected tile.");
        //UnselectAllPlayers();
        DisplayAttackRange();
    }

    public bool MovementRangeDisplayed()
    {
        foreach (PlayerController player in level.players)
        {
			if (player.getPlayerMoveRangeDisplayed())
            {
                return true;
            }
        }
        return false;
    }
    
    public void DisplayAttackRange()
    {
		level.getMap().HideMovementRange();
		level.getMap().DisplayAttackRange(cursorPosition2D + Vector2.up);
		level.getMap().DisplayAttackRange(cursorPosition2D + Vector2.right);
		level.getMap().DisplayAttackRange(cursorPosition2D + Vector2.down);
		level.getMap().DisplayAttackRange(cursorPosition2D + Vector2.left);
		level.getMap().DisplayAttackRange(cursorPosition2D);
		level.players[level.getPlayerIdAtPosition(transform.position)].setPlayerAttackRangeDisplayed(true);
    }

	public void TrySelectPlayer() {
		if (cursorOnPlayerFlag) {
			foreach (PlayerController player in level.players) {
				if (player.getCursorIsOnPlayer ()) {
					SelectPlayer(player.getPlayerId());
					break;
				}
			}
		}
	}

	public bool getCursorOnPlayerFlag(){
		return cursorOnPlayerFlag;
	}

	public void setCursorOnPlayerFlag(bool flag) {
		cursorOnPlayerFlag = flag;
	}

	public void HideMovementRange() {
		level.getMap().HideMovementRange();
	}
}
