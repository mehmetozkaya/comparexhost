using Abp.Application.Features;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.UI.Inputs;

namespace CompareX.Features
{
    public class CompareXFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            //var sampleBooleanFeature = context.Create("SampleBooleanFeature", defaultValue: "false");
            //sampleBooleanFeature.CreateChildFeature("SampleNumericFeature", defaultValue: "10");
            //context.Create("SampleSelectionFeature", defaultValue: "B");

            var sampleBooleanFeature = context.Create(
                                       CompareXFeatures.SampleBooleanFeature,
                                       defaultValue: "false",
                                       displayName: L("Sample boolean feature"),
                                       inputType: new CheckboxInputType()
                                       );

            sampleBooleanFeature.CreateChildFeature(
                                    CompareXFeatures.SampleNumericFeature,
                                    defaultValue: "10",
                                    displayName: L("Sample numeric feature"),
                                    inputType: new SingleLineStringInputType(new NumericValueValidator(1, 1000000))
                                    );

            context.Create(
                        CompareXFeatures.SampleSelectionFeature,
                        defaultValue: "B",
                        displayName: L("Sample selection feature"),
                        inputType: new ComboboxInputType(
                            new StaticLocalizableComboboxItemSource(
                                new LocalizableComboboxItem("A", L("Selection A")),
                                new LocalizableComboboxItem("B", L("Selection B")),
                                new LocalizableComboboxItem("C", L("Selection C"))
                                )
                            )
                        );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CompareXConsts.LocalizationSourceName);
        }
    }
}
