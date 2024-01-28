using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TextScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    [SerializeField] private int lettersPerSecond;
    // Start is called before the first frame update
    
    public void Write()
    {
        StartCoroutine(WriteText());
    }

    public IEnumerator WriteText()
    {
        string wholeText = text.text;
        print(wholeText);
        text.text = "";
        foreach (var letter in wholeText.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);

        }
        
    }
}
