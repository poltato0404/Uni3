using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    public void openKeypad(){
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
