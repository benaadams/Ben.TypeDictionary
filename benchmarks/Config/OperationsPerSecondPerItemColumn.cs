using System;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using static BenchmarkDotNet.Columns.StatisticColumn;

namespace Ben.Collections.TypeDictionary.Benchmarks
{
    public class OperationsPerSecondPerItemColumn : IColumn
    {
        public string Id => nameof(OperationsPerSecondPerItemColumn);

        public string ColumnName { get; } = "Op/s per Item";

        public bool AlwaysShow => true;

        public ColumnCategory Category => ColumnCategory.Statistics;

        public int PriorityInCategory => (int)Priority.Additional;

        public bool IsNumeric => true;

        public UnitType UnitType => UnitType.Dimensionless;

        public bool IsAvailable(Summary summary) => true;

        public bool IsDefault(Summary summary, Benchmark benchmark) => false;

        public string Legend => "Operation per second per Item";

        public string GetValue(Summary summary, Benchmark benchmark)
            => Format(summary, benchmark, SummaryStyle.Default);

        public string GetValue(Summary summary, Benchmark benchmark, ISummaryStyle style)
            => Format(summary, benchmark, SummaryStyle.Default);

        private string Format(Summary summary, Benchmark benchmark, ISummaryStyle style)
        {
            Statistics statistics = summary[benchmark].ResultStatistics;
            var items = (int)benchmark.Parameters["Items"];
            Func<Statistics, double> calc = s => 1.0 * 1000 * 1000 * 1000 * items / s.Mean;


            if (statistics == null)
                return "NA";

            var allValues = summary
                .Reports
                .Where(r => r.ResultStatistics != null)
                .Select(r => calc(r.ResultStatistics))
                .Where(v => !double.IsNaN(v) && !double.IsInfinity(v))
                .Select(v => UnitType == UnitType.Time ? v / style.TimeUnit.NanosecondAmount : v)
                .ToList();
            double minValue = allValues.Any() ? allValues.Min() : 0;
            bool allValuesAreZeros = allValues.All(v => Math.Abs(v) < 1e-9);
            string format = "N" + (allValuesAreZeros ? 1 : GetBestAmountOfDecimalDigits(minValue));

            double value = calc(statistics);
            if (double.IsNaN(value))
                return "NA";
            return /*UnitType == UnitType.Time ? value.ToTimeStr(style.TimeUnit, 1, style.PrintUnitsInContent, format: format) :*/ value.ToString(format);
        }
    }

}
