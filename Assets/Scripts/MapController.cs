using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DisplayMovementRangeDiagonal(Transform playerPosition, int mvrngDiagX, int mvrngDiagY, int dirX, int dirY, string tileTag)
    {
        if (mvrngDiagX == 0 || mvrngDiagY == 0)
        {
            return;
        }
        Vector2 playerPosition2D = playerPosition.position;
        Vector2 grassTilePosition2D = new Vector2();
        Vector2 diagTilePosition2D = new Vector2(mvrngDiagX, mvrngDiagY);
        foreach (Transform child in transform)
        {
            //int mvrng = playerPosition.gameObject.GetComponent<PlayerController>().moveRange;
            if (child.tag == "GrassTile")
            {
                grassTilePosition2D = child.transform.position;
                ////Debug.Log("Grass Tile Position: " + grassTilePosition2D.x + ", " + grassTilePosition2D.y);
                ////Debug.Log("Player Position: " + playerPosition2D.x + ", " + playerPosition2D.y);
                if ((playerPosition2D + diagTilePosition2D) == grassTilePosition2D)
                {
                    foreach (Transform movementRangeSprite in child)
                    {
                        if (movementRangeSprite.tag == tileTag)
                        {
                            //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                            movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                        }
                    }
                    DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX + dirX, mvrngDiagY + dirY, dirX, dirY, tileTag);
                }
            }
        }
    }

    public void altDisplayMovementRange(Transform playerPosition)
    {
        //Debug.Log("Player movement range: " + playerPosition.gameObject.GetComponent<PlayerController>().moveRange);
        Vector2 playerPosition2D = playerPosition.position;
        //Debug.Log("Player position : (" + playerPosition2D.x + ", " + playerPosition2D.y + ")");
        Vector2 grassTilePosition2D = new Vector2();
        foreach (Transform child in transform)
        {
            int mvrng = playerPosition.gameObject.GetComponent<PlayerController>().moveRange;
            while (mvrng > 0)
            {
                // check if (px + mvrng, py) exists
                float px = playerPosition2D.x;
                float py = playerPosition2D.y;
                Vector2 target = new Vector2(px + mvrng, py);
                if (TileExistsAt(target))
                {
                    // tile at (px + mvrng, py) exists. 
                    // Highlight tile
                    //Debug.Log("Found grass tile at: (" + grassTilePosition2D.x + "," + grassTilePosition2D.y + ")");
                    foreach (Transform movementRangeSprite in child)
                    {
                        if (movementRangeSprite.tag == "MoveRange")
                        {
                            //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                            movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            //Debug.Log("Found Movement Range Sprite. Enabled?: " + movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled);
                            ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                        }
                        else
                        {
                            //Debug.Log("In else branch. Movement Range Sprite tag: " + movementRangeSprite.tag);
                        }
                    }
                }

            }
        }
    }

    public void DisplayMovementRange(Transform playerPosition, int moveRange)
    {
        
        //Debug.Log("Player movement range: " + playerPosition.gameObject.GetComponent<PlayerController>().moveRange);
        Vector2 playerPosition2D = playerPosition.position;
        //Debug.Log("Player position : (" + playerPosition2D.x + ", " + playerPosition2D.y + ")");
        Vector2 grassTilePosition2D = new Vector2();
        foreach (Transform child in transform)
        {
            int mvrng = moveRange;
            if (child.tag == "GrassTile")
            {
                grassTilePosition2D = child.transform.position;
                //Debug.Log("Grass tile position: (" + grassTilePosition2D.x + ", " + grassTilePosition2D.y + ")"); 
                ////Debug.Log("Grass Tile Position: " + grassTilePosition2D.x + ", " + grassTilePosition2D.y);
                ////Debug.Log("Player Position: " + playerPosition2D.x + ", " + playerPosition2D.y);
				if (playerPosition2D == grassTilePosition2D) {
					foreach (Transform movementRangeSprite in child)
					{
						if (movementRangeSprite.tag == "MoveRange")
						{
							//movementRangeSprite.Translate(new Vector3(0, 0, -10));
							movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
							//Debug.Log("Found Movement Range Sprite. Enabled?: " + movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled);
							////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
						}
						else
						{
							//Debug.Log("In else branch. Movement Range Sprite tag: " + movementRangeSprite.tag);
						}
					}
				}
                while (mvrng > 0)
                {
                    if ((playerPosition2D + new Vector2(mvrng, 0)) == grassTilePosition2D)
                    {
                        //Debug.Log("Found grass tile at: (" + grassTilePosition2D.x + "," + grassTilePosition2D.y + ")");
                        foreach (Transform movementRangeSprite in child)
                        {
                            if (movementRangeSprite.tag == "MoveRange")
                            {
                                //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                                movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                //Debug.Log("Found Movement Range Sprite. Enabled?: " + movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled);
                                ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                            }
                            else
                            {
                                //Debug.Log("In else branch. Movement Range Sprite tag: " + movementRangeSprite.tag);
                            }
                        }
                        // check diagonal minus x plus y 
                        int dirX = -1;
                        int dirY = 1;
                        int mvrngDiagX = mvrng + dirX;
                        int mvrngDiagY = 1;
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, mvrngDiagY, dirX, dirY, "MoveRange");
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, -1, dirX, -1, "MoveRange");
                        // display attack range
                        if (mvrng == moveRange)
                        {
                            DisplayAttackRange(playerPosition2D + new Vector2(mvrng + 1, 0));
                            // display diagonal attack range 
                            DisplayMovementRangeDiagonal(playerPosition, ++mvrngDiagX, mvrngDiagY, dirX, dirY, "AttackRange");
                            DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, -1, dirX, -1, "AttackRange");
                        }
                    }
                    if ((playerPosition2D + new Vector2(0, mvrng)) == grassTilePosition2D)
                    {
                        foreach (Transform movementRangeSprite in child)
                        {
                            if (movementRangeSprite.tag == "MoveRange")
                            {
                                //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                                movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                            }
                        }
                        // check diagonal minus x minus y 
                        int dirX = -1;
                        int dirY = -1;
                        int mvrngDiagX = -1;
                        int mvrngDiagY = mvrng + dirY;
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, mvrngDiagY, dirX, dirY, "MoveRange");
                        DisplayMovementRangeDiagonal(playerPosition, 1, mvrngDiagY, 1, dirY, "MoveRange");
                        // display attack range
                        if (mvrng == moveRange)
                        {
                            DisplayAttackRange(playerPosition2D + new Vector2(0, mvrng + 1));
                            // display diagonal attack range 
                            DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, ++mvrngDiagY, dirX, dirY, "AttackRange");
                            DisplayMovementRangeDiagonal(playerPosition, 1, mvrngDiagY, 1, dirY, "AttackRange");
                        }
                    }
                    if ((playerPosition2D + new Vector2(-mvrng, 0)) == grassTilePosition2D)
                    {
                        foreach (Transform movementRangeSprite in child)
                        {
                            if (movementRangeSprite.tag == "MoveRange")
                            {
                                //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                                movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                            }
                        }
                        // check diagonal plus x minus y 
                        int dirX = 1;
                        int dirY = -1;
                        int mvrngDiagX = -mvrng + dirX;
                        int mvrngDiagY = -1;
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, mvrngDiagY, dirX, dirY, "MoveRange");
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, 1, dirX, 1, "MoveRange");
                        // display attack range
                        if (mvrng == moveRange)
                        {
                            DisplayAttackRange(playerPosition2D + new Vector2(-mvrng - 1, 0));
                            // display diagonal attack range 
                            DisplayMovementRangeDiagonal(playerPosition, --mvrngDiagX, mvrngDiagY, dirX, dirY, "AttackRange");
                            DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, 1, dirX, 1, "AttackRange");
                        }
                    }
                    if ((playerPosition2D + new Vector2(0, -mvrng)) == grassTilePosition2D)
                    {
                        foreach (Transform movementRangeSprite in child)
                        {
                            if (movementRangeSprite.tag == "MoveRange")
                            {
                                //movementRangeSprite.Translate(new Vector3(0, 0, -10));
                                movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                            }
                        }
                        // check diagonal plus x plus y 
                        int dirX = 1;
                        int dirY = 1;
                        int mvrngDiagX = 1;
                        int mvrngDiagY = -mvrng + dirY;
                        DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, mvrngDiagY, dirX, dirY, "MoveRange");
                        DisplayMovementRangeDiagonal(playerPosition, -1, mvrngDiagY, -1, dirY, "MoveRange");
                        // display attack range
                        if (mvrng == moveRange)
                        {
                            DisplayAttackRange(playerPosition2D + new Vector2(0, -mvrng - 1));
                            // display diagonal attack range 
                            DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, --mvrngDiagY, dirX, dirY, "AttackRange");
                            DisplayMovementRangeDiagonal(playerPosition, mvrngDiagX, mvrngDiagY, -1, dirY, "AttackRange");
                        }
                    }
                    mvrng--;
                }
            }
            else
            {
                //Debug.Log("In else branch. Child.tag = " + child.tag);
            }
        }
    }

	// disable the movement range sprite for all tiles 
    public void HideMovementRange()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "GrassTile")
            {
                foreach (Transform movementRangeSprite in child)
                {
                    ////Debug.Log("Move Range Sprite invisible");
                    //movementRangeSprite.Translate(new Vector3(0, 0, 10));
                    movementRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    ////Debug.Log("MovementRangeSprite z-axis position: " + movementRangeSprite.transform.position.z);
                }
            }
        }
    }

    //sets tile to display attack range, if it exists
    public void DisplayAttackRange(Vector2 tilePosition)
    {
        Vector2 grassTilePosition2D = new Vector2();
        foreach (Transform child in transform)
        {
            grassTilePosition2D = child.transform.position;
            if (grassTilePosition2D == tilePosition)
            {
                foreach (Transform attackRangeSprite in child)
                {
                    if (attackRangeSprite.tag == "AttackRange")
                    {
                        attackRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
    }

	// checks if a given tile is in the currently displayed movement range
	public bool TileIsInRange(Vector2 tilePosition, string spriteTag)
    {
        Vector2 grassTilePosition2D = new Vector2();
        foreach (Transform child in transform)
        {
            grassTilePosition2D = child.transform.position;
            if (grassTilePosition2D == tilePosition)
            {
                foreach (Transform moveRangeSprite in child)
                {
					if (moveRangeSprite.tag == spriteTag)
                    {
                        if (moveRangeSprite.gameObject.GetComponent<SpriteRenderer>().enabled)
                        {
                            return true;
                        }                        
                    }
                }
            }
        }
        return false;
    }

	// checks if a tile exists at the given position
    public bool TileExistsAt(Vector2 tilePosition)
    {
        Vector2 grassTilePosition2D = new Vector2();
        foreach (Transform child in transform)
        {
            grassTilePosition2D = child.transform.position;
            if (grassTilePosition2D == tilePosition)
            {
                return true;
            }
        }
        return false;
    }
}
