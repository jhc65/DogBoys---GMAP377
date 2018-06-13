﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    private static GameController instance = null;
    [SerializeField]
    public GameObject currentlySelectedCharacter = null;
	public List<GameObject> p1Chars = new List<GameObject>();
	public List<GameObject> p2Chars = new List<GameObject>();
	public string turn = "";
	public bool attackMode = false;
    private bool gameOver = false;
	/*
	 * Board status
	 * 0 - empty
	 * 1 - character
	 * 2 - half cover
	 * 3 - full cover
	 * 4 - dead
	 * */
	private int[,] gameBoard = new int[32, 24] {
        { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4},
        { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 1, 0, 1, 0, 1, 0, 4, 4, 4, 4, 4, 4, 4, 4},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3},
        { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4},
        { 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4},
        { 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4},
        { 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4},
        { 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4},
        { 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3},
        { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0, 0, 0, 0, 0, 3, 4, 4, 4, 4, 4, 3, 0, 0},
        { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 1, 0, 1, 0, 1, 0, 4, 4, 4, 4, 4, 4, 4, 4},
        { 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4}
    };

    #region Getters and Setters
    public static GameController Instance {
        get { return instance; }
    }

	public void setSpace(int x, int y, int status){
		gameBoard [y,x] = status;
	}

	public int getSpace(int x, int y){
		return gameBoard[y,x];
	}

    public int[,] getBoard() {
        return gameBoard;
    }
    #endregion
    #endregion

    #region GameController Custom Methods
    public bool HasSelectedCharacter() {
        if (currentlySelectedCharacter) {
            return true;
        }
        else {
            return false;
        }
    }

    // Check for cover
    public bool IsEnemyProtected(GameObject charIn, GameObject enemyIn) {// Vector3 charIn, Vector3 enemyIn) {//float charX, float charY, float enemyX, float enemyY) {
        if (charIn.GetComponent<Character>().GetCoverDirection() == Constants.Global.C_CoverDirection.N) {
            if (Mathf.Round(enemyIn.transform.position.z) <= Mathf.Round(charIn.transform.position.z)) {
                return false;
            }
        }
        else if (charIn.GetComponent<Character>().GetCoverDirection() == Constants.Global.C_CoverDirection.S) {
            if (Mathf.Round(enemyIn.transform.position.z) >= Mathf.Round(charIn.transform.position.z)) {
                return false;
            }
        }
        else if (charIn.GetComponent<Character>().GetCoverDirection() == Constants.Global.C_CoverDirection.E) {
            if (Mathf.Round(enemyIn.transform.position.x) <= Mathf.Round(charIn.transform.position.x)) {
                return false;
            }
        }
        else {
            if (Mathf.Round(enemyIn.transform.position.x) >= Mathf.Round(charIn.transform.position.x)) {
                return false;
            }
        }

        return true;

        //if (turn == "P2" && charIn.z <= enemyIn.z) {
        //    return false;
        //}
        //else if (turn == "P1" && charIn.z >= enemyIn.z) {
        //    return false;
        //}
        //else {
        //    return true;
        //}

        //if (charIn.x == enemyIn.x) {
        //    return true;
        //}

        //Vector3 charV;
        //Vector3 enemyV;
        //enemyV = new Vector3( (enemyIn.x * -1), enemyIn.y, enemyIn.z);
        //charV = new Vector3(0, charIn.y, charIn.z);

        //if ((90 - Vector3.Angle(charV, enemyIn)) < 30f) {
        //    return false;
        //}
        //else {
        //    return true;
        //}
    }

    public void SetSelectedCharacter(GameObject charIn) {
        currentlySelectedCharacter = charIn;
    }

    private void UnselectCharacter() {
		if (attackMode)
			toggleAttackMode ();
        currentlySelectedCharacter.GetComponent<Character>().UnselectCharacter();
        currentlySelectedCharacter = null;

    }

    public void MoveSelectedCharacter(Vector3 position) {
        if (currentlySelectedCharacter) {
            currentlySelectedCharacter.GetComponent<Character>().Move(position);
        }
    }

    public void MoveSelectedCharacter(Vector3 position, bool inCover) {
        if (currentlySelectedCharacter) {
            currentlySelectedCharacter.GetComponent<Character>().Move(position, inCover);
        }
    }

    public void MoveSelectedCharacter(Vector3 position, bool inCover, Constants.Global.C_CoverDirection dirIn) {
        if (currentlySelectedCharacter) {
            currentlySelectedCharacter.GetComponent<Character>().Move(position, inCover, dirIn);
        }
    }

    public void MoveSelectedCharacter_NoHit(Vector3 position)
    {
        if (currentlySelectedCharacter) {
            currentlySelectedCharacter.GetComponent<Character>().Move_NoHit(position);
        }
    }

    private bool p1CanMove() {
		foreach (GameObject chara in p1Chars) {
			if (chara.GetComponent<Character> ().getMovesLeft () > 0) {
				Debug.Log ("P1 can still move");
				return true;
			}
		}
		return false;
	}

	private bool p2CanMove() {
		foreach (GameObject chara in p2Chars) {
			if (chara.GetComponent<Character> ().getMovesLeft () > 0) {
				Debug.Log ("P2 can still move");
				return true;
			}
		}
		return false;
	}

	public void updateTurns(){
		if (turn == "P1" && !p1CanMove ()) {
			StartP2Turn ();
		} else if (turn == "P2" && !p2CanMove ()) {
			StartP1Turn ();
		}
	}

	private void StartP1Turn() {
		Debug.Log ("P1 turn start");
		foreach (GameObject chara in p2Chars) {
			chara.GetComponent<Character> ().setMovesLeft (0);
		}
		foreach (GameObject chara in p1Chars) {
			chara.GetComponent<Character> ().setMovesLeft (2);
		}

		turn = "P1";
	}

	private void StartP2Turn() {
		Debug.Log ("P2 turn start");
		foreach (GameObject chara in p1Chars) {
			chara.GetComponent<Character> ().setMovesLeft (0);
		}
		foreach (GameObject chara in p2Chars) {
			chara.GetComponent<Character> ().setMovesLeft (2);
		}

		turn = "P2";
	}

	public void printBoard() {
		string Out = "";
		for (int i = 0; i < 32; i++) {
			for (int j = 0; j < 24; j++) {
				Out += gameBoard [i, j].ToString ();
			}
			Out += '\n';
		}
		Debug.Log (Out);
	}


    public void lineOfSight()
    {
        GameObject whatDidIHit = null;
        if (turn == "P1")
        {
            //Make sure that the current player's team is active 
            foreach (GameObject friendly in p1Chars)
            {
                friendly.GetComponent<Character>().turnOnGameObject();
            }

            //Each frame reset the enemy can be seen boolean to false
            foreach (GameObject enemy in p2Chars)
            {
                enemy.GetComponent<Character>().CanBeSeen = false;
            }
            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //Check to see if the enemy can be seen
            foreach (GameObject friendly in p1Chars)
            {
                foreach (GameObject enemy in p2Chars)
                {
                    whatDidIHit = friendly.GetComponent<Character>().characterLineOfSight(enemy.transform.position);
                    //Debug.Log(whatDidIHit.name);
                    //Debug.Log(enemy.name);
                    if (whatDidIHit != null)
                    {
                        if (enemy.name == whatDidIHit.name)
                        {
                            if (!enemy.GetComponent<Character>().CanBeSeen)
                            {
                                enemy.GetComponent<Character>().CanBeSeen = true;
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                            //Update EnemySeen list if the target is already visable to the player
                            else
                            {
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Character>().turnOnGameObject();
                        enemy.GetComponent<Character>().CanBeSeen = true;
                        List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                        tempList.Add(enemy.GetComponent<Character>());
                        friendly.GetComponent<Character>().EnemySeen = tempList;
                    }
                }
            }

            //Turn on/off characters if they can be seen
            foreach (GameObject enemy in p2Chars)
            {
                if (enemy.GetComponent<Character>().CanBeSeen || whatDidIHit == null)
                {
                    enemy.GetComponent<Character>().turnOnGameObject();
                }
                else
                {
                    enemy.GetComponent<Character>().turnOffGameObject();
                }
            }
        }
        else
        {
            //Make sure that the current player's team is active 
            foreach (GameObject friendly in p2Chars)
            {
                friendly.GetComponent<Character>().turnOnGameObject();
            }

            //Each frame reset the enemy can be seen boolean to false
            foreach (GameObject enemy in p1Chars)
            {
                enemy.GetComponent<Character>().CanBeSeen = false;
            }
            //Check to see if the enemy can be seen
            foreach (GameObject friendly in p2Chars)
            {
                foreach (GameObject enemy in p1Chars)
                {
                    whatDidIHit = friendly.GetComponent<Character>().characterLineOfSight(enemy.transform.position);
                    if (whatDidIHit != null)
                    {
                        if (enemy.name == whatDidIHit.name)
                        {
                            if (!enemy.GetComponent<Character>().CanBeSeen)
                            {
                                enemy.GetComponent<Character>().CanBeSeen = true;
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                            //Update EnemySeen list if the target is already visable to the player
                            else
                            {
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Character>().turnOnGameObject();
                        enemy.GetComponent<Character>().CanBeSeen = true;
                        List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                        tempList.Add(enemy.GetComponent<Character>());
                        friendly.GetComponent<Character>().EnemySeen = tempList;
                    }
                }
            }

            //Turn on/off characters if they can be seen
            foreach (GameObject enemy in p1Chars)
            {
                if (enemy.GetComponent<Character>().CanBeSeen || whatDidIHit == null)
                {
                    enemy.GetComponent<Character>().turnOnGameObject();
                }
                else
                {
                    enemy.GetComponent<Character>().turnOffGameObject();
                }
            }

        }
    }

    public void winGame()
    {
        if (!gameOver)
        {
            if (p1Chars.Count == 0)
            {
                Constants.WinScreen.C_WinText = "Player 2 Wins!!";
                gameOver = true;
                SceneManager.LoadScene("Win Scene");
            }
            else if (p2Chars.Count == 0)
            {
                Constants.WinScreen.C_WinText = "Player 1 Wins!!";
                gameOver = true;
                SceneManager.LoadScene("Win Scene");
            }
        }
        gameOver = false;
    }

    public void toggleAttackMode(){
		attackMode = !attackMode;
		currentlySelectedCharacter.GetComponent<Character>().toggleAttackMode ();
	}

    public void setGuardDog()
    {
        currentlySelectedCharacter.GetComponent<Character>().IsOnOverwatch = true;
        currentlySelectedCharacter.GetComponent<Character>().useMove();
        currentlySelectedCharacter.GetComponent<Character>().useMove();
        currentlySelectedCharacter.GetComponent<Character>().UnselectCharacter();
    }

    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start() {
        lineOfSight();
        StartP1Turn ();
    }

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //lineOfSight();
        //winGame();
        if (currentlySelectedCharacter && Input.GetMouseButtonDown(1)) {
            UnselectCharacter();
        }
    }
    #endregion
}
