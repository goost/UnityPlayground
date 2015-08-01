using System;
using UnityEngine;

//Simple Wrapper around Log to disable Log Output.
//NOTE(goost) 100% Unity has built-in support for this, need to look it up
public class Logger: MonoBehaviour
{
    private const bool _log = false;

    public static void Log(Action<string> log, string msg)
    {
        if (_log)
        {
            log(msg);
        }
    }
}
