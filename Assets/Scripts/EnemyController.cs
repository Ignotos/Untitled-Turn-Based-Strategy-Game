using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public string enemyName;
	public int enemyHP;  
    public int enemyMoveRange;
    public int enemyAttackRange;
	private int enemyId;  
	private static int enemyIds = 0;
	private LevelManager level;

	// Use this for initialization
	void Start () {
		enemyId = enemyIds++;
		level = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getEnemyId() {
		return enemyId;
	}

    public void DoEnemyTurn()
    {
		//move enemy toward the closest player
		MoveEnemy(GetClosestPlayer());

        // if there is a player adjacent to the enemy, attack them
		// to do
    }

	public int GetClosestPlayer() {
		int closestPlayerId = 0;
		float currentMinDistance = 0.0f;
		foreach (PlayerController player in level.players) {
			Vector2 enemyPos2D = transform.position;
			Vector2 playerPos2D = player.transform.position;
			float distance = Vector2.Distance(enemyPos2D, playerPos2D);
			if (currentMinDistance > distance || currentMinDistance <= 0.0f) {
				currentMinDistance = distance;
				closestPlayerId = player.getPlayerId();
			}
		}
		Debug.Log ("Min distance is " + currentMinDistance + " from Player " + closestPlayerId);
		return closestPlayerId;
	}

	public void MoveEnemy(int targetPlayerId) {
		Vector2 enemyPos2D = transform.position;
		Vector2 playerPos2D = level.players[targetPlayerId].transform.position;

		//Vector2 directionTowardPlayer = (enemyPos2D - playerPos2D).normalized;
		Vector2 directionTowardPlayer = playerPos2D - enemyPos2D;
		Debug.Log("Direction toward player: (" + directionTowardPlayer.x + "," + directionTowardPlayer.y + ")");

		if (Mathf.Abs(directionTowardPlayer.x) > Mathf.Abs(directionTowardPlayer.y)) {
			// greater x distance, move along x 
			if (directionTowardPlayer.x > 0) {
				transform.Translate(Vector3.right);
			} 
			else {
				transform.Translate(Vector3.left);
			}
		}
		else {
			// greater y distance, move along y 
			if (directionTowardPlayer.y > 0) {
				transform.Translate(Vector3.up);
			} 
			else {
				transform.Translate(Vector3.down);
			}
		}
	}
}
