using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtButtonNum;

    /// <summary>
    /// ナンバーボタンに数字を割り当てる
    /// </summary>
    /// <param name="num">割り当てる数字</param>
    public void SetButtonNum(int num)
    {
        txtButtonNum.text = num.ToString();
    }
}
