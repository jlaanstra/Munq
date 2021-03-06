﻿using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Performance
{
	class Program
	{
		const long DefaultIterations = 10000;
		static long baseTicks = 0;

		//static readonly List<long> BatchIterations = new List<long> { 10000 };
		static readonly List<long> BatchIterations = new List<long> { 1000, 5000, 20000, 100000, 250000, 1000000, 5000000 };
		static readonly List<UseCaseInfo> useCases = new List<UseCaseInfo>
		{
			new PlainUseCase(),
			new MunqUseCase(),
			new MunqGenericUseCase(),
			new UnityUseCase(),
			new AutofacUseCase(), 
			new StructureMapUseCase(),
			new Ninject2UseCase(), 
			new WindsorUseCase(), 
			new PlainSingletonUseCase(),
			new MunqSingletonUseCase(),
			new UnitySingletonUseCase(),
			new AutofacSingletoncUseCase(),
			new StructureMapSingletonUseCase(),
			new Ninject2SingletonUseCase(),
			new WindsorSingletonUseCase(),
			new HiroUseCase()
	};

		static void Main(string[] args)
		{
			if (args.Length == 1 && args[0] == "csv")
				RunBatch();
			else
				RunInteractive(args);
				
			Console.ReadKey();
		}

		private static void RunBatch()
		{
			Console.Write(";");
			useCases.ForEach(uc => Console.Write("{0};", uc.Name));
			Console.WriteLine();
			BatchIterations.ForEach(iterations =>
			{
				baseTicks = 0;
				Console.Write("{0};", iterations);
				useCases.ForEach(uc =>
				{
					// warmup
					uc.UseCase.Run();
					GC.Collect();
					Console.Write("{0:N2};", Measure(uc.UseCase.Run, iterations)/baseTicks);
				});
				Console.WriteLine();
			});
		}

		private static void RunInteractive(string[] args)
		{
			long iterations = DefaultIterations;

			if (args.Length != 0)
				iterations = long.Parse(args[0]);

			Console.WriteLine("Running {0} iterations for each use case.", iterations);
			Console.WriteLine("{0,30}: {1,12} - {2,12} - {3,12}", "Test", "Ticks", "mSec", "Normalized");
			baseTicks = 0;
			useCases.ForEach(uc =>
			{
				// warmup
				uc.UseCase.Run();
				GC.Collect();
				var ticks = Measure(uc.UseCase.Run, iterations);
				Console.WriteLine("{0,30}: {1,12:N0} - {2,12:N2} - {3,12:N2}", uc.Name, ticks, ticks*1000 / Stopwatch.Frequency, ticks / baseTicks);
			});
		}

		private static string Pad(int count, string value)
		{
			return value + new string(' ', count - value.Length);
		}

		private static decimal Measure(Action action, long iterations)
		{
			GC.Collect();
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			for (int i = 0; i < iterations; i++)
			{
				action();
			}

			stopwatch.Stop();
			if (baseTicks == 0)
				baseTicks = stopwatch.ElapsedTicks;

			return stopwatch.ElapsedTicks ;
		}
	}
}
