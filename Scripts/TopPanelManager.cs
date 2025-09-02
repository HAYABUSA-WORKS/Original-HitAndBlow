using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRound;
    [SerializeField] TextMeshProUGUI txtRoundCount;
    [SerializeField] TextMeshProUGUI txtRingCount;


    public void ChangeRound(int roundCount)
    {
        if(roundCount <= 8)
        {
            txtRoundCount.text = roundCount.ToString();
        }
        else
        {
            txtRound.gameObject.SetActive(false);
            txtRoundCount.text = "FinalRound";
        }
    }

    public void ChangeRing(int ringCount)
    {
        txtRingCount.text = ringCount.ToString();
    }
}
