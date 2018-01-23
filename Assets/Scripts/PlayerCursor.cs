using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCursor : MonoBehaviour {

    private Vector2 cursorPosition;
    private Vector2 lastCursorPosition;
    private MapController map;
    private bool cursorOnPlayerFlag;
	private LevelManager level;
	public static Vector2 overlapBoxSize; //used to determine if cursor is overlapping with another object
	public static string mvrngDisplayed;
	public static string playerSelected;
	public static string enemyHP;
	public static string enemyName;

    // Use this for initialization
    void Start () {
        cursorPosition = transform.position;
        lastCursorPosition = transform.position;
        overlapBoxSize = new Vector2(0.5f, 0.5f);
        cursorOnPlayerFlag = false;
        mvrngDisplayed = "None";
        playerSelected = "None";
        enemyName = "None";
        enemyHP = "None";
		level = FindObjectOfType<LevelManager>();
		map = FindObjectOfType<MapController>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void TryMoveCursor(Vector2 moveDir)
    {
		if (level.getPlayerSelectedFlag()) {
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (Physics2D.OverlapBox(cursorPosition + Vector2.right, overlapBoxSize, 0))
				{
					// check if the tile to the right is within the movement range
					if (map.TileIsInMoveRange(cursorPosition + Vector2.right))
					{
						transform.Translate(Vector3.right);
						cursorPosition = transform.position;
					}
				}
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (Physics2D.OverlapBox(cursorPosition + Vector2.left, overlapBoxSize, 0))
				{
					if (map.TileIsInMoveRange(cursorPosition + Vector2.left))
					{
						transform.Translate(Vector3.left);
						cursorPosition = transform.position;
					}
				}
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (Physics2D.OverlapBox(cursorPosition + Vector2.up, overlapBoxSize, 0))
				{
					if (map.TileIsInMoveRange(cursorPosition + Vector2.up))
					{
						transform.Translate(Vector3.up);
						cursorPosition = transform.position;
					}
				}
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (Physics2D.OverlapBox(cursorPosition + Vector2.down, overlapBoxSize, 0))
				{
					if (map.TileIsInMoveRange(cursorPosition + Vector2.down))
					{
						transform.Translate(Vector3.down);
						cursorPosition = transform.position;
					}
				}
			}			
		}
        else if (Physics2D.OverlapBox(cursorPosition + moveDir, overlapBoxSize, 0))
        {
            lastCursorPosition = transform.position;
            transform.Translate(moveDir);
            cursorPosition = transform.position;
            //Debug.Log("Last cursor position: (" + lastCursorPosition.x + ", " + lastCursorPosition.y + ")");
            Debug.Log("New cursor position: (" + cursorPosition.x + ", " + cursorPosition.y + ")");
            bool foundPlayer = false;
            int foundPlayerID = -1;
            string foundPlayerName = "";
            // check to see if cursor is hovering over a player 
            foreach (PlayerController player in level.players)
            {
                if (player.transform.position == transform.position)
                {
					player.setCursorIsOnPlayer(true);
                    foundPlayer = true;
					foundPlayerID = player.getPlayerId();
                    foundPlayerName = player.playerName;
                }
                else
                {
					player.setCursorIsOnPlayer(false);
                }
            }
            if (foundPlayer)
            {
				if (!level.players[foundPlayerID].getPlayerMoved())
                {
                    DisplayPlayerMoveRange(foundPlayerID);
                    mvrngDisplayed = foundPlayerName;
                    cursorOnPlayerFlag = true;
                    //Debug.Log("Cursor on player? " + cursorOnPlayerFlag);
                }
                else
                {
                    Debug.Log("Player has already moved. Displaying Attack range");
                    DisplayAttackRange();
                }
            }
            else
            {
				if (!level.getPlayerSelectedFlag())
                {
                    map.HideMovementRange();
                    mvrngDisplayed = "None";
                }
                cursorOnPlayerFlag = false;
                //Debug.Log("Cursor on player? " + cursorOnPlayerFlag);
            }
            // check to see if cursor is hovering over an enemy
            if (!foundPlayer)
            {
				foreach (EnemyController enemy in level.enemies) {
					if (enemy.transform.position == transform.position)
					{
						enemyHP = enemy.enemyHP.ToString();
						enemyName = enemy.enemyName;
						DisplayEnemyMoveRange(enemy.getEnemyId());
					}
					else
					{
						enemyHP = "None";
						enemyName = "None";
						map.HideMovementRange();
					}
				}
            }
        }
    }

    public void DisplayPlayerMoveRange(int playerId)
    {
        map.HideMovementRange();
        foreach (PlayerController player in level.players)
        {
			if (player.getPlayerId() == playerId)
            {
                map.DisplayMovementRange(player.transform, player.moveRange);
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
				map.DisplayMovementRange(enemy.transform, enemy.enemyMoveRange);
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
                playerSelected = player.playerName;
            }
        }
		level.setPlayerSelectedFlag(true);
    }

    public void UnselectAllPlayers()
    {
        foreach (PlayerController player in level.players)
        {
			player.setPlayerSelected(false);
        }
		level.setPlayerSelectedFlag(false);
        map.HideMovementRange();
        playerSelected = "None";
        mvrngDisplayed = "None";
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
        map.HideMovementRange();
        map.DisplayAttackRange(cursorPosition + Vector2.up);
        map.DisplayAttackRange(cursorPosition + Vector2.right);
        map.DisplayAttackRange(cursorPosition + Vector2.down);
        map.DisplayAttackRange(cursorPosition + Vector2.left);
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
}
