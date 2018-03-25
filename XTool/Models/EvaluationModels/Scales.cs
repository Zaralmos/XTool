using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XTool.Data.ModelInterfaces;
using XTool.Models.ActorModels;

namespace XTool.Models.EvaluationModels
{
    public class Scales : IStorageModel<int> // : IEnumerable<int>
    {
        public int Id { get; set; }

        [NotMapped]
        public static IReadOnlyList<string> ScalesNames { get; }

        static Scales() => ScalesNames = typeof(Scales).GetProperties().Where(p => p.IsDefined(typeof(ScaleValueAttribute), false)).Select(prop => prop.Name).ToList();

        private readonly int[] _values = new int[ScalesNames.Count]; // м.б. перенести в обычной конструктор

        private int IndexOf(string name) => ScalesNames.Select((n, i) => new { curName = n, index = i })
            .FirstOrDefault(pair => pair.curName == name).index;

        public IUpdateble Update(IUpdateble model)
        {
            throw new NotImplementedException();
        }

        public Scales()
        { }

        public Scales(IEnumerable<int> values)
        {
            for (int i = 0; i < Math.Min(Values.Count(), values.Count()); i++)
            {
                this[i] = values.ElementAt(i);
            }
        }

        //  public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)_values).GetEnumerator();

        // IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<int>)_values).GetEnumerator();

        [NotMapped]
        public IEnumerable<int> Values => new List<int>(_values);

        [NotMapped]
        public int this[int i]
        {
            get => _values[i];
            set
            {
                if (value > 100 || value < 0)
                    throw new ArgumentException("Значение, выражаемле в процентах должно быть в пределах диапозона [0; 100]");
                if (i < 0 || i >= _values.Length)
                    throw new KeyNotFoundException($"Не существует шкалы под номером {i}.");
                _values[i] = value;
            }
        }

        [ScaleValue]
        public int Hypochondriasis
        {
            get => _values[IndexOf(nameof(Hypochondriasis))];
            set => _values[IndexOf(nameof(Hypochondriasis))] = value;
        }

        [ScaleValue]
        public int Depression
        {
            get => _values[IndexOf(nameof(Depression))];
            set => _values[IndexOf(nameof(Depression))] = value;
        }

        [ScaleValue]
        public int Hysteria
        {
            get => _values[IndexOf(nameof(Hysteria))];
            set => _values[IndexOf(nameof(Hysteria))] = value;
        }

        [ScaleValue]
        public int PsychopathicDeviate
        {
            get => _values[IndexOf(nameof(PsychopathicDeviate))];
            set => _values[IndexOf(nameof(PsychopathicDeviate))] = value;
        }

        [ScaleValue]
        public int MasculinityFeminity
        {
            get => _values[IndexOf(nameof(MasculinityFeminity))];
            set => _values[IndexOf(nameof(MasculinityFeminity))] = value;
        }

        [ScaleValue]
        public int Paranoia
        {
            get => _values[IndexOf(nameof(Paranoia))];
            set => _values[IndexOf(nameof(Paranoia))] = value;
        }

        [ScaleValue]
        public int Psychasthenia
        {
            get => _values[IndexOf(nameof(Psychasthenia))];
            set => _values[IndexOf(nameof(Psychasthenia))] = value;
        }

        [ScaleValue]
        public int Schizophrenia
        {
            get => _values[IndexOf(nameof(Schizophrenia))];
            set => _values[IndexOf(nameof(Schizophrenia))] = value;
        }

        [ScaleValue]
        public int Hypomania
        {
            get => _values[IndexOf(nameof(Hypomania))];
            set => _values[IndexOf(nameof(Hypomania))] = value;
        }

        [ScaleValue]
        public int SocialInteroversion
        {
            get => _values[IndexOf(nameof(SocialInteroversion))];
            set => _values[IndexOf(nameof(SocialInteroversion))] = value;
        }
    }
}
