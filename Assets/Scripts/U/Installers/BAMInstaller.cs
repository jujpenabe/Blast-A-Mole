using System.Collections;
using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

namespace BAM
{
    public class BAMInstaller : MonoBehaviour, IInstaller
    {   
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            Debug.Log("BAMInstaller.InstallBindings()");

            // Add Instances
            descriptor.AddInstance("Hello World!");
        }
    }
}
