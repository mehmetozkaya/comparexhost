using Abp.Application.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Features
{
    public class CompareXFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            var sampleBooleanFeature = context.Create("SampleBooleanFeature", defaultValue: "false");
            sampleBooleanFeature.CreateChildFeature("SampleNumericFeature", defaultValue: "10");
            context.Create("SampleSelectionFeature", defaultValue: "B");            
        }
    }
}
