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

	// Use this for initialization
	void Start () {
		enemyId = enemyIds++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getEnemyId() {
		return enemyId;
	}

    public void DoEnemyTurn()
    {
        // find closest player to this enemy (player pos - enemy position)

        // find direction to move in (TBD) 

        // move in that direction

        // if there is a player adjacent to the enemy, attack them

    }
}
