using System;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using static BenchmarkDotNet.Columns.StatisticColumn;

namespace Ben.Collections.TypeDictionary.Benchmarks
{
    public class ItemsColumn : IColumn
    {
        private string _name;
        private string _legend;
        private UnitType _unitType;
        private Func<int, Statistics, double> _calc;

        private ItemsColumn(
            string name,
            string legend,
            UnitType unitType,
            Func<int, Statistics, double> calc)
        {
            _name = name;
            _legend = legend;
            _unitType = unitType;
            _calc = calc;
        }

        public static ItemsColumn OperationsPerSecondPerItem { get; } = new ItemsColumn("Op/s per Item", "Operation per second per Item", UnitType.Dimensionless, (i, s) => 1.0 * 1000 * 1000 * 1000 * i / s.Mean);
        public static ItemsColumn MeanPerItem { get; } = new ItemsColumn("Mean per Item", "Mean per Item", UnitType.Time, (i, s) => s.Mean / i);

        string IColumn.Id => nameof(ItemsColumn) + "." + _name;

        string IColumn.ColumnName => _name;

        bool IColumn.AlwaysShow => true;

        ColumnCategory IColumn.Category => ColumnCategory.Statistics;

        int IColumn.PriorityInCategory => (int)Priority.Additional;

        bool IColumn.IsNumeric => true;

        public UnitType UnitType => _unitType;

        bool IColumn.IsAvailable(Summary summary) => true;

        bool IColumn.IsDefault(Summary summary, Benchmark benchmark) => false;

        string IColumn.Legend => _legend;

        string IColumn.GetValue(Summary summary, Benchmark benchmark)
            => Format(summary, benchmark, SummaryStyle.Default);

        string IColumn.GetValue(Summary summary, Benchmark benchmark, ISummaryStyle style)
            => Format(summary, benchmark, style);

        string Format(Summary summary, Benchmark benchmark, ISummaryStyle style)
        {
            Statistics statistics = summary[benchmark].ResultStatistics;
            var items = (int)benchmark.Parameters["Items"];


            if (statistics == null)
                return "NA";

            var allValues = summary
                .Reports
                .Where(r => r.ResultStatistics != null)
                .Select(r => _calc(items, r.ResultStatistics))
                .Where(v => !double.IsNaN(v) && !double.IsInfinity(v))
                .Select(v => UnitType == UnitType.Time ? v / 1 : v)
                .ToList();
            double minValue = allValues.Any() ? allValues.Min() : 0;
            bool allValuesAreZeros = allValues.All(v => Math.Abs(v) < 1e-9);
            string format = "N" + (allValuesAreZeros ? 1 : GetBestAmountOfDecimalDigits(minValue));

            double value = _calc(items, statistics);
            if (double.IsNaN(value))
                return "NA";
            return UnitType == UnitType.Time ? value.ToTimeStr(style.TimeUnit, 1, style.PrintUnitsInContent, format: format) : value.ToString(format);
        }
    }

}
