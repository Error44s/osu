// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Mods;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Osu.Tests.Mods
{
    public partial class TestSceneOsuModAutoTiming : OsuModTestScene
    {
        [Test]
        public void TestAutoTiming()
        {
            var beatmap = new Beatmap
            {
                BeatmapInfo = new BeatmapInfo
                {
                    Difficulty = new BeatmapDifficulty
                    {
                        CircleSize = 5,
                        ApproachRate = 8,
                        OverallDifficulty = 7
                    }
                },
                HitObjects = new List<HitObject>
                {
                    new HitCircle { StartTime = 1000 },
                    new HitCircle { StartTime = 2000 },
                    new HitCircle { StartTime = 3000 },
                    new HitCircle { StartTime = 4000 }
                }
            };

            var mod = new OsuModAutoTiming();
            mod.ApplyToBeatmap(beatmap);

            foreach (var hitObject in beatmap.HitObjects)
            {
                if (hitObject is HitCircle hitCircle)
                {
                    if (hitCircle.StartTime == 1000 || hitCircle.StartTime == 2000 || hitCircle.StartTime == 3000 || hitCircle.StartTime == 4000)
                    {
                        Assert.Fail("A HitCircle has not changed its start time.");
                    }
                }
            }

            Assert.Pass("All start times were successfully changed.");
        }
    }
}
