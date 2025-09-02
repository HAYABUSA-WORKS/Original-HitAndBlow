using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AnswerField : MonoBehaviour
{
    [SerializeField] AnswerNum answerNumPrefab;

    public UnityEvent<bool, int> interactableNumButtonsEvent;
    public UnityEvent<List<int>> interactableSelectedNumButtonsEvent;  // trueにするのにしか使わない
    public UnityEvent interactableCancelButtonsEvent;                  // trueにするのにしか使わない
    public UnityEvent<bool> interactableDecisionButtonsEvent;

    // AnswerCardの表示制御用
    Stack<AnswerNum> answerNumStack = new Stack<AnswerNum>();
    // correctAnswerとの比較用
    public Stack<int> answerStack { get; private set; } = new Stack<int>();
    // AnswerCardを4枚生成後にキャンセルした時にどのボタンが押されていたか記憶用
    List<int> removeNumList = new List<int>();

    int answerNumLength = 4;

    /// <summary>
    /// AnswerCardを場に生成する
    /// </summary>
    /// <param name="num"></param>
    public void CreateAnswerCard(int num)
    {
        // 押したナンバーボタンを押せなくする
        interactableNumButtonsEvent.Invoke(false, num);
        interactableCancelButtonsEvent.Invoke();

        AnswerNum answer = Instantiate(answerNumPrefab, transform, false);
        answer.SetAnswerNum(num);

        answerNumStack.Push(answer);
        answerStack.Push(num);

        // AnswerCardを4枚生成したら全てのボタンを押せなくするEventを発行
        if(answerStack.Count >= answerNumLength)
        {
            int[] answerArray = answerStack.ToArray();
            removeNumList.Clear();
            var tempList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //string temp = "";
            //foreach (int i in tempList) temp += i;
            //Debug.Log("removeNumList1:" + temp);

            // 0～9のリストから現在押されているナンバーボタンの数を取り除く
            var newTempList = tempList.Except(answerArray);
            removeNumList = newTempList.ToList();

            //Debug.Log("answerArray:" + answerArray[0] + answerArray[1] + answerArray[2] + answerArray[3]);

            //string ss = "";
            //foreach (int i in removeNumList) ss += i;
            //Debug.Log("removeNumList2:" + ss);

    ;       // 11は全てのボタンのinteractableを操作する
            interactableNumButtonsEvent.Invoke(false, 11);

            // 決定ボタンを押せるようにする
            CanDecideAnswer(true);
        }
    }

    /// <summary>
    /// 戻るボタンを押した時の実装
    /// </summary>
    /// <returns></returns>
    public bool RemoveAnswerCard()
    {
        // 決定ボタンを押せなくする
        CanDecideAnswer(false);

        // AnswerCardが4枚生成されている状態の時全てのボタンを押せるようにするEventを発行
        if (answerStack.Count == answerNumLength)
        {
            // 11は全てのボタンのinteractableを操作する
            interactableSelectedNumButtonsEvent.Invoke(removeNumList);
        }

        var answerNum = answerNumStack.Pop();
        Destroy(answerNum.gameObject);

        int removeNum = answerStack.Pop();
        interactableNumButtonsEvent.Invoke(true, removeNum);

        // AnswerCardが場に1枚もなければfalseを返す＝戻すボタンを押せなくする
        if (answerNumStack.Count == 0)
        {
            return false;
        }

        return true;
    }

    void CanDecideAnswer(bool canPush)
    {
        interactableDecisionButtonsEvent.Invoke(canPush);
    }

    /// <summary>
    /// 場のAnswerCard4枚を全て破壊し、各リストをクリアする
    /// </summary>
    public void ResetLists()
    {
        foreach (var answerNum in answerNumStack)
        {
            Destroy(answerNum.gameObject);
        }

        answerNumStack.Clear();
        answerStack.Clear();
        removeNumList.Clear();
    }
}
