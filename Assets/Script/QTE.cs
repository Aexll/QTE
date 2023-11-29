using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class QTE : MonoBehaviour
{

    int len = 1;


    public int score = 0;
    int Score {
        get
        {
            return score;
        }
        set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
            OnScoreChangedString?.Invoke(score.ToString());
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        possiblesQTE = new string[][]{
            new string[]{ "a", "z","e","r" } ,
            new string[]{ "z", "z","z","z" } ,
            new string[]{ "a", "e","a","z" } ,
        };
        baseQTE = new List<string>();
        currentQTE = new List<string>();
        fillqte();
        Score = 0;

    }

    string lettres = "abcdefghijklnopqrstuvwxyz";
    //public string lettres = "awsd";

    public void fillqte()
    {
        baseQTE.Clear();
        for (int i = 0; i < len; i++)
        {
            baseQTE.Add(lettres[Random.Range(0, lettres.Length)].ToString());
        }
        /*        foreach (var item in possiblesQTE[Random.Range(0, possiblesQTE.Length)])
                {
                    baseQTE.Add(item);
                }*/
        //baseQTE.CopyTo(possiblesQTE[Random.Range(0, possiblesQTE.Length)]);
        //print(baseQTE[0]);
        resetdefaultQTE();
        localMin = currentQTE.Count;
    }

    public void resetdefaultQTE()
    {
        currentQTE.Clear();
        foreach (var item in baseQTE)
        {
            currentQTE.Add(item);
        }
    }


    List<string> currentQTE;
    List<string> baseQTE;
    string[][] possiblesQTE;

    bool fueldrain = false;
    float fuel = 5f;
    float maxfuel = 5f;
    public void addfuel(float ammount)
    {
        fuel += ammount;
        maxfuel = Mathf.Max(maxfuel, fuel);
    }


    public UnityEvent OnWonQTE;
    public UnityEvent OnFailedQTE;
    public UnityEvent OnGameEndQTE;
    public UnityEvent<string> stateQTE;
    public UnityEvent<float> onfueltick;
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<string> OnScoreChangedString;


    public string listtostring(List<string> l)
    {
        string r = "";
        foreach (string s in l)
        {
            r += s + ' ';
        }
        return r;
    }

    int localMin = 0;


    // Update is called once per frame
    void Update()
    {
        if (fueldrain)
        {
            fuel -= Time.deltaTime;
            onfueltick?.Invoke(Mathf.Clamp(fuel/maxfuel,0,1));
            if (fuel <= 0)
            {
                OnGameEndQTE?.Invoke();
                Destroy(gameObject);
            }
        }
        if (currentQTE != null)
        {
            //Debug.Log(currentQTE[0]);
            stateQTE?.Invoke(listtostring(currentQTE));
            if(currentQTE.Count != 0)
            {
                if (Input.GetKeyDown(currentQTE[0]))
                {
                    fueldrain = true;
                    currentQTE.RemoveAt(0);
                    if (currentQTE.Count < localMin)
                    {
                        Score++;
                        localMin = currentQTE.Count;
                    }
                    if (currentQTE.Count == 0 )
                    {
                        OnWonQTE?.Invoke();
                        fillqte();
                        //Score += len;
                        addfuel(5);
                        len += 1;
                    }
                }else if (Input.anyKeyDown)
                {
                    OnFailedQTE?.Invoke();
                    resetdefaultQTE();
                }
            }
        }
    }
}
