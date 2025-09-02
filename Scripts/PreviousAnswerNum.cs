using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviousAnswerNum : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtPreviousAnswerNum;

    public void SetPreviousAnswerNum(int num)
    {
        txtPreviousAnswerNum.text = num.ToString();
    }
}
