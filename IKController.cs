using UnityEngine;

[RequireComponent(typeof(Animator))]

/*	Klasse voor inverse kinematics.
	De voet kan hierdoor bewegen door 'handle' te positioneren en roteren.
	De positionering en rotering wordt in Leg afgehandeld.
	Wanneer handle beweegt, wordt de rechtervoet met OnAnimatorIK() gepositioneerd en geroteerd.
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
