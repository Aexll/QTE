using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct PlayerInfo
{
    public string name;
    public int score;

    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

}

[CreateAssetMenu(fileName = "Scoreboard_", menuName = "ScriptableObjects/Scoreboard", order = 1)]
public class SO_Scorelist : ScriptableObject
{

    public List<PlayerInfo> players;

    public UnityEvent OnNewEntry;

    public void addScore(string n,int s)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].name == n )
            {
                if( players[i].score > s)
                {
                    var ms = players[i];
                    ms.score = s;
                    players[i] = ms;
                }
                return;
            }
        }
        players.Add(new PlayerInfo(n, s));
    }

    public void Sort()
    {
        players.Sort((x, y) => y.score.CompareTo(x.score));

    }

    public string GetVisual()
    {
        Sort();
        string r = "";
        foreach (var item in players)
        {
            r += item.name + " " + item.score + "\n";
        }
        return r;
    }


}
