using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtButtonNum;

    /// <summary>
    /// �i���o�[�{�^���ɐ��������蓖�Ă�
    /// </summary>
    /// <param name="num">���蓖�Ă鐔��</param>
    public void SetButtonNum(int num)
    {
        txtButtonNum.text = num.ToString();
    }
}
