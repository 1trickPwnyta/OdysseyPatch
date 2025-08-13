using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Verse;

namespace OdysseyPatch.LavaPathAvoidance
{
    [HarmonyPatch(typeof(PathFinderMapData))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new[] { typeof(Map) })]
    public static class Patch_PathFinderMapData_Constructor
    {
        public static void Postfix(PathFinderMapData __instance, Map map)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                __instance.RegisterSource(new PotentialDangerSource(map));
            }
        }
    }

    [HarmonyPatch(typeof(PathFinderMapData))]
    [HarmonyPatch(nameof(PathFinderMapData.ParameterizeGridJob))]
    public static class Patch_PathFinderMapData_ParameterizeGridJob
    {
        public static void Postfix(List<IPathFinderDataSource> ___sources, ref PathGridJob job)
        {
            if (OdysseyPatchSettings.LavaPathAvoidance)
            {
                PotentialDangerSource source = ___sources.OfType<PotentialDangerSource>().FirstOrDefault();
                if (source != null)
                {
                    NativeBitArray.ReadOnly persistentDanger = job.persistentDanger;
                    NativeBitArray.ReadOnly potentialDanger = source.Data;
                    ulong[] persistentDangerBits = NativeBitArrayToUlongs(ref persistentDanger);
                    ulong[] potentialDangerBits = NativeBitArrayToUlongs(ref potentialDanger);
                    ulong[] combinedBits = Combine(persistentDangerBits, potentialDangerBits);
                    job.persistentDanger = UlongsToNativeBitArray(combinedBits);
                }
            }
        }

        private static ulong[] NativeBitArrayToUlongs(ref NativeBitArray.ReadOnly bits)
        {
            ulong[] result = new ulong[bits.Length / 64 + 1];
            for (int i = 0; i < bits.Length; i += 64)
            {
                result[i / 64] = bits.GetBits(i, 64);
            }
            return result;
        }

        private static NativeBitArray.ReadOnly UlongsToNativeBitArray(ulong[] bits)
        {
            NativeBitArray array = new NativeBitArray(bits.Length, Allocator.Temp);
            for (int i = 0; i < bits.Length; i++)
            {
                array.SetBits(i * 64, bits[i], 64);
            }
            return array.AsReadOnly();
        }

        private static ulong[] Combine(ulong[] a, ulong[] b)
        {
            ulong[] result = new ulong[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] | b[i];
            }
            return result;
        }
    }
}
