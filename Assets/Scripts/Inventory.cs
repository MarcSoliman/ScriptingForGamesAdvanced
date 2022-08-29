using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _treasureUI;
  
  private int _treasure = 0;
  public int Treasure
  {
    get => _treasure;
    set
    {
      _treasure = value;
      _treasureUI.text = "Treasure - " + _treasure.ToString();
    }
  }
}
