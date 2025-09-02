using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckGiveUpPanel : MonoBehaviour
{
    [SerializeField] Button btnYes;
    [SerializeField] Button btnNo;

    public UnityEvent giveUpEvent;

    private void Awake()
    {
        btnYes.onClick.AddListener(PushYes);
        btnNo.onClick.AddListener(() => gameObject.SetActive(false));
    }

    void PushYes()
    {
        Debug.Log("�Ƃ��イ�ł�����߂��I");
        giveUpEvent.Invoke();
        gameObject.SetActive(false);
    }
}
