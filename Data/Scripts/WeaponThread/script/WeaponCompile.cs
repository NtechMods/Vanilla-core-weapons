using System;
using System.Collections.Generic;
using VRageMath;
using static WeaponThread.Session.ShieldDefinition;
using static WeaponThread.Session.EventTriggers;

namespace WeaponThread
{
    partial class Weapons
    {
        internal List<Session.WeaponDefinition> Weapon = new List<Session.WeaponDefinition>();
        internal void ConfigFiles(params Session.WeaponDefinition[] defs)
        {
            foreach (var def in defs) Weapon.Add(def);
        }

        internal Session.WeaponDefinition[] ReturnDefs()
        {
            var weaponDefinitions = new Session.WeaponDefinition[Weapon.Count];
            for (int i = 0; i < Weapon.Count; i++) weaponDefinitions[i] = Weapon[i];
            Weapon.Clear();
            return weaponDefinitions;
        }

        internal Session.ParticleOptions Options(bool loop, bool restart, float distance, float duration, float scale, float hitPlayChance = 1f)
        {
            return new Session.ParticleOptions
            {
                Loop = loop,
                Restart = restart,
                MaxDistance = distance,
                MaxDuration = duration,
                Scale = scale,
                HitPlayChance = hitPlayChance,
            };
        }

        internal Session.Detonate Options(bool detonateOnEnd, bool armOnlyOnHit, float detonationDamage, float detonationRadius)
        {
            return new Session.Detonate
            {
                DetonateOnEnd = detonateOnEnd,
                ArmOnlyOnHit = armOnlyOnHit,
                DetonationDamage = detonationDamage,
                DetonationRadius = detonationRadius,
            };
        }

        internal Session.Explosion Options(bool noVisuals, bool noSound, float scale, string customParticle, string customSound)
        {
            return new Session.Explosion
            {
                NoVisuals = noVisuals,
                NoSound = noSound,
                Scale = scale,
                CustomParticle = customParticle,
                CustomSound = customSound,
            };
        }

        internal Session.GridSizeDefinition Options(float largeGridModifier, float smallGridModifier)
        {
            return new Session.GridSizeDefinition { Large = largeGridModifier, Small = smallGridModifier };
        }

        internal Session.ObjectsHit Options(int maxObjectsHit, bool countBlocks)
        {
            return new Session.ObjectsHit { MaxObjectsHit = maxObjectsHit, CountBlocks = countBlocks };
        }

        internal Session.Shrapnel Options(float baseDamage, int fragments, float maxTrajectory, bool noAudioVisual, bool noGuidance, Session.Shrapnel.ShrapnelShape shape)
        {
            return new Session.Shrapnel { BaseDamage = baseDamage, Fragments = fragments, MaxTrajectory = maxTrajectory, NoAudioVisual = noAudioVisual, NoGuidance = noGuidance, Shape = shape};
        }

        internal Session.CustomScalesDefinition SubTypeIds(bool ignoreOthers, params Session.CustomBlocksDefinition[] customDefScale)
        {
            return new Session.CustomScalesDefinition {IgnoreAllOthers = ignoreOthers, Types = customDefScale};
        }

        internal Session.ArmorDefinition Options(float armor, float light, float heavy, float nonArmor)
        {
            return new Session.ArmorDefinition { Armor = armor, Light = light, Heavy = heavy, NonArmor = nonArmor };
        }

        internal Session.OffsetEffect Options(double maxOffset, double minLength, double maxLength)
        {
            return new Session.OffsetEffect { MaxOffset = maxOffset, MinLength = minLength, MaxLength = maxLength};
        }

        internal Session.ShieldDefinition Options(float modifier, ShieldType type)
        {
            return new Session.ShieldDefinition { Modifier = modifier, Type = type };
        }

        internal Session.ShapeDefinition Options(Session.ShapeDefinition.Shapes shape, double diameter)
        {
            return new Session.ShapeDefinition { Shape = shape, Diameter = diameter };
        }

        internal Session.Pulse Options(int interval, int pulseChance)
        {
            return new Session.Pulse { Interval = interval, PulseChance = pulseChance };
        }

        internal Session.EwarFields Options(int duration, bool stackDuration, bool depletable)
        {
            return new Session.EwarFields { Duration = duration, StackDuration = stackDuration, Depletable = depletable};
        }

