using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    private static Camera _camera;

    public static Camera Cam()
    {
        if (_camera == null) _camera = Camera.main;
        return _camera;
    }

    public static String TimeToString(this float time, bool precise = false)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        int fraction = (int)(time * 1000f) % 1000;
        if (precise) return String.Format("{0:0}:{1:00},{2:000}", minutes, seconds, fraction);
        return String.Format("{0:0}", seconds);
    }

    public static void LockCursor(bool lockcursor = true)
    {
        if (lockcursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

    }

    public static void EnableInput(bool enable = true)
    {
        if (enable)
            PlayerInput.GetPlayerByIndex(0).actions.Enable();
        else
            PlayerInput.GetPlayerByIndex(0).actions.Disable();
    }
}
