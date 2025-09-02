using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRewardCount;
    [SerializeField] Button btnNextGame;

    public UnityEvent restartGameEvent;

    private void Awake()
    {
        btnNextGame.onClick.AddListener(PushNextGame);
    }

    public void SetResult(int reward)
    {
        txtRewardCount.text = reward.ToString();
    }

    public void PushNextGame()
    {
        restartGameEvent?.Invoke();
    }
}
