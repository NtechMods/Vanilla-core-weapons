using System.Collections.Generic;
using VRageMath;
using static WeaponThread.WeaponStructure.ShieldDefinition;
using static WeaponThread.WeaponStructure.PartAnimationSetDef.EventTriggers;

namespace WeaponThread
{
    partial class Weapons
    {
        internal List<WeaponStructure.WeaponDefinition> Weapon = new List<WeaponStructure.WeaponDefinition>();
        internal void ConfigFiles(params WeaponStructure.WeaponDefinition[] defs)
        {
            foreach (var def in defs) Weapon.Add(def);
        }

        internal WeaponStructure.WeaponDefinition[] ReturnDefs()
        {
            var weaponDefinitions = new WeaponStructure.WeaponDefinition[Weapon.Count];
            for (int i = 0; i < Weapon.Count; i++) weaponDefinitions[i] = Weapon[i];
            Weapon.Clear();
            return weaponDefinitions;
        }

        internal WeaponStructure.ParticleOptions Options(bool loop, bool restart, float distance, float duration, float scale, float hitPlayChance = 1f)
        {
            return new WeaponStructure.ParticleOptions
            {
                Loop = loop,
                Restart = restart,
                MaxDistance = distance,
                MaxDuration = duration,
                Scale = scale,
                HitPlayChance = hitPlayChance,
            };
        }

        internal WeaponStructure.Detonate Options(bool detonateOnEnd, bool armOnlyOnHit, float detonationDamage, float detonationRadius)
        {
            return new WeaponStructure.Detonate
            {
                DetonateOnEnd = detonateOnEnd,
                ArmOnlyOnHit = armOnlyOnHit,
                DetonationDamage = detonationDamage,
                DetonationRadius = detonationRadius,
            };
        }

        internal WeaponStructure.Explosion Options(bool noVisuals, bool noSound, float scale, string customParticle, string customSound)
        {
            return new WeaponStructure.Explosion
            {
                NoVisuals = noVisuals,
                NoSound = noSound,
                Scale = scale,
                CustomParticle = customParticle,
                CustomSound = customSound,
            };
        }

        internal WeaponStructure.GridSizeDefinition Options(float largeGridModifier, float smallGridModifier)
        {
            return new WeaponStructure.GridSizeDefinition { Large = largeGridModifier, Small = smallGridModifier };
        }

        internal WeaponStructure.ObjectsHit Options(int maxObjectsHit, bool countBlocks)
        {
            return new WeaponStructure.ObjectsHit { MaxObjectsHit = maxObjectsHit, CountBlocks = countBlocks };
        }

        internal WeaponStructure.Shrapnel Options(float baseDamage, int fragments, float maxTrajectory, bool noAudioVisual, bool noGuidance, WeaponStructure.Shrapnel.ShrapnelShape shape, bool areaEffect = false)
        {
            return new WeaponStructure.Shrapnel { BaseDamage = baseDamage, Fragments = fragments, MaxTrajectory = maxTrajectory, NoAudioVisual = noAudioVisual, NoGuidance = noGuidance, Shape = shape};
        }

        internal WeaponStructure.CustomScalesDefinition SubTypeIds(bool ignoreOthers, params WeaponStructure.CustomBlocksDefinition[] customDefScale)
        {
            return new WeaponStructure.CustomScalesDefinition {IgnoreAllOthers = ignoreOthers, Types = customDefScale};
        }

        internal WeaponStructure.ArmorDefinition Options(float armor, float light, float heavy, float nonArmor)
        {
            return new WeaponStructure.ArmorDefinition { Armor = armor, Light = light, Heavy = heavy, NonArmor = nonArmor };
        }

        internal WeaponStructure.OffsetEffect Options(double maxOffset, double minLength, double maxLength)
        {
            return new WeaponStructure.OffsetEffect { MaxOffset = maxOffset, MinLength = minLength, MaxLength = maxLength};
        }

        internal WeaponStructure.ShieldDefinition Options(float modifier, ShieldType type, float bypassModifier = -1f)
        {
            return new WeaponStructure.ShieldDefinition { Modifier = modifier, Type = type , BypassModifier = bypassModifier };
        }

        internal WeaponStructure.ShapeDefinition Options(WeaponStructure.ShapeDefinition.Shapes shape, double diameter)
        {
            return new WeaponStructure.ShapeDefinition { Shape = shape, Diameter = diameter };
        }

        internal WeaponStructure.Pulse Options(int interval, int pulseChance)
        {
            return new WeaponStructure.Pulse { Interval = interval, PulseChance = pulseChance };
        }

        internal WeaponStructure.EwarFields Options(int duration, bool stackDuration, bool depletable, int maxStacks = int.MaxValue, double triggerRange = 0)
        {
            return new WeaponStructure.EwarFields { Duration = duration, StackDuration = stackDuration, Depletable = depletable, MaxStacks = maxStacks, TriggerRange = triggerRange};
        }

        internal WeaponStructure.TrailDefinition Options(bool enable, string material, int decayTime, Vector4 color, bool back = false, float customWidth = 0, bool useWidthVariance = false, bool useColorFade = false)
        {
            return new WeaponStructure.TrailDefinition { Enable = enable, Material = material, DecayTime = decayTime, Color = color, Back = back, CustomWidth = customWidth, UseWidthVariance = useWidthVariance, UseColorFade = useColorFade};
        }

