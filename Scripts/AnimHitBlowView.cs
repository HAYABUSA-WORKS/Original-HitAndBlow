using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimHitBlowView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtHitCount;
    [SerializeField] RectTransform txtHitRect;
    [SerializeField] TextMeshProUGUI txtBlowCount;
    [SerializeField] RectTransform txtBlowRect;
    [SerializeField] CanvasGroup canvasGroup;

    public async UniTask AnimHitBlow((int, int) hitBlowCounts)
    {
        var token = this.GetCancellationTokenOnDestroy();

        txtHitCount.text = hitBlowCounts.Item1.ToString();
        txtBlowCount.text = hitBlowCounts.Item2.ToString();

        var txtHitCountRect = txtHitCount.GetComponent<RectTransform>();
        var txtBlowCountRect = txtBlowCount.GetComponent<RectTransform>();

        // 初期位置をキャッシュ
        var preTxtHitRect = txtHitRect;
        var preTxtBlowRect = txtBlowRect;

        gameObject.SetActive(true);

        // Hit数アニメ
        Sequence sequenceHitCount = DOTween.Sequence();
        _ = sequenceHitCount
            .Append(txtHitCountRect.DOScale(new Vector3(4, 4, 1), 0.1f).SetEase(Ease.InCirc))
            .Append(txtHitCountRect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce))
            .Join(txtHitRect.DOMoveX(800, 0f))
            .Join(txtHitRect.DOMoveX(preTxtHitRect.position.x, 0.2f))
            .ToUniTask();

        await UniTask.WaitForSeconds(0.3f);

        // Blow数アニメ
        Sequence sequenceBlowCount = DOTween.Sequence();
        _ = sequenceBlowCount
        //    .SetDelay(0.3f)
            .Append(txtBlowCountRect.DOScale(new Vector3(4, 4, 1), 0.1f).SetEase(Ease.InCirc))
            .Append(txtBlowCountRect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce))
            .Join(txtBlowRect.DOMoveX(800, 0f))
            .Join(txtBlowRect.DOMoveX(preTxtBlowRect.position.x, 0.2f));

        await UniTask.WaitForSeconds(1.0f);

        gameObject.SetActive(false);

        await UniTask.WaitForSeconds(0.3f);

    }
}
