﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    private Object playerPrefab;
    PlayerControl player1, player2, player3, player4;
    List<GameObject> players;


    // Use this for initialization
    void Start () {
        playerPrefab = Resources.Load("Prefabs/Player");
        players = new List<GameObject>();

        int joysticks = Input.GetJoystickNames().Length;

        if (joysticks == 0)
        {
            GameObject go = Instantiate(playerPrefab, new Vector3(2, 10, 2), Quaternion.identity) as GameObject;
            go.name = "Player 1";
            go.GetComponent<PlayerControl>().index = 1;
            players.Add(go);
        } else
        {
            for (int i = 1; i <= joysticks; i++)
            {
                GameObject go = Instantiate(playerPrefab, new Vector3(2 * i, 10, 2 * i), Quaternion.identity) as GameObject;
                go.name = "Player " + i;
                go.GetComponent<PlayerControl>().index = i;
                players.Add(go);
            }
        }

        AdjustViewports(joysticks);
        StartCoroutine(AreYouAlive());
	}

    void AdjustViewports(int numPlayers)
    {
        switch (numPlayers)
        {
            case 0:
            case 1:
                players[0].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 1);
                break;
            case 2:
                players[0].GetComponentInChildren<Camera>().rect = new Rect(0, .5f, 1, .5f);
                players[1].GetComponentInChildren<Camera>().rect = new Rect(0, 0f, 1, .5f);
                break;
            case 3:
                players[0].GetComponentInChildren<Camera>().rect = new Rect(0, .5f, 1, .5f);
                players[1].GetComponentInChildren<Camera>().rect = new Rect(0, 0f, .5f, .5f);
                players[2].GetComponentInChildren<Camera>().rect = new Rect(.5f, 0f, .5f, .5f);
                break;
            case 4:
                players[0].GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, .5f);
                players[1].GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, .5f, .5f);
                players[2].GetComponentInChildren<Camera>().rect = new Rect(0f, 0f, .5f, .5f);
                players[3].GetComponentInChildren<Camera>().rect = new Rect(.5f, 0f, .5f, .5f);
                break;
        }
    }

    IEnumerator AreYouAlive()
    {
        while(true)
        {
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 75)
                {
                    player.GetComponent<PlayerControl>().Reset();
                }
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
    }

}
