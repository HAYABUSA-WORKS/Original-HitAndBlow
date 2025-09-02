using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AnswerField : MonoBehaviour
{
    [SerializeField] AnswerNum answerNumPrefab;

    public UnityEvent<bool, int> interactableNumButtonsEvent;
    public UnityEvent<List<int>> interactableSelectedNumButtonsEvent;  // true�ɂ���̂ɂ����g��Ȃ�
    public UnityEvent interactableCancelButtonsEvent;                  // true�ɂ���̂ɂ����g��Ȃ�
    public UnityEvent<bool> interactableDecisionButtonsEvent;

    // AnswerCard�̕\������p
    Stack<AnswerNum> answerNumStack = new Stack<AnswerNum>();
    // correctAnswer�Ƃ̔�r�p
    public Stack<int> answerStack { get; private set; } = new Stack<int>();
    // AnswerCard��4��������ɃL�����Z���������ɂǂ̃{�^����������Ă������L���p
    List<int> removeNumList = new List<int>();

    int answerNumLength = 4;

    /// <summary>
    /// AnswerCard����ɐ�������
    /// </summary>
    /// <param name="num"></param>
    public void CreateAnswerCard(int num)
    {
        // �������i���o�[�{�^���������Ȃ�����
        interactableNumButtonsEvent.Invoke(false, num);
        interactableCancelButtonsEvent.Invoke();

        AnswerNum answer = Instantiate(answerNumPrefab, transform, false);
        answer.SetAnswerNum(num);

        answerNumStack.Push(answer);
        answerStack.Push(num);

        // AnswerCard��4������������S�Ẵ{�^���������Ȃ�����Event�𔭍s
        if(answerStack.Count >= answerNumLength)
        {
            int[] answerArray = answerStack.ToArray();
            removeNumList.Clear();
            var tempList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //string temp = "";
            //foreach (int i in tempList) temp += i;
            //Debug.Log("removeNumList1:" + temp);

            // 0�`9�̃��X�g���猻�݉�����Ă���i���o�[�{�^���̐�����菜��
            var newTempList = tempList.Except(answerArray);
            removeNumList = newTempList.ToList();

            //Debug.Log("answerArray:" + answerArray[0] + answerArray[1] + answerArray[2] + answerArray[3]);

            //string ss = "";
            //foreach (int i in removeNumList) ss += i;
            //Debug.Log("removeNumList2:" + ss);

    ;       // 11�͑S�Ẵ{�^����interactable�𑀍삷��
            interactableNumButtonsEvent.Invoke(false, 11);

            // ����{�^����������悤�ɂ���
            CanDecideAnswer(true);
        }
    }

    /// <summary>
    /// �߂�{�^�������������̎���
    /// </summary>
    /// <returns></returns>
    public bool RemoveAnswerCard()
    {
        // ����{�^���������Ȃ�����
        CanDecideAnswer(false);

        // AnswerCard��4����������Ă����Ԃ̎��S�Ẵ{�^����������悤�ɂ���Event�𔭍s
        if (answerStack.Count == answerNumLength)
        {
            // 11�͑S�Ẵ{�^����interactable�𑀍삷��
            interactableSelectedNumButtonsEvent.Invoke(removeNumList);
        }

        var answerNum = answerNumStack.Pop();
        Destroy(answerNum.gameObject);

        int removeNum = answerStack.Pop();
        interactableNumButtonsEvent.Invoke(true, removeNum);

        // AnswerCard�����1�����Ȃ����false��Ԃ����߂��{�^���������Ȃ�����
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
    /// ���AnswerCard4����S�Ĕj�󂵁A�e���X�g���N���A����
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
