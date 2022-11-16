using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
[SerializeField] private Text moneyText;
 public static MoneyText instance {get; private set;}

private void Awake() 
{
    instance = this;
    
}



public void TextChange(string _Text)
{
    moneyText.text = _Text;
    
}









}

