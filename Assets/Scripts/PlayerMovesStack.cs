using System;
using System.Collections.Generic;
using UnityEngine;

// This class keeps track of all player moves using a stack. This makes it possible to undo previous moves. 
public class PlayerMovesStack : MonoBehaviour
{
	private Stack<int> movedPlayerIds;
	private Stack<Vector3> positionsMovedFrom;
	private LevelManager level;

	public PlayerMovesStack() {
		movedPlayerIds = new Stack<int>();
		positionsMovedFrom = new Stack<Vector3>();
		level = FindObjectOfType<LevelManager>();
	}

	public void Push(int movedPlayerId, Vector3 positionMovedFrom) {
		movedPlayerIds.Push(movedPlayerId);
		positionsMovedFrom.Push(positionMovedFrom);
		Debug.Log ("Pushed Player ID " + movedPlayerId + " moved to (" + positionMovedFrom.x + ", " + positionMovedFrom.y + ") onto the stack."); 
	}

	public void Pop() {
		if (movedPlayerIds.Count > 0) {
			int movedPlayerId = movedPlayerIds.Pop(); 
			level.players[movedPlayerId].transform.position = positionsMovedFrom.Pop();
		}
	}
}

