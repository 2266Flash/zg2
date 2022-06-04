using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class globalVars : MonoBehaviour
{
    public static PlayerManager p;
    public static string playerName;
    public static int cruelty;
    public static IEnumerator textTyper(AudioSource sound, string text, TextMeshProUGUI uitext)
    {
        for (int i = 0; i < text.Length+1; i++)
        {
            sound.Stop();
            uitext.text = text.Substring(0, i);
            sound.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
