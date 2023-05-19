using System;

namespace BAM.B
{
	public interface IInstantiator
	{
		Object GetSpawnable();
		void SetSpawnable(Object spawnable);
		void Spawn();
    }
}
