using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousAnswerField : MonoBehaviour
{
    [SerializeField] PreviousAnswerSheet previousAnswerSheetPrefab;
    [SerializeField] Transform previousAnswerSet;

    /// <summary>
    /// previousAnswerSheetÇê∂ê¨Ç∑ÇÈ
    /// </summary>
    /// <param name="roundCount"></param>
    /// <param name="hitBlowCounts"></param>
    /// <param name="answerList"></param>
    public PreviousAnswerSheet CreatePreviousAnswerSheet(int roundCount, (int, int) hitBlowCounts, List<int> answerList)
    {
        var sheet = Instantiate(previousAnswerSheetPrefab, previousAnswerSet, false);

        sheet.SetPreviousAnswerNum(roundCount, hitBlowCounts, answerList);

        return sheet;
    }
}
