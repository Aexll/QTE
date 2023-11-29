using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class textshower : MonoBehaviour
{


    public UnityEvent<string> onTextConverted;
    public void showconvertedstring(string str)
    {
        string r = "";
        foreach(char c in str)
        {
            switch (c)
            {
                case 'a':
                    r += 'q';
                    break;
                case 'q':
                    r += 'a';
                    break;
                case 'z':
                    r += 'w';
                    break;
                case 'w':
                    r += 'z';
                    break;
                case 'm':
                    r += ',';
                    break;
                case ',':
                    r += 'm';
                    break;
                default:
                    r += c;
                    break;
            }
        }
        onTextConverted?.Invoke(r);
    }
}
