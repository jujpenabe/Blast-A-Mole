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
		int Power
		{
			get;
			set;
		}
		void Shoot(Vector3 direction, float speed, int power);
	}
}
