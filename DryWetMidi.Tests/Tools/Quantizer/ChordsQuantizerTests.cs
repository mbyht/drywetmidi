﻿using System.Collections.Generic;
using Melanchall.DryWetMidi.Smf.Interaction;
using Melanchall.DryWetMidi.Tests.Common;
using Melanchall.DryWetMidi.Tools;
using NUnit.Framework;

namespace Melanchall.DryWetMidi.Tests.Tools
{
    [TestFixture]
    public sealed class ChordsQuantizerTests : LengthedObjectsQuantizerTests<Chord, ChordsQuantizingSettings>
    {
        #region Nested classes

        private sealed class SkipChordsQuantizer : ChordsQuantizer
        {
            #region Overrides

            protected override QuantizingCorrectionResult CorrectObject(Chord obj, long time, IGrid grid, IReadOnlyCollection<long> gridTimes, TempoMap tempoMap, ChordsQuantizingSettings settings)
            {
                return QuantizingCorrectionResult.Skip;
            }

            #endregion
        }

        private sealed class FixedTimeChordsQuantizer : ChordsQuantizer
        {
            #region Fields

            private readonly long _time;

            #endregion

            #region Constructor

            public FixedTimeChordsQuantizer(long time)
            {
                _time = time;
            }

            #endregion

            #region Overrides

            protected override QuantizingCorrectionResult CorrectObject(Chord obj, long time, IGrid grid, IReadOnlyCollection<long> gridTimes, TempoMap tempoMap, ChordsQuantizingSettings settings)
            {
                return new QuantizingCorrectionResult(QuantizingInstruction.Apply, _time);
            }

            #endregion
        }

        #endregion

        #region Constructor

        public ChordsQuantizerTests()
            : base(new ChordMethods(),
                   new ChordsQuantizer(),
                   new SkipChordsQuantizer(),
                   time => new FixedTimeChordsQuantizer(time))
        {
        }

        #endregion
    }
}
