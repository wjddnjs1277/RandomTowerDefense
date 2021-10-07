using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    [SerializeField] new Text name;
    [SerializeField] Text value;

    public void Show(string name, string value)
    {
        this.name.text = name;
        this.value.text = value;
    }
}
