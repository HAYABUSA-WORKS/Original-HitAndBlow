using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main
{
    List<int> correctAnswerList = new List<int>();
    int correctAnswerListLength = 4;

    /// <summary>
    /// 正解の4ケタの数字を設定する（重複なし）
    /// </summary>
    public void SetCorrectAnswer()
    {
        correctAnswerList.Clear();
        List<int> answerPool = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        for (int i = 0; i < correctAnswerListLength; i++)
        {
            int rand = Random.Range(0, 10 - i);

            correctAnswerList.Add(answerPool[rand]);
            answerPool.RemoveAt(rand);
        }

        Debug.Log("Answer:" + correctAnswerList[0] + correctAnswerList[1] + correctAnswerList[2] + correctAnswerList[3]);
    }

    /// <summary>
    /// AnswerStackとcorrectAnswerListを比較してHit数とBlow数を返す
    /// </summary>
    /// <param name="answerStack"></param>
    /// <returns></returns>
    public (int, int) CheckAnswer(List<int> answerList)
    {
        int hitCount = 0;
        int blowCount = 0;

        // correctAnswerListのコピーを作成
        List<int> correctAnswerList = this.correctAnswerList;
        
        // Hitした数の一時格納用
        List<int> hitList = new List<int>();

        Debug.Log("AnswerList:" + answerList[0] + answerList[1] + answerList[2] + answerList[3]);

        // Hitをチェック
        for (int i = 0; i < correctAnswerListLength; i++) 
        {
            if (correctAnswerList[i] == answerList[i])
            {
                hitCount++;
                hitList.Add(correctAnswerList[i]);
            }
        }

        // correctAnswerListとanswerListからHitした数を取り除く
        var correctAnswerBlow = correctAnswerList.Except(hitList);
        var answerBlow =  answerList.Except(hitList);

        // FindAll用にListにキャスト
        var correctAnswerBlowList = correctAnswerBlow.ToList();

        // Blowをチェック　→　Hitしなかった数同士を比べて共通があればそれはBlow
        List<int> blowList = correctAnswerBlowList.FindAll(answerBlow.Contains);
        blowCount = blowList.Count;

        return (hitCount, blowCount);
    }
}
