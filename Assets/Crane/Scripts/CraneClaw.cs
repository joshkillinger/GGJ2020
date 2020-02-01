using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneClaw : MonoBehaviour
{
	[SerializeField]
	private Transform _rightClaw;

	[SerializeField]
	private Transform _leftClaw;

	public bool isOpen { get; set; }
}
