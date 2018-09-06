using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class ObjectQuickActions : MonoBehaviour
{
	public UnityEvent upRight;
	public UnityEvent up;
	public UnityEvent upLeft;
	public UnityEvent downLeft;
	public UnityEvent forward;
	public UnityEvent back;

	public void UpRight()
	{
		upRight.Invoke();
	}
	public void Up()
	{
		up.Invoke();
	}
	public void UpLeft()
	{
		upLeft.Invoke();
	}
	public void DownLeft()
	{
		downLeft.Invoke();
	}
	public void Forward()
	{
		forward.Invoke();
	}
	public void Back()
	{
		back.Invoke();
	}
}