        internal Session.TrailDefinition Options(bool enable, string material, int decayTime, Vector4 color)
        {
            return new Session.TrailDefinition { Enable = enable, Material = material, DecayTime = decayTime, Color = color };
        }

        internal Session.Mines Options(double detectRadius, double deCloakRadius, int fieldTime, bool cloak, bool persist)
        {
            return new Session.Mines {  DetectRadius = detectRadius, DeCloakRadius = deCloakRadius, FieldTime = fieldTime, Cloak = cloak, Persist = persist};
        }

        internal Session.CustomBlocksDefinition Block(string subTypeId, float modifier)
        {
            return new Session.CustomBlocksDefinition { SubTypeId = subTypeId, Modifier = modifier };
        }

        internal Session.TracerBaseDefinition Base(bool enable, float length, float width, Vector4 color)
        {
            return new Session.TracerBaseDefinition { Enable = enable, Length = length, Width = width, Color = color};
        }

        internal Session.AimControlDefinition AimControl(bool trackTargets, bool turretAttached, bool turretController, float rotateRate, float elevateRate, Vector3D offset, bool primaryTracking = false, int minAzimuth = 0, int maxAzimuth = 0, int minElevation = 0, int maxElevation = 0, bool fixedOffset = false, float inventorySize = .384f, bool debug = false)
        {
            return new Session.AimControlDefinition { TrackTargets = trackTargets, TurretAttached = turretAttached, TurretController = turretController, RotateRate = rotateRate, ElevateRate = elevateRate, Offset = offset, Debug = debug, MinAzimuth = minAzimuth, MaxAzimuth = maxAzimuth, MinElevation = minElevation, MaxElevation = maxElevation, FixedOffset = fixedOffset, InventorySize = inventorySize, PrimaryTracking = primaryTracking };
        }

        internal Session.UiDefinition Display(bool rateOfFire, bool damageModifier, bool toggleGuidance, bool enableOverload)
        {
            return new Session.UiDefinition { RateOfFire = rateOfFire, DamageModifier = damageModifier, ToggleGuidance = toggleGuidance, EnableOverload = enableOverload };
        }

        internal Session.TargetingDefinition.BlockTypes[] Priority(params Session.TargetingDefinition.BlockTypes[] systems)
        {
            return systems;
        }

        internal Session.TargetingDefinition.Threat[] Valid(params Session.TargetingDefinition.Threat[] threats)
        {
            return threats;
        }

        internal Session.Randomize Random(float start, float end)
        {
            return new Session.Randomize { Start = start, End = end };
        }

        internal Vector4 Color(float red, float green, float blue, float alpha)
        {
            return new Vector4(red, green, blue, alpha);
        }

        internal Vector3D Vector(double x, double y, double z)
        {
            return new Vector3D(x, y, z);
        }

        internal Session.MountPoint MountPoint(string subTypeId, string aimPartId, string muzzlePartId, string azimuthPartId = "", string elevationPartId = "")
        {
            return new Session.MountPoint { SubtypeId = subTypeId, AimPartId = aimPartId, MuzzlePartId = muzzlePartId, AzimuthPartId = azimuthPartId, ElevationPartId = elevationPartId };
        }

        internal Session.EventTriggers[] Events(params Session.EventTriggers[] events)
        {
            return events;
        }

        internal Session.XYZ Transformation(double X, double Y, double Z)
        {
            return new Session.XYZ { x = X, y = Y, z = Z };
        }

        internal Dictionary<Session.EventTriggers, uint> Delays(uint FiringDelay = 0, uint ReloadingDelay = 0, uint OverheatedDelay = 0, uint TrackingDelay = 0, uint LockedDelay =0, uint OnDelay = 0, uint OffDelay = 0, uint BurstReloadDelay = 0, uint OutOfAmmoDelay = 0, uint PreFireDelay = 0)
        {
            return new Dictionary<Session.EventTriggers, uint>
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
            };
        }

        internal Session.WeaponEmissive Emissive(string EmissiveName, bool CycleEmissiveParts, bool LeavePreviousOn, Vector4[] Colors, float IntensityFrom, float IntensityTo, string[] EmissivePartNames)
        {
            return new Session.WeaponEmissive()
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
