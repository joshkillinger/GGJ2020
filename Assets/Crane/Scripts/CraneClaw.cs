using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CraneClaw : MonoBehaviour
{
    [SerializeField]
    private Transform _rightClaw = null;

    [SerializeField]
    private Transform _leftClaw = null;

    [SerializeField, HideInInspector]
    private bool _isGrasping = false;

    public bool isGrasping
    {
        get => _isGrasping;
        set
        {
            // find the amount that the claws should be rotated by
            float degrees = value ? -0 : 45;

            // apply the rotation
            _leftClaw.transform.rotation = Quaternion.Euler(0, 0, -degrees);
            _rightClaw.transform.rotation = Quaternion.Euler(0, 0, degrees);

            // set the flag so that the proper value can be returned from the getter
            _isGrasping = value;
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(CraneClaw))]
public class CraneClaw_Inspector : Editor
{
	public CraneClaw claw => target as CraneClaw;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		// add a toggle control to the inspector that the user can click on
		bool grasping = EditorGUILayout.Toggle("isGrasping", claw.isGrasping);

		// if the toggle is pressed
		if(grasping != claw.isGrasping) {

			// apply the property to the CraneClaw target
			claw.isGrasping = grasping;
			EditorUtility.SetDirty(claw); // notify that a change to the claw's data has occured
		}
	}
}

#endif