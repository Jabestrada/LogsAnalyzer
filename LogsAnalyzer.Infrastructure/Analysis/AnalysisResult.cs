﻿using LogAnalyzer.Infrastructure.Analysis;
using System.Collections.Generic;

namespace LogsAnalyzer.Infrastructure.Analysis {
    public class AnalysisResult : BaseAnalysisResult {

        public override string ToString() {
            return Text;
        }
    }
}
