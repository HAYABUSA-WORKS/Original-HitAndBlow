using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonField : MonoBehaviour
{
    [SerializeField] NumButton numButtonPrefab;
    [SerializeField] Transform numButtonField;

    [SerializeField] AnswerField answerField;

    [SerializeField] Button btnCancel;
    [SerializeField] Button btnDecision;
    [SerializeField] Button btnGiveUp;

    public CheckGiveUpPanel checkGiveUpPanel;

    List<NumButton> numButtonList = new List<NumButton>();

    public UnityEvent<Stack<int>> checkAnswerEvent;

    /// <summary>
    /// ナンバーボタンの生成＆各ボタンのinteractable関係のメソッドの登録
    /// </summary>
    public void InitializedButton()
    {
        CreateNumButton();

        btnCancel.onClick.AddListener(PushCancel);
        btnDecision.onClick.AddListener(PushDecision);
        btnGiveUp.onClick.AddListener(PushGiveUp);

        // ナンバーボタンのinteractableを切り替えるメソッドを登録
        answerField.interactableNumButtonsEvent.AddListener((pushable, num) =>
        {
            if(num == 11)
            {
                foreach (NumButton numButton in numButtonList)
                {
                    numButton.GetComponent<Button>().interactable = pushable;
                }

                return;
            }

            numButtonList[num].GetComponent<Button>().interactable = pushable;
        });

        // 場に出ていない数のナンバーボタンのinteractableをtrueにするメソッドを登録
        answerField.interactableSelectedNumButtonsEvent.AddListener((intList) =>
        {
            for (int i = 0; i < intList.Count; i++)
            {
                numButtonList[intList[i]].GetComponent<Button>().interactable = true;
            }
        });

        // キャンセルボタンのinteractableをtrueにするメソッドを登録
        answerField.interactableCancelButtonsEvent.AddListener(() => btnCancel.interactable = true);

        // 決定ボタンのinteractableを切り替えるメソッドを登録
        answerField.interactableDecisionButtonsEvent.AddListener(pushable => btnDecision.interactable = pushable);

        
    }

    /// <summary>
    /// 解答用の0～9のナンバーボタンを生成する
    /// </summary>
    void CreateNumButton()
    {
        for(int i = 0; i < 10; i++)
        {
            // ループカウンタiを一度別の変数に格納し直す→こうしないと引数アリのonClick.AddListenerがうまく機能しない
            int j = i;

            NumButton numButton = Instantiate(numButtonPrefab, numButtonField, false);

            numButton.SetButtonNum(j);

            // AnswerFieldのCreateAnswerCardメソッドをボタンに登録
            numButton.GetComponent<Button>().onClick.AddListener(() => answerField.CreateAnswerCard(j));

            numButtonList.Add(numButton);
        }
    }

    void PushCancel()
    {
        btnCancel.interactable = answerField.RemoveAnswerCard();
    }

    void PushDecision()
    {
        Debug.Log("かいとうした！");

        checkAnswerEvent.Invoke(answerField.answerStack);
        ResetButtons();
    }

    void PushGiveUp()
    {
        checkGiveUpPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// ボタンの状態を最初に戻す
    /// </summary>
    public void ResetButtons()
    {
        btnCancel.interactable = false;
        btnDecision.interactable = false;
        btnGiveUp.interactable = true;

        // ナンバーボタンを全て押せるようにする
        foreach (NumButton numButton in numButtonList)
        {
            numButton.GetComponent<Button>().interactable = true;
        }

        answerField.ResetLists();
    }
}
