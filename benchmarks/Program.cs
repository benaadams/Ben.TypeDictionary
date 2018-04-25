using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Ben.Collections.TypeDictionary.Benchmarks
{
    [DefaultBenchmark]
    public class Benchmarks
    {
        private Type[] _types;

        private TypeDictionary<int> _typeDictionary;
        private Dictionary<Type, int> _dictionary;

        private TypeDictArray<int> _typeDictArray;
        private TypeDictPure<int> _typeDictPure;

        [Params(1, 2, 3, 4, 6, 8, 16, 24, 32, 40, 64, 128, 256)]
        public int Items { get; set; }

        [Benchmark(Baseline = true, Description = "Dictionary<Type, TValue>")]
        public int Dictionary()
        {
            var result = 0;
            var types = _types;
            for (var i = 0; i < Items; i++)
            {
                result += _dictionary[types[i]];
            }
            return result;
        }

        [Benchmark(Description = "TypeDictionary<TValue>")]
        public int TypeDictionary()
        {
            var result = 0;
            var types = _types;
            for (var i = 0; i < Items; i++)
            {
                result += _typeDictionary[types[i]];
            }
            return result;
        }

        //[Benchmark(Description = "TypeDictionary<TValue> (Pure)")]
        //public int TypeDictionaryPure()
        //{
        //    var result = 0;
        //    var types = _types;
        //    for (var i = 0; i < Items; i++)
        //    {
        //        result += _typeDictPure[types[i]];
        //    }
        //    return result;
        //}

        //[Benchmark(Description = "Type[]+Value[]")]
        //public int Array()
        //{
        //    var result = 0;
        //    var types = _types;
        //    for (var i = 0; i < Items; i++)
        //    {
        //        result += _typeDictArray[types[i]];
        //    }
        //    return result;
        //}

        [GlobalSetup]
        public void GlobalSetup()
        {
            var assembly = Assembly.GetAssembly(typeof(int));
            _types = assembly.GetTypes();

            _typeDictionary = new TypeDictionary<int>();
            _dictionary = new Dictionary<Type, int>();

            _typeDictArray = new TypeDictArray<int>();
            _typeDictPure = new TypeDictPure<int>();

            for (var i = 0; i < Items; i++)
            {
                _typeDictionary[_types[i]] = i;
                _dictionary[_types[i]] = i;

                _typeDictArray[_types[i]] = i;
                _typeDictPure[_types[i]] = i;
            }
        }

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
