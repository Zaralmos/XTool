using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTool.Models;
using XTool.Models.EvaluationModels;

namespace XTool.Statistics
{
    public static class ScalesStatistics
    {
        /// <summary>
        /// Возвращает набор оценок, из которых удалено (100 - selectProcent) процентов крайних оценок (отсечены выбросы).
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        /// <param name="selectionPercent"></param>
        private static IEnumerable<Scales> RemoveOutliers(this IEnumerable<Scales> scalesCollection, double selectionPercent)
        {
            if (selectionPercent > 100 || selectionPercent < 0)
                throw new ArgumentException("Аргумент выражается в процентах [0; 100]");
            Dictionary<int, double> expectations = scalesCollection.AverageDictionary();
            return scalesCollection.OrderBy(scales => scales.OutlineIndex(expectations))
                .Take(Math.Max((int)(selectionPercent / 100 * scalesCollection.Count()), 1));
        }

        /// <summary>
        /// Транспонирует матрицу, составленную построчно из набора объектов, содержащих по 10 значений.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        private static IEnumerable<int>[] TransposeScales(this IEnumerable<Scales> scalesCollection) =>
            scalesCollection.SelectMany(scalesItem => scalesItem.Values.Select((val, i) => new { index = i, value = val }))
            .GroupBy(pair => pair.index)
            .Select(groupsCollection => groupsCollection.Select(groupItem => groupItem.value))
            .ToArray();


        /// <summary>
        /// Возвращает словарь, содержащий средние значения от набора объектов шкал.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        private static Dictionary<int, double> AverageDictionary(this IEnumerable<Scales> scalesCollection) => new Dictionary<int, double>(
            scalesCollection.TransposeScales().Select((valuesCollection, i) => new KeyValuePair<int, double>(i, valuesCollection.Average())));



        /// <summary>
        /// Возвращает среднеквадратичную дистанцию данной величины относительно заданного набора значений
        /// </summary>
        /// <param name="valuesCollection">Набор значений</param>
        /// <param name="comparedValue">Величина, для которой ищется расстояние</param>
        private static double RootMeanSquareDistance(this IEnumerable<int> valuesCollection, int comparedValue) =>
            RootMeanSquareDistance(valuesCollection.Average(), comparedValue);

        /// <summary>
        /// Возвращает среднеквадратичную дистанцию данной величины относительно заданного значения математического ожидания
        /// </summary>
        /// <param name="expectation">Математическое ожидание</param>
        /// <param name="comparedValue">Величина, для которой ищется расстояние</param>
        private static double RootMeanSquareDistance(double expectation, int comparedValue) =>
            Math.Pow(expectation - comparedValue, 2);

        private static double StandardDeviation(this IEnumerable<int> valuesCollection)
        {
            double expectation = valuesCollection.Average();
            return Math.Sqrt(valuesCollection.Average(val => RootMeanSquareDistance(expectation, val)));
        }

        /// <summary>
        /// Возвращает словарь, в котором ключ - номер шкалы, а значение - среднеквадратичное отклонение для шкалы по всему набору объектов.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        private static Dictionary<int, double> StandardDeviation(this IEnumerable<Scales> scalesCollection)
        {
            IEnumerable<int>[] transposedScales = scalesCollection.TransposeScales();
            return new Dictionary<int, double>(
                transposedScales.Select((valuesCollection, i) => new KeyValuePair<int, double>(
                    i, valuesCollection.StandardDeviation())));
        }

        /// <summary>
        /// Возвращает усреднённое среднеквадратичное расстояние шкал данного объекта от средних арифметических наборов значений, распределённых по шкалам.
        /// </summary>
        /// <param name="expectation">Математическое ожидание</param>
        /// <param name="comparedValue">Величина, для которой ищется расстояние</param>
        private static double OutlineIndex(this Scales comparedScales, Dictionary<int, double> expectations) =>
            comparedScales.Values.Select((val, i) => RootMeanSquareDistance(expectations[i], val)).Average();

        /// <summary>
        /// Возвращает показатель отклонения данной оценки от набора оценок.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        /// <param name="scales">Целевая оценка для сравнения.</param>
        public static double OutierIndex(this IEnumerable<Scales> scalesCollection, Scales scales) => scales.OutlineIndex(scalesCollection.AverageDictionary());

        /// <summary>
        /// Возвращает среднюю арифметическую оценку на основе исходного набора оценок.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        public static Scales SimpleAverage(this IEnumerable<Scales> scalesCollection) => new Scales(scalesCollection.AverageDictionary().Values.Select(d => (int)d));

        /// <summary>
        /// Возвращает среднюю квадратичное оценку на основе исхоного набора оценок.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        /// <param name="selectionPercent">Значение в процентах, показывающее какую часть исхожной выборки оставлять после отсечения</param>
        public static Scales RootMeanSquare(this IEnumerable<Scales> scalesCollection, double selectionPercent = 100) => new Scales(
        scalesCollection.RemoveOutliers(selectionPercent).TransposeScales().Select(valuesCollection => (int)(Math.Sqrt(valuesCollection.Average(val => val * val)))));

        public static double AccuracyIndex(this IEnumerable<Scales> scalesCollection, Scales scales)
        {
            Dictionary<int, double> expectations = scalesCollection.AverageDictionary();
            return scales.OutlineIndex(expectations) / scalesCollection.Max(scal => scal.OutlineIndex(expectations));
        }

        /// <summary>
        /// Показатель разброса мнений экспертов по данному набору оценок.
        /// </summary>
        /// <param name="scalesCollection">исходный набор оценок.</param>
        public static double Dispersion(this IEnumerable<Scales> scalesCollection) => scalesCollection.StandardDeviation().Values.Average();
    }
}
