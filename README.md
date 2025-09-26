# Original-HitAndBlow
## ○ 制作時期
2024年3月～6月

## ○ 使用技術
Unity(2022.3.24f1)　C#　UniTask　DOTween

## ○ 概要
664 lines　プログラムコードは全て自作（AI・コピペ未使用）  
ヒットアンドブローの判定アルゴリズムを考えたみたいと思いゲームを制作した。  
ヒットアンドブローとは、0～9までの数で構成された4桁の数字（今回は重複なし）がなんのかを当てるゲーム。解答として4桁の数字を提示するとヒット数とブロー数を教えてもらえるので、それをヒントに正解を絞り込んでいく。ヒットとは数と位置のどちらも正解している個数。ブローとは数は合っているが位置が違う個数。

## ○ 工夫、頑張った点
+ 正解の4桁の数字を重複なしで生成するアルゴリズム
+ ヒット数とブロー数を判定するアルゴリズム
+ 正解入力ボタンまわりの挙動（一度押したボタンは押せない、4桁が決まったら全部押せない、戻るボタンで押した逆順でボタンを押せるように戻す）

## ○ スクリーンショット
<img width="473" height="278" alt="image" src="https://github.com/user-attachments/assets/a3ad730c-2dd8-4290-adbf-d688b6eebdac" />
<img width="473" height="278" alt="image" src="https://github.com/user-attachments/assets/342baacf-7bdc-438d-a14d-4d316478ca98" />
<img width="475" height="278" alt="image" src="https://github.com/user-attachments/assets/fee6bed2-7bd0-41f5-8d38-18ff9dfa2783" />
