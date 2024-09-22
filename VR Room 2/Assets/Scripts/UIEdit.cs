using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEdit : MonoBehaviour
{
    [SerializeField] List<Graphic> theUis;
    [SerializeField] Vector3 scaleChangeAmonut;
    [SerializeField] List<Graphic> theUisColorOnly;
     
    void IncreaseScaleValue(Vector3 theScale)
    {    
        foreach (Graphic theUI in theUis)
        {
            RectTransform theRectTransform = theUI.GetComponent<RectTransform>();

            theRectTransform.localScale = theRectTransform.localScale + theScale;
        }
    }

    public void ChangeAmount(Vector3 theScale)
    {
        scaleChangeAmonut = theScale;
    }

    public void IncreaseScale()
    {
        IncreaseScaleValue(scaleChangeAmonut);
    }

    public void DecreaseScale()
    {
        IncreaseScaleValue(-scaleChangeAmonut);
    }

    public void ChangeColor()
    {
        foreach (Graphic theUI in theUis)
        {
            theUI.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        foreach (Graphic theUI in theUisColorOnly)
        {
            theUI.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }



}
