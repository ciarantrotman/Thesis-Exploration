using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class FingerTriggerController : MonoBehaviour 
{
	public enum Hand {LeftHand, RightHand}
	public Hand hand;
	
	private float _gestureTimer;
	private float _holdPercent;
	private int _gestureTouchCount = 1;
	[HideInInspector]
	public bool Touching;
	private bool _switch;
	private float _currentTime;
	private bool _holdTriggered;
	[HideInInspector]
	public bool Grab;
	private Collider _lastCollider;
	private SkinnedMeshRenderer _holdTimerUi;
	[Header("Finger Trigger Settings")]
	[Space(5)]
	public float HoldTimeout;
	public float GestureTimeout;
	public int GestureTouchCount;
	public GameObject HoldTimerUi;
	[Space(10)]
	[Header("Fingertip Touch Events")]
	[Space(5)]
	public UnityEvent OnIndexTipTouch;
	public UnityEvent OnMiddleTipTouch;
	public UnityEvent OnRingTipTouch;
	public UnityEvent OnPinkyTipTouch;
	[Space(10)]
	[Header("Fingertip Hold Events")]
	[Space(5)]
	public UnityEvent OnIndexTipHold;
	public UnityEvent OnMiddleTipHold;
	public UnityEvent OnRingTipHold;
	public UnityEvent OnPinkyTipHold;
	[Space(10)]
	[Header("Fingertip Gesture Events")]
	[Space(5)]
	public UnityEvent OnIndexTipGesture;
	public UnityEvent OnMiddleTipGesture;
	public UnityEvent OnRingTipGesture;
	public UnityEvent OnPinkyTipGesture;

	private void Start()
	{
		if (HoldTimerUi == null) return;
		//_holdTimerUi = HoldTimerUi.GetComponent<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (Grab == true) return;
		if (_lastCollider != null && hand.ToString() != _lastCollider.tag.ToString()) return;
		
		var debugTextTop = GameObject.Find("ActiveObjectTextTop").GetComponent<TextMeshPro>();
		debugTextTop.SetText("Count: {0}", _gestureTouchCount);
		var debugTextBottom = GameObject.Find("ActiveObjectTextBottom").GetComponent<TextMeshPro>();
		debugTextBottom.SetText("{0:2}", _gestureTimer);
		
		if (_gestureTimer > HoldTimeout && Touching == true && _holdTriggered == false)
		{
			_holdTriggered = true;
			switch (_lastCollider.gameObject.name)
			{
				case "IndexTip":
					OnIndexTipHold.Invoke();
					break;
				case "MiddleTip":
					OnMiddleTipHold.Invoke();
					break;
				case "RingTip":
					OnRingTipHold.Invoke();
					break;
				case "PinkyTip":
					OnPinkyTipHold.Invoke();
					break;
				default:
					break;
			}
		}
		
		if (_gestureTouchCount == GestureTouchCount)
		{
			_gestureTouchCount = 1;
			switch (_lastCollider.gameObject.name)
			{
				case "IndexTip":
					OnIndexTipGesture.Invoke();
					break;
				case "MiddleTip":
					OnMiddleTipGesture.Invoke();
					break;
				case "RingTip":
					OnRingTipGesture.Invoke();
					break;
				case "PinkyTip":
					OnPinkyTipGesture.Invoke();
					break;
				default:
					break;
			}
		}

		if (Touching == true && HoldTimerUi != null)
			Invoke("HoldVisual",0);
			
		if (Touching == false && HoldTimerUi != null)
			HoldTimerUi.SetActive(false);
		
		_gestureTimer++;
	}

	private void OnCollisionEnter(Collision thumbCollision)
	{
		if (Grab == true || hand.ToString() != thumbCollision.collider.gameObject.tag.ToString()) return;
		
		if (thumbCollision.collider == _lastCollider && _gestureTimer < GestureTimeout)
			_gestureTouchCount++;

		_switch = true;
		_currentTime = 0f;
		
		switch (thumbCollision.gameObject.name)
		{
			case "IndexTip":
				_gestureTimer = 0;
				Touching = true;
				_lastCollider = thumbCollision.collider;
				OnIndexTipTouch.Invoke();
				break;
			case "MiddleTip":
				_gestureTimer = 0;
				Touching = true;
				_lastCollider = thumbCollision.collider;
				OnMiddleTipTouch.Invoke();
				break;
			case "RingTip":
				_gestureTimer = 0;
				Touching = true;
				_lastCollider = thumbCollision.collider;
				OnRingTipTouch.Invoke();
				break;
			case "PinkyTip":
				_gestureTimer = 0;
				Touching = true;
				_lastCollider = thumbCollision.collider;
				OnPinkyTipTouch.Invoke();
				break;
			default:
				break;
		}
	}

	private void OnCollisionExit(Collision thumbCollision)
	{
		if (Grab == true || hand.ToString() != thumbCollision.collider.gameObject.tag.ToString()) return;
		
		_holdTriggered = false;
		
		if (HoldTimerUi != null && _switch == false)
			HoldTimerUi.GetComponent<LineRendererCurved>().EndAngle = 0;
		
		//_holdTimerUi.SetBlendShapeWeight(0, 0);
		
		switch (thumbCollision.gameObject.name)
		{
			case "IndexTip":
				Touching = false;
				break;
			case "MiddleTip":
				Touching = false;
				break;
			case "RingTip":
				Touching = false;
				break;
			case "PinkyTip":
				Touching = false;
				break;
			default:
				break;
		}
	}

	public void HoldVisual()
	{
		HoldTimerUi.SetActive(true);
		HoldTimerUi.transform.position = _lastCollider.transform.position;
		HoldTimerUi.transform.rotation = _lastCollider.transform.rotation;
		if (_switch == false) return; 
		if (_currentTime <= .4f && Touching == true)
		{
			_currentTime += Time.deltaTime;
			HoldTimerUi.GetComponent<LineRendererCurved>().EndAngle = Mathf.Lerp(90f, 360f, _currentTime/.4f);
		}
		else
		{	
			_currentTime = 0f;
			_switch = false;
		}
		//_holdPercent = _gestureTimer / HoldTimeout;
		//_holdTimerUi.SetBlendShapeWeight(0, _holdPercent * 100);
	}
	
	public void OnGraspBegin()
	{
		Grab = true;
	}
	
	public void OnGraspEnd()
	{
		Grab = false;
	}
}
