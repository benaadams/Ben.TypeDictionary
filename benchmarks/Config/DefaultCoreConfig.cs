// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;
using BenchmarkDotNet.Validators;

namespace Ben.Collections.TypeDictionary.Benchmarks
{
    public class DefaultCoreConfig : ManualConfig
    {
        public DefaultCoreConfig()
        {
            Add(MarkdownExporter.GitHub);

            Add(MemoryDiagnoser.Default);
            Add(ItemsColumn.MeanPerItem);
            Add(ItemsColumn.OperationsPerSecondPerItem);
            Add(DefaultColumnProviders.Job);
            Add(DefaultColumnProviders.Params);
            Add(DefaultColumnProviders.Diagnosers);
            Add(BaselineScaledColumn.Scaled);


            Add(JitOptimizationsValidator.FailOnError);

            Add(Job.Core
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp21))
                .With(new GcMode { Server = true })
                .With(RunStrategy.Throughput));
        }
        
    }
}
