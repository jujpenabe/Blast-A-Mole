using System;

namespace BAM
{
	public interface IInstantiator
	{
		Object GetSpawnable();
		void SetSpawnable(Object spawnable);
		void Spawn();
    }
}
