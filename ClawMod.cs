using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace ClawMod
{
#if (DEBUG)
    [EnableReloading]
#endif
    
    static class Main
    {
        private static bool _enabled;
        private static bool _modified;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            modEntry.OnUpdate = OnUpdate;
#if (DEBUG)
            modEntry.OnUnload = Unload;
            return true;
        }

        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            return true;
        }
#else
        return true;
    }
#endif

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            _enabled = value;

            if (_modified != _enabled && TryModifyValues(value))
            {
                _modified = _enabled;
            }

            return true;
        }

        static void OnUpdate(UnityModManager.ModEntry modEntry, float dt)
        {
            if (_modified != _enabled && TryModifyValues(_enabled))
            {
                _modified = _enabled;
            }
        }

        static bool TryModifyValues(bool mod)
        {
            String[] claws = new String[44] {
                "f68af48f9ebf32549b5f9fdc4edfd475","408b5ec07bf0fd1468d3f16ef569613a","ee7356a601de6bb40a4929d074337b48","fe7a7bebab335fc4e936e3f9d23fedb4",
                "120e51788082260498a961a38a4fa617","f845e496f8f01d04f8f7c67b62151801","55fd2d4b5906d4d4f95f82dcc0bd6b1e","ffebbc8fc1fc8b7449bdec1fab0779f4",
                "a9afa9dbbf5c8f341bdb1ac801bf2a37","93d053a695996ab4f8d5ed73a3ad1978","962c818a4eb6bda48807e60d17dee876","6c3e365ac6c28f947b5f3d9eebe94948",
                "f83de96081f99be47ae13beb8515211e","66540e66ecead3649a282d3e340e9157","9644b6178d554154aa0de16a586fe7bf","a8a647bf71f901240bb72648580b5f9e",
                "0531d089e8bf4bd49867986a18a3ce4d","35d83033ca2abd14e83b8a41453ccb61","e4d48aeacde77254b9df3e8e17d5dd86","01b84b8b79477a74481f19b9dd7b1fbd",
                "6c9a37ae130da1a46b48113e91ffee36","424b28f3c0807db46a537b150092b3e3","468aec11b69c64040929a0eb231c190b","83df4e26aec72f74fb0a250f769cb2b9",
                "3a27e888cae24684695182fc53554261","0fed2fbd0398bc548ab11c34179e3e52","4dd112a5d15928d4da1f0651f9ae3816","cc21b60762191b04eb1f3d1e30679b62",
                "184117d257a92db47a626f89a9a7260c","dd1584878ecc429479773a8580394a3c","b0d7e27160e9f0c4facfe37eda07e201","e6fd21a3a07e4654089a39eb666e789b",
                "e8177155408433c489c70028c823faf9","a0a8b8e7950f2b648af38095e76aa146","572c8c6d25eafc44bb6df322e14541f1","85f3e48a9278b2e499582a3e5e3bda17",
                "8c9716bc50496f1488c8a418b9fc7d05","87e7081370c84ba4faa4ea1ac0d173d2","776868fba5e120e429276434113042d9","eb0b2789613d0964eb73efd29ac67f67",
                "2caa6f370fbb5b34ab6ad307f0d93708","a7379e2173493644088dec6f09158038","dd9768b309bdcbc43ae46102963fc211","f617ad00079a75149949f79a3c15ee4a"
            };
            // Starting from top Row: Abyssal, Black, Blue, Brass, Bronze, Copper, Gold, Green, Red, Silver, White

            LibraryScriptableObject lib = typeof(ResourcesLibrary)
                .GetField("s_LibraryObject", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null) as LibraryScriptableObject;
            
            if (lib == null)
                return false;

            if (mod)
            {
                
                for (int countClaws = 0; countClaws < claws.Length; countClaws++)
                {

                    BlueprintActivatableAbility bloodlineClawsAbililyLevel
                        = lib.BlueprintsByAssetId[claws[countClaws]] as BlueprintActivatableAbility;
                    (bloodlineClawsAbililyLevel.ComponentsArray[0] as ActivatableAbilityResourceLogic).SpendType =
                        ActivatableAbilityResourceLogic.ResourceSpendType.None;
                    bloodlineClawsAbililyLevel.DeactivateIfOwnerDisabled = false;
                    bloodlineClawsAbililyLevel.DeactivateIfCombatEnded = false;
                    bloodlineClawsAbililyLevel.IsOnByDefault = true;                    
                }
            }
            else
            {
                
                for (int countClaws = 0; countClaws < claws.Length; countClaws++)
                {

                    BlueprintActivatableAbility bloodlineClawsAbililyLevel
                        = lib.BlueprintsByAssetId[claws[countClaws]] as BlueprintActivatableAbility;
                    (bloodlineClawsAbililyLevel.ComponentsArray[0] as ActivatableAbilityResourceLogic).SpendType =
                        ActivatableAbilityResourceLogic.ResourceSpendType.None;
                    bloodlineClawsAbililyLevel.DeactivateIfOwnerDisabled = false;
                    bloodlineClawsAbililyLevel.DeactivateIfCombatEnded = false;
                    bloodlineClawsAbililyLevel.IsOnByDefault = true;
                    
                }
            }

            return true;
        }
    }
}
