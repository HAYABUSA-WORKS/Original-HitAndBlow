using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerNum : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtAnswerNum;

    public void SetAnswerNum(int num)
    {
        txtAnswerNum.text = num.ToString();
    }
}
