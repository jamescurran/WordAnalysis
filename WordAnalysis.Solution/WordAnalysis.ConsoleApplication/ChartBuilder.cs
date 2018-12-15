using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace WordAnalysis.ConsoleApplication
{
    public class ChartBuilder
    {
        public static Chart GetChart<T>(IList<Counter<T>> data, string title)
        {
            return GetChart(data, title, "Letter", "Frequency");
        }

        public static Chart GetChart<T>(IList<Counter<T>> data, string title, string xAxis, string yAxis)
        {
            var chart = new Chart
            {
                Width = 600
            };
            chart.Titles.Add(title);

            var series = new Series();
            series.Points.DataBindXY(data.Select(d=>d.AsString).ToList(), data.Select(d=>d.Count).ToList());
            chart.Series.Add(series);

            var chartArea = new ChartArea
            {
                AxisX = {Interval = 1, Title = xAxis, MajorGrid = {Enabled = false}, MinorGrid = {Enabled = false}},
                AxisY = {Title = yAxis, LabelStyle = {Format = "{0:0,}K"}}
            };

            chart.ChartAreas.Add(chartArea);

            return chart;
        }

        public static void SaveChart(Chart chart, string filePath)
        {
            chart.SaveImage(filePath, ChartImageFormat.Png);
        }

        public static void SaveLetterFrequencyChart(IList<Counter<char>> letterFrequency)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charts/letter-frequency.png");
            ChartBuilder.SaveChart(ChartBuilder.GetChart(letterFrequency, "Letter Frequency"), filePath);
        }

        public static void SaveLetterStartingWithChart(List<Letter> letters)
        {
            var data = letters.OrderBy(x => x.Value).Select(d => new Counter<char>(d.Value, d.StartingWith)).ToList();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charts/letter-ending-with.png");
            ChartBuilder.SaveChart(ChartBuilder.GetChart(data, "Ending With Frequency"), filePath);
        }

        public static void SaveLetterEndingWithChart(List<Letter> letters)
        {
            var data = letters.OrderBy(x => x.Value).Select(d=> new Counter<char>(d.Value, d.EndingWith)).ToList();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charts/letter-starting-with.png");
            ChartBuilder.SaveChart(ChartBuilder.GetChart(data, "Starting With Frequency"), filePath);
        }

        public static void SaveWordLengthFrequencyChart(IList<Counter<int>> wordLengthFrequency)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charts/word-length-with.png");
            ChartBuilder.SaveChart(ChartBuilder.GetChart(wordLengthFrequency, "Word Length Frequency", "Word Length", "Frequency"), filePath);
        }

        public static void SaveDoubleLetterFrequencyChart(List<Letter> letters)
        {
            var data = letters.OrderBy(x => x.Value).Select(d => new Counter<char>(d.Value, d.DoubleLetters)).ToList();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charts/double-letters.png");
            ChartBuilder.SaveChart(ChartBuilder.GetChart(data, "Double Letter Frequency"), filePath);
        }
    }
}
