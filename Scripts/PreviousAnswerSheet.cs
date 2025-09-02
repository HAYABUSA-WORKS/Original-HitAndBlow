using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviousAnswerSheet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRound;
    [SerializeField] TextMeshProUGUI txtHitCount;
    [SerializeField] TextMeshProUGUI txtBlowCount;

    [SerializeField] Transform previousAnswer;
    [SerializeField] PreviousAnswerNum previousAnswerNumPrefab;

    /// <summary>
    /// Round��Hit��Blow�����L�q��previousAnswerNum�𐶐�
    /// </summary>
    /// <param name="roundCount"></param>
    /// <param name="hitBlowCounts"></param>
    /// <param name="answerList"></param>
    public void SetPreviousAnswerNum(int roundCount, (int, int) hitBlowCounts, List<int> answerList)
    {
        txtRound.text = roundCount.ToString();
        txtHitCount.text = hitBlowCounts.Item1.ToString();
        txtBlowCount.text = hitBlowCounts.Item2.ToString();

        // PreviousAnswer��4�̐�����\��
        foreach (var num in answerList)
        {
            var answer = Instantiate(previousAnswerNumPrefab, previousAnswer, false);
            answer.SetPreviousAnswerNum(num);
        }
    }
}