        internal WeaponStructure.Mines Options(double detectRadius, double deCloakRadius, int fieldTime, bool cloak, bool persist)
        {
            return new WeaponStructure.Mines {  DetectRadius = detectRadius, DeCloakRadius = deCloakRadius, FieldTime = fieldTime, Cloak = cloak, Persist = persist};
        }

        internal WeaponStructure.CustomBlocksDefinition Block(string subTypeId, float modifier)
        {
            return new WeaponStructure.CustomBlocksDefinition { SubTypeId = subTypeId, Modifier = modifier };
        }

        internal WeaponStructure.TracerBaseDefinition Base(bool enable, float length, float width, Vector4 color)
        {
            return new WeaponStructure.TracerBaseDefinition { Enable = enable, Length = length, Width = width, Color = color};
        }

        internal WeaponStructure.AimControlDefinition AimControl(bool trackTargets, bool turretAttached, bool turretController, float rotateRate, float elevateRate, Vector3D offset, bool primaryTracking = false, int minAzimuth = 0, int maxAzimuth = 0, int minElevation = 0, int maxElevation = 0, bool fixedOffset = false, float inventorySize = .384f, bool debug = false, bool lockOnFocus = false)
        {
            return new WeaponStructure.AimControlDefinition { TrackTargets = trackTargets, TurretAttached = turretAttached, TurretController = turretController, RotateRate = rotateRate, ElevateRate = elevateRate, Offset = offset, Debug = debug, MinAzimuth = minAzimuth, MaxAzimuth = maxAzimuth, MinElevation = minElevation, MaxElevation = maxElevation, FixedOffset = fixedOffset, InventorySize = inventorySize, PrimaryTracking = primaryTracking, LockOnFocus = lockOnFocus };
        }

        internal WeaponStructure.UiDefinition Display(bool rateOfFire, bool damageModifier, bool toggleGuidance, bool enableOverload)
        {
            return new WeaponStructure.UiDefinition { RateOfFire = rateOfFire, DamageModifier = damageModifier, ToggleGuidance = toggleGuidance, EnableOverload = enableOverload };
        }

        internal WeaponStructure.TargetingDefinition.BlockTypes[] Priority(params WeaponStructure.TargetingDefinition.BlockTypes[] systems)
        {
            return systems;
        }

        internal WeaponStructure.TargetingDefinition.Threat[] Valid(params WeaponStructure.TargetingDefinition.Threat[] threats)
        {
            return threats;
        }

        internal WeaponStructure.Randomize Random(float start, float end)
        {
            return new WeaponStructure.Randomize { Start = start, End = end };
        }

        internal Vector4 Color(float red, float green, float blue, float alpha)
        {
            return new Vector4(red, green, blue, alpha);
        }

        internal Vector3D Vector(double x, double y, double z)
        {
            return new Vector3D(x, y, z);
        }

        internal WeaponStructure.MountPoint MountPoint(string subTypeId, string aimPartId, string muzzlePartId, string azimuthPartId = "", string elevationPartId = "")
        {
            return new WeaponStructure.MountPoint { SubtypeId = subTypeId, AimPartId = aimPartId, MuzzlePartId = muzzlePartId, AzimuthPartId = azimuthPartId, ElevationPartId = elevationPartId };
        }

        internal WeaponStructure.PartAnimationSetDef.EventTriggers[] Events(params WeaponStructure.PartAnimationSetDef.EventTriggers[] events)
        {
            return events;
        }

        internal WeaponStructure.XYZ Transformation(double X, double Y, double Z)
        {
            return new WeaponStructure.XYZ { x = X, y = Y, z = Z };
        }

        internal Dictionary<WeaponStructure.PartAnimationSetDef.EventTriggers, uint> Delays(uint FiringDelay = 0, uint ReloadingDelay = 0, uint OverheatedDelay = 0, uint TrackingDelay = 0, uint LockedDelay = 0, uint OnDelay = 0, uint OffDelay = 0, uint BurstReloadDelay = 0, uint OutOfAmmoDelay = 0, uint PreFireDelay = 0, uint StopFiringDelay = 0, uint StopTrackingDelay = 0)
        {
            return new Dictionary<WeaponStructure.PartAnimationSetDef.EventTriggers, uint>
            {
                [Firing] = FiringDelay,
                [Reloading] = ReloadingDelay,
                [Overheated] = OverheatedDelay,
                [Tracking] = TrackingDelay,
                [TurnOn] = OnDelay,
                [TurnOff] = OffDelay,
                [BurstReload] = BurstReloadDelay,
                [OutOfAmmo] = OutOfAmmoDelay,
                [PreFire] = PreFireDelay,
                [EmptyOnGameLoad] = 0,
                [StopFiring] = StopFiringDelay,
                [StopTracking] = StopTrackingDelay,
            };
        }

        internal WeaponStructure.WeaponEmissive Emissive(string EmissiveName, bool CycleEmissiveParts, bool LeavePreviousOn, Vector4[] Colors, float IntensityFrom, float IntensityTo, string[] EmissivePartNames)
        {
            return new WeaponStructure.WeaponEmissive()
            {
                EmissiveName = EmissiveName,
                Colors = Colors,
                CycleEmissivesParts = CycleEmissiveParts,
                LeavePreviousOn = LeavePreviousOn,
                EmissivePartNames = EmissivePartNames,
                IntensityRange = new[]{ IntensityFrom, IntensityTo }
            };
        }

        internal string[] Names(params string[] names)
        {
            return names;
        }
    }
}
