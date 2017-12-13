// 作成者    :菅原 
// 機能      :ポーズウインドウのアニメーション
// 作成日    :2017/12/12 
// 最終更新日:2017/12/12 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //DoTweenを使用する

public class WindowMove : MonoBehaviour {

	[SerializeField] private float speed = 0.0f; //アニメーションの速度 
	[SerializeField] private float downPosition = 0.0f; //落ちる場所のY座標
	private float upPosition; //初期位置

	[SerializeField] private RectTransform rectTransform = null; //ポーズウインドウのRectTransform

	private void Start()
	{
		upPosition = rectTransform.position.y; //初期位置の設定
	}

	public void MoveOn(bool down) //downでウインドウが降りてくる、upでウインドウが下がってくる
	{
		if (down)
		{
			rectTransform.DOMoveY(downPosition,speed).SetUpdate(true).SetEase(Ease.Linear); //落下位置まで移動、Ease.Linearでなめらかに移動させる
		}
		else
		{
			rectTransform.DOMoveY(upPosition, speed).SetUpdate(true).SetEase(Ease.Linear); //初期位置まで移動、Ease.Linearでなめらかに移動させる
		}
	}


}
