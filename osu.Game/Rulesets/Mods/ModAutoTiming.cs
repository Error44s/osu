// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Localisation;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModAutoTiming : Mod
    {
        public override string Name => "Auto Timing";
        public override string Acronym => "AT";
        public override ModType Type => ModType.Conversion;
        public override LocalisableString Description => "Hit the Circles at the correct time.";
        public override double ScoreMultiplier => 1.0;

        [SettingSource("Max Time Offset", "The maximum possible offset for start times.")]
        public virtual BindableDouble MaxTimeOffset { get; } = new BindableDouble(200) { MinValue = 0, MaxValue = 500 };
    }
}
