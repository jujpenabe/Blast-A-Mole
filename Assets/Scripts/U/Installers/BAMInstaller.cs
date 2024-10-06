using System.Collections;
using System.Collections.Generic;
using BAM.B;
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
            var score = new ScoreUsecase();

            // Add Bindings
            descriptor.AddInstance(score,typeof(IScoreUsecase));
        }
    }
}
