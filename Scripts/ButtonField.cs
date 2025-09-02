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
    /// �i���o�[�{�^���̐������e�{�^����interactable�֌W�̃��\�b�h�̓o�^
    /// </summary>
    public void InitializedButton()
    {
        CreateNumButton();

        btnCancel.onClick.AddListener(PushCancel);
        btnDecision.onClick.AddListener(PushDecision);
        btnGiveUp.onClick.AddListener(PushGiveUp);

        // �i���o�[�{�^����interactable��؂�ւ��郁�\�b�h��o�^
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

        // ��ɏo�Ă��Ȃ����̃i���o�[�{�^����interactable��true�ɂ��郁�\�b�h��o�^
        answerField.interactableSelectedNumButtonsEvent.AddListener((intList) =>
        {
            for (int i = 0; i < intList.Count; i++)
            {
                numButtonList[intList[i]].GetComponent<Button>().interactable = true;
            }
        });

        // �L�����Z���{�^����interactable��true�ɂ��郁�\�b�h��o�^
        answerField.interactableCancelButtonsEvent.AddListener(() => btnCancel.interactable = true);

        // ����{�^����interactable��؂�ւ��郁�\�b�h��o�^
        answerField.interactableDecisionButtonsEvent.AddListener(pushable => btnDecision.interactable = pushable);

        
    }

    /// <summary>
    /// �𓚗p��0�`9�̃i���o�[�{�^���𐶐�����
    /// </summary>
    void CreateNumButton()
    {
        for(int i = 0; i < 10; i++)
        {
            // ���[�v�J�E���^i����x�ʂ̕ϐ��Ɋi�[���������������Ȃ��ƈ����A����onClick.AddListener�����܂��@�\���Ȃ�
            int j = i;

            NumButton numButton = Instantiate(numButtonPrefab, numButtonField, false);

            numButton.SetButtonNum(j);

            // AnswerField��CreateAnswerCard���\�b�h���{�^���ɓo�^
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
        Debug.Log("�����Ƃ������I");

        checkAnswerEvent.Invoke(answerField.answerStack);
        ResetButtons();
    }

    void PushGiveUp()
    {
        checkGiveUpPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// �{�^���̏�Ԃ��ŏ��ɖ߂�
    /// </summary>
    public void ResetButtons()
    {
        btnCancel.interactable = false;
        btnDecision.interactable = false;
        btnGiveUp.interactable = true;

        // �i���o�[�{�^����S�ĉ�����悤�ɂ���
        foreach (NumButton numButton in numButtonList)
        {
            numButton.GetComponent<Button>().interactable = true;
        }

        answerField.ResetLists();
    }
}
