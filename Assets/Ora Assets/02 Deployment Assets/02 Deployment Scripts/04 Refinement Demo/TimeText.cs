using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour {

    private int _tm = 0;
    private int _m = 0;
    private int _ts = 0;
    private int _s = 0;
    void Update ()
    {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        if (Time.time % 10 == 0)
        {
            _s++;
        }
        if (_s % 10 == 0)
        {
            _s = 0;
            _ts++;
        }
        if (_ts % 6 == 0)
        {
            _ts = 0;
            _m++;
        }
        if (Time.time % 60 == 0)
        {
            _m++;
        }
        else if (Time.time > 9)
        {
            
        }
        textmeshPro.SetText("0{0}:{1}{2}", _tm, _ts, _s);
    }
}
