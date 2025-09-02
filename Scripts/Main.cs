using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main
{
    List<int> correctAnswerList = new List<int>();
    int correctAnswerListLength = 4;

    /// <summary>
    /// ������4�P�^�̐�����ݒ肷��i�d���Ȃ��j
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
    /// AnswerStack��correctAnswerList���r����Hit����Blow����Ԃ�
    /// </summary>
    /// <param name="answerStack"></param>
    /// <returns></returns>
    public (int, int) CheckAnswer(List<int> answerList)
    {
        int hitCount = 0;
        int blowCount = 0;

        // correctAnswerList�̃R�s�[���쐬
        List<int> correctAnswerList = this.correctAnswerList;
        
        // Hit�������̈ꎞ�i�[�p
        List<int> hitList = new List<int>();

        Debug.Log("AnswerList:" + answerList[0] + answerList[1] + answerList[2] + answerList[3]);

        // Hit���`�F�b�N
        for (int i = 0; i < correctAnswerListLength; i++) 
        {
            if (correctAnswerList[i] == answerList[i])
            {
                hitCount++;
                hitList.Add(correctAnswerList[i]);
            }
        }

        // correctAnswerList��answerList����Hit����������菜��
        var correctAnswerBlow = correctAnswerList.Except(hitList);
        var answerBlow =  answerList.Except(hitList);

        // FindAll�p��List�ɃL���X�g
        var correctAnswerBlowList = correctAnswerBlow.ToList();

        // Blow���`�F�b�N�@���@Hit���Ȃ����������m���ׂċ��ʂ�����΂����Blow
        List<int> blowList = correctAnswerBlowList.FindAll(answerBlow.Contains);
        blowCount = blowList.Count;

        return (hitCount, blowCount);
    }
}
