using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpaceEngineers.Game.EntityComponents.Blocks;
using Torch.Managers.PatchManager;
using VRage.Game;

namespace AI_Range_Extender
{
    [PatchShim]
    public class Patching
    {
        internal static readonly MethodInfo InitCall =
            typeof(MySearchEnemyComponent).GetMethod("Init", BindingFlags.Instance | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");

        internal static readonly MethodInfo PatchInit =
            typeof(Patching).GetMethod(nameof(DoThePatch), BindingFlags.Static | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");

        public static void Patch(PatchContext ctx)
        {
            ctx.GetPattern(InitCall).Prefixes.Add(PatchInit);
        }

        public static void DoThePatch(ref MySearchEnemyComponent __instance, ref MyComponentDefinitionBase definition)
        {
            var def = definition as MySearchEnemyComponentDefinition;
            def.SearchRadius = Core.config.NewAIBlockRange;
        }
    }
}

