using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ButtonField buttonField;
    [SerializeField] PreviousAnswerField previousAnswerField;
    [SerializeField] TopPanelManager topPanelManager;
    [SerializeField] ResultPanel resultPanel;

    [SerializeField] AnimHitBlowView animHitBlowView;
 
    Main main = new Main();

    int roundCount = 1;
    int ringCount = 500;
    int[] rewards = { 0, 2000, 500, 200, 100, 50, 30, 20, 10 };

    List<PreviousAnswerSheet> previousAnswerSheetList = new List<PreviousAnswerSheet>();

    void Start()
    {
        InitializedGame();
        buttonField.InitializedButton();

        buttonField.checkAnswerEvent.AddListener(answerStack => CheckAnswerCommand(answerStack));

        resultPanel.restartGameEvent.AddListener(InitializedGame);
        buttonField.checkGiveUpPanel.giveUpEvent.AddListener(InitializedGame);
    }

    async void CheckAnswerCommand(Stack<int> answerStack)
    {
        ringCount -= 10;
        topPanelManager.ChangeRing(ringCount);

        // StackをListにキャストして逆順に並び変える
        List<int> answerList = answerStack.ToList();
        answerList.Reverse();

        (int, int) hitBlowCounts = main.CheckAnswer(answerList);

        Debug.Log($"Hit:{hitBlowCounts.Item1} Blow:{hitBlowCounts.Item2}");

        // TODO アニメーション　Hit数とBlow数の表示
        await animHitBlowView.AnimHitBlow(hitBlowCounts);

        if(hitBlowCounts.Item1 == 4)
        {
            Debug.Log("せいかい！！おめでとう！！");

            DisplayResult();

            return;
        }

        // 「今までの解答欄」の中身を作成　→　リスタート時にobjectを破壊するため
        var sheet =  previousAnswerField.CreatePreviousAnswerSheet(roundCount, hitBlowCounts, answerList);
        previousAnswerSheetList.Add(sheet);

        roundCount++;

        topPanelManager.ChangeRound(roundCount);
    }

    void DisplayResult()
    {
        // 正解にかかったRound数からRewardを決定
        int reward = rewards[roundCount];

        resultPanel.gameObject.SetActive(true);
        resultPanel.SetResult(reward);

        ringCount += reward;
    }

    void InitializedGame()
    {
        main.SetCorrectAnswer();

        roundCount = 1;
        topPanelManager.ChangeRound(roundCount);
        topPanelManager.ChangeRing(ringCount);

        resultPanel.gameObject.SetActive(false);
        buttonField.ResetButtons();

        if(previousAnswerSheetList.Count > 0)
        {
            foreach (var sheet in previousAnswerSheetList)
            {
                Destroy(sheet.gameObject);
            }
        }
    }

}
