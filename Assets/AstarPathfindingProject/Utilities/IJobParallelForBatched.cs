// This file is only included because the Unity.Jobs package is currently experimental and it seems bad to rely on it.
// The Unity.Jobs version of this interface will be used when it is stable.
using System;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Pathfinding.Jobs {
	[JobProducerType(typeof(JobParallelForBatchedExtensions.ParallelForBatchJobStruct<>))]
	public interface IJobParallelForBatched {
		bool allowBoundsChecks { get; }
		void Execute (int startIndex, int count);
	}

	public static class JobParallelForBatchedExtensions {
		internal struct ParallelForBatchJobStruct<T> where T : struct, IJobParallelForBatched {
			static public IntPtr jobReflectionData;

			public static IntPtr Initialize () {
				if (jobReflectionData == IntPtr.Zero)
					jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, (ExecuteJobFunction)Execute);
				return jobReflectionData;
			}

			public delegate void ExecuteJobFunction (ref T data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
			public unsafe static void Execute (ref T jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex) {
				while (true) {
					int begin;
					int end;
					if (!JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end))
						return;

					#if ENABLE_UNITY_COLLECTIONS_CHECKS
					if (jobData.allowBoundsChecks) JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf(ref jobData), begin, end - begin);
					#endif

					jobData.Execute(begin, end - begin);
				}
			}
		}

		unsafe static public JobHandle ScheduleBatch<T>(this T jobData, int arrayLength, int minIndicesPerJobCount, JobHandle dependsOn = new JobHandle ()) where T : struct, IJobParallelForBatched {
			var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), ParallelForBatchJobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);

			return JobsUtility.ScheduleParallelFor(ref scheduleParams, arrayLength, minIndicesPerJobCount);
		}

		unsafe static public void RunBatch<T>(this T jobData, int arrayLength) where T : struct, IJobParallelForBatched {
			var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), ParallelForBatchJobStruct<T>.Initialize(), new JobHandle(), ScheduleMode.Run);

			JobsUtility.ScheduleParallelFor(ref scheduleParams, arrayLength, arrayLength);
		}
	}
}
