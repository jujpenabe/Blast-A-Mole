using UnityEngine;

namespace BAM.B
{
	public interface IShootable
	{
		Vector3 Direction
		{
			get;
			set;
		}
		float Speed
		{
			get;
			set;
		}
		void Shoot(Vector3 direction, float speed);
	}
}
