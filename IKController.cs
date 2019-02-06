using UnityEngine;

[RequireComponent(typeof(Animator))]

/*	Klasse voor inverse kinematics.
	De voet kan hierdoor bewegen door 'handle' te positioneren.
	De positionering van handle wordt in AppManager gedaan.
	Wanneer handle beweegt, wordt de rechtervoet met OnAnimatorIK() gepositioneerd.
*/
public class IKController : MonoBehaviour
{
	protected Animator animator;

	public bool ikActive = true;
	public Transform handle = null;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnAnimatorIK()
	{
		if (animator)
		{
			if (ikActive)
			{
				if (handle != null)
				{
					animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
					animator.SetIKPosition(AvatarIKGoal.LeftFoot, handle.position);
					animator.SetIKRotation(AvatarIKGoal.LeftFoot, handle.rotation);
				}
			}
		}
	}
}
