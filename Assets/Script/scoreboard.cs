using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class scoreboard : MonoBehaviour
{

    public SO_Scorelist scorelist;
    public TextMeshProUGUI txt;
    public QTE qteref;


    private void Awake()
    {
        scorelist.OnNewEntry.AddListener(EventNewEntry);
    }


    public void RegisterPlayerScore(string playername)
    {
        scorelist.addScore(playername, qteref.score);
        Debug.Log("newplayer" + playername + " with score : " + qteref.score.ToString());
    }


    public void EventNewEntry()
    {

        txt.text = scorelist.GetVisual();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventNewEntry();
    }


}
