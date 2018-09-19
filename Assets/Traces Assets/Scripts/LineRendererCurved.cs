using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LineRendererCurved : MonoBehaviour
{
	private LineRenderer _lr;
	private float _angle;
	private float _startAngle;
	private float _endAngle;
	private float _arcLength;
	private float _radius;
	
	public float Radius;
	public float StartAngle;
	public float EndAngle;
	public int Quality;
	
	public enum Orientation
	{
	Forward,
	Right,
	Down
	}

	public Orientation orientation;

	public void Start()
	{
		_lr = transform.GetComponent<LineRenderer>();
	}
	
	private void Update ()
	{
		_lr.positionCount = Quality;
		_angle = StartAngle;
		_arcLength = EndAngle - StartAngle;
		for (var i = 0; i < Quality; i++)
		{
			var x = Mathf.Sin(Mathf.Deg2Rad * _angle) * Radius;
			var y = Mathf.Cos(Mathf.Deg2Rad * _angle) * Radius;
			switch (orientation)
			{
				case Orientation.Forward:
					_lr.SetPosition(i, new Vector3(x, y, 0));
					break;
				case Orientation.Right:
					_lr.SetPosition(i, new Vector3(x, 0, y));
					break;
				case Orientation.Down:
					_lr.SetPosition(i, new Vector3(0, x, y));
					break;
				default:
					break;
			}
			_angle += (_arcLength / Quality);
		}
	}
}
