using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ProtoBuf;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRageMath;
using VRageMath;

namespace WeaponThread
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate, int.MaxValue)]
    public class Session : MySessionComponentBase
    {
        internal WeaponDefinition[] WeaponDefinitions;

        public override void LoadData()
        {
            //Log.Init("weapon.log");
            //Log.CleanLine($"Logging Started at: {DateTime.Now:MM-dd-yy_HH-mm-ss-fff}");
            MyAPIGateway.Utilities.RegisterMessageHandler(7772, Handler);
            Init();
            SendModMessage();
        }

        protected override void UnloadData()
        {
            //Log.CleanLine($"Logging stopped at: {DateTime.Now:MM-dd-yy_HH-mm-ss-fff}");
            //Log.Close();
            MyAPIGateway.Utilities.UnregisterMessageHandler(7772, Handler);
            Array.Clear(Storage, 0, Storage.Length);
            Storage = null;
        }

        void Handler(object o)
        {
            if (o == null) SendModMessage();
        }

        void SendModMessage()
        {
            MyAPIGateway.Utilities.SendModMessage(7771, Storage);
        }

        internal byte[] Storage;

        internal void Init()
        {
            var weapons = new Weapons();
            WeaponDefinitions = weapons.ReturnDefs();
            for (int i = 0; i < WeaponDefinitions.Length; i++)
                WeaponDefinitions[i].ModPath = ModContext.ModPath;
            Storage = MyAPIGateway.Utilities.SerializeToBinary(WeaponDefinitions);
            Array.Clear(WeaponDefinitions, 0, WeaponDefinitions.Length);
            WeaponDefinitions = null;
        }

        public enum EventTriggers
        {
            Reloading,
            Firing,
            Tracking,
            Overheated,
            TurnOn,
            TurnOff,
            BurstReload,
            OutOfAmmo,
            PreFire,
            EmptyOnGameLoad,
            StopFiring,
            StopTracking
        }

        [ProtoContract]
        public struct WeaponDefinition
        {
            [ProtoMember(1)] internal HardPointDefinition HardPoint;
            [ProtoMember(2)] internal AmmoDefinition Ammo;
            [ProtoMember(3)] internal GraphicDefinition Graphics;
            [ProtoMember(4)] internal AudioDefinition Audio;
            [ProtoMember(5)] internal ModelAssignments Assignments;
            [ProtoMember(6)] internal DamageScaleDefinition DamageScales;
            [ProtoMember(7)] internal TargetingDefinition Targeting;
            [ProtoMember(8)] internal string ModPath;
            [ProtoMember(9)] internal AnimationDefinition Animations;
        }


        [ProtoContract]
        public struct ModelAssignments
        {
            [ProtoMember(1)] internal MountPoint[] MountPoints;
            [ProtoMember(2)] internal string[] Barrels;
            [ProtoMember(3)] internal bool EnableSubPartPhysics;
        }

        [ProtoContract]
        public struct UiDefinition
        {
            [ProtoMember(1)] internal bool RateOfFire;
            [ProtoMember(2)] internal bool DamageModifier;
            [ProtoMember(3)] internal bool ToggleGuidance;
            [ProtoMember(4)] internal bool EnableOverload;
        }

        [ProtoContract]
        public struct HardPointDefinition
        {
            public enum Prediction
            {
                Off,
                Basic,
                Accurate,
                Advanced,
            }

            [ProtoMember(1)] internal string WeaponId;
            [ProtoMember(2)] internal string AmmoMagazineId;
            [ProtoMember(3)] internal bool Hybrid;
            [ProtoMember(4)] internal int DelayCeaseFire;
            [ProtoMember(5)] internal int RotateBarrelAxis;
            [ProtoMember(6)] internal int EnergyPriority;
            [ProtoMember(7)] internal int GridWeaponCap;
            [ProtoMember(8)] internal float DeviateShotAngle;
            [ProtoMember(9)] internal float EnergyCost;
            [ProtoMember(10)] internal double AimingTolerance;
            [ProtoMember(11)] internal Prediction AimLeadingPrediction;
            [ProtoMember(12)] internal AmmoLoading Loading;
            [ProtoMember(13)] internal AimControlDefinition Block;
            [ProtoMember(14)] internal UiDefinition Ui;
            [ProtoMember(15)] internal bool MuzzleCheck;
        }

        [ProtoContract]
        public struct AimControlDefinition
        {
            [ProtoMember(1)] internal bool TrackTargets;
            [ProtoMember(2)] internal bool TurretAttached;
            [ProtoMember(3)] internal bool TurretController;
            [ProtoMember(4)] internal float RotateRate;
            [ProtoMember(5)] internal float ElevateRate;
            [ProtoMember(6)] internal Vector3D Offset;
            [ProtoMember(7)] internal bool FixedOffset;
            [ProtoMember(8)] internal bool Debug;
            [ProtoMember(9)] internal int MaxAzimuth;
            [ProtoMember(10)] internal int MinAzimuth;
            [ProtoMember(11)] internal int MaxElevation;
            [ProtoMember(12)] internal int MinElevation;
            [ProtoMember(13)] internal bool PrimaryTracking;
            [ProtoMember(14)] internal float InventorySize;
        }

        [ProtoContract]
        public struct AmmoLoading
        {
            [ProtoMember(1)] internal int ReloadTime;
            [ProtoMember(2)] internal int RateOfFire;
            [ProtoMember(3)] internal int BarrelsPerShot;
            [ProtoMember(4)] internal int SkipBarrels;
            [ProtoMember(5)] internal int TrajectilesPerBarrel;
            [ProtoMember(6)] internal int HeatPerShot;
            [ProtoMember(7)] internal int MaxHeat;
            [ProtoMember(8)] internal int HeatSinkRate;
            [ProtoMember(9)] internal float Cooldown;
            [ProtoMember(10)] internal int DelayUntilFire;
            [ProtoMember(11)] internal int ShotsInBurst;
            [ProtoMember(12)] internal int DelayAfterBurst;
            [ProtoMember(13)] internal bool DegradeRof;
            [ProtoMember(14)] internal int BarrelSpinRate;
        }

        [ProtoContract]
        public struct MountPoint
        {
            [ProtoMember(1)] internal string SubtypeId;
            [ProtoMember(2)] internal string AimPartId;
            [ProtoMember(3)] internal string MuzzlePartId;
            [ProtoMember(4)] internal string AzimuthPartId;
            [ProtoMember(5)] internal string ElevationPartId;
        }

        [ProtoContract]
        public struct TargetingDefinition
        {
            public enum Threat
            {
                Projectiles,
                Characters,
                Grids,
                Neutrals,
                Meteors,
                Other
            }

            public enum BlockTypes
            {
                Any,
                Offense,
                Utility,
                Power,
                Production,
                Thrust,
                Jumping,
                Steering
            }

            [ProtoMember(1)] internal int TopTargets;
            [ProtoMember(2)] internal int TopBlocks;
            [ProtoMember(3)] internal double StopTrackingSpeed;
            [ProtoMember(4)] internal float MinimumDiameter;
            [ProtoMember(5)] internal float MaximumDiameter;
            [ProtoMember(6)] internal bool ClosestFirst;
            [ProtoMember(7)] internal BlockTypes[] SubSystems;
            [ProtoMember(8)] internal Threat[] Threats;
        }

        [ProtoContract]
        public struct AmmoDefinition
        {
            [ProtoMember(1)] internal float BaseDamage;
            [ProtoMember(2)] internal float Mass;
            [ProtoMember(3)] internal float Health;
            [ProtoMember(4)] internal float BackKickForce;
            [ProtoMember(5)] internal ShapeDefinition Shape;
            [ProtoMember(6)] internal ObjectsHit ObjectsHit;
            [ProtoMember(7)] internal AmmoTrajectory Trajectory;
            [ProtoMember(8)] internal AreaDamage AreaEffect;
            [ProtoMember(9)] internal BeamDefinition Beams;
            [ProtoMember(10)] internal Shrapnel Shrapnel;
        }

        [ProtoContract]
        public struct ShapeDefinition
        {
            public enum Shapes
            {
                Line,
                Sphere,
            }

            [ProtoMember(1)] internal Shapes Shape;
            [ProtoMember(2)] internal double Diameter;
        }

        [ProtoContract]
        public struct ObjectsHit
        {
            [ProtoMember(1)] internal int MaxObjectsHit;
            [ProtoMember(2)] internal bool CountBlocks;
        }

        [ProtoContract]
        public struct BeamDefinition
        {
            [ProtoMember(1)] internal bool Enable;
            [ProtoMember(2)] internal bool ConvergeBeams;
            [ProtoMember(3)] internal bool VirtualBeams;
            [ProtoMember(4)] internal bool RotateRealBeam;
            [ProtoMember(5)] internal bool OneParticle;
        }

        [ProtoContract]
        public struct AreaDamage
        {
            public enum AreaEffectType
            {
                Disabled,
                Explosive,
                Radiant,
                AntiSmart,
                JumpNullField,
                EnergySinkField,
                AnchorField,
                EmpField,
                OffenseField,
                NavField,
                DotField,
            }

            [ProtoMember(1)] internal double AreaEffectRadius;
            [ProtoMember(2)] internal float AreaEffectDamage;
            [ProtoMember(3)] internal Pulse Pulse;
            [ProtoMember(4)] internal AreaEffectType AreaEffect;
            [ProtoMember(5)] internal Detonate Detonation;
            [ProtoMember(6)] internal Explosion Explosions;
            [ProtoMember(7)] internal EwarFields EwarFields;
        }

        [ProtoContract]
        public struct Pulse
        {
            [ProtoMember(1)] internal int Interval;
            [ProtoMember(2)] internal int PulseChance;
        }

        [ProtoContract]
        public struct EwarFields
        {
            [ProtoMember(1)] internal int Duration;
            [ProtoMember(2)] internal bool StackDuration;
            [ProtoMember(3)] internal bool Depletable;
        }

        [ProtoContract]
        public struct Detonate
        {
            [ProtoMember(1)] internal bool DetonateOnEnd;
            [ProtoMember(2)] internal bool ArmOnlyOnHit;
            [ProtoMember(3)] internal float DetonationRadius;
            [ProtoMember(4)] internal float DetonationDamage;
        }

        [ProtoContract]
        public struct Explosion
        {
            [ProtoMember(1)] internal bool NoVisuals;
            [ProtoMember(2)] internal bool NoSound;
            [ProtoMember(3)] internal float Scale;
            [ProtoMember(4)] internal string CustomParticle;
            [ProtoMember(5)] internal string CustomSound;
        }

        [ProtoContract]
        public struct AmmoTrajectory
        {
            internal enum GuidanceType
            {
                None,
                Remote,
                TravelTo,
                Smart,
                DetectTravelTo,
                DetectSmart,
                DetectFixed,
            }

            [ProtoMember(1)] internal float MaxTrajectory;
            [ProtoMember(2)] internal float AccelPerSec;
            [ProtoMember(3)] internal float DesiredSpeed;
            [ProtoMember(4)] internal float TargetLossDegree;
            [ProtoMember(5)] internal int TargetLossTime;
            [ProtoMember(6)] internal int FieldTime;
            [ProtoMember(7)] internal Randomize SpeedVariance;
            [ProtoMember(8)] internal Randomize RangeVariance;
            [ProtoMember(9)] internal GuidanceType Guidance;
            [ProtoMember(10)] internal Smarts Smarts;
            [ProtoMember(11)] internal Mines Mines;
        }

        [ProtoContract]
        public struct Smarts
        {
            [ProtoMember(1)] internal double Inaccuracy;
            [ProtoMember(2)] internal double Aggressiveness;
            [ProtoMember(3)] internal double MaxLateralThrust;
            [ProtoMember(4)] internal double TrackingDelay;
            [ProtoMember(5)] internal int MaxChaseTime;
            [ProtoMember(6)] internal bool OverideTarget;
        }

        [ProtoContract]
        public struct Mines
        {
            [ProtoMember(1)] internal double DetectRadius;
            [ProtoMember(2)] internal double DeCloakRadius;
            [ProtoMember(3)] internal int FieldTime;
            [ProtoMember(4)] internal bool Cloak;
            [ProtoMember(5)] internal bool Persist;
        }

        [ProtoContract]
        public struct Shrapnel
        {
            internal enum ShrapnelShape
            {
                Cone,
                HalfMoon,
                FullMoon,
            }

            [ProtoMember(1)] internal float BaseDamage;
            [ProtoMember(2)] internal int Fragments;
            [ProtoMember(3)] internal float MaxTrajectory;
            [ProtoMember(4)] internal bool NoAudioVisual;
            [ProtoMember(5)] internal bool NoGuidance;
            [ProtoMember(6)] internal ShrapnelShape Shape;
        }

        [ProtoContract]
        public struct GraphicDefinition
        {
            [ProtoMember(1)] internal bool ShieldHitDraw;
            [ProtoMember(2)] internal float VisualProbability;
            [ProtoMember(3)] internal string ModelName;
            [ProtoMember(4)] internal ParticleDefinition Particles;
            [ProtoMember(5)] internal LineDefinition Line;
        }

        [ProtoContract]
        public struct ParticleDefinition
        {
            [ProtoMember(1)] internal Particle Ammo;
            [ProtoMember(2)] internal Particle Hit;
            [ProtoMember(3)] internal Particle Barrel1;
            [ProtoMember(4)] internal Particle Barrel2;
        }

        [ProtoContract]
        public struct Particle
        {
            [ProtoMember(1)] internal string Name;
            [ProtoMember(2)] internal Vector4 Color;
            [ProtoMember(3)] internal Vector3D Offset;
            [ProtoMember(4)] internal ParticleOptions Extras;
            [ProtoMember(5)] internal bool ApplyToShield;
            [ProtoMember(6)] internal bool ShrinkByDistance;
        }

        [ProtoContract]
        public struct ParticleOptions
        {
            [ProtoMember(1)] internal float Scale;
            [ProtoMember(2)] internal float MaxDistance;
            [ProtoMember(3)] internal float MaxDuration;
            [ProtoMember(4)] internal bool Loop;
            [ProtoMember(5)] internal bool Restart;
            [ProtoMember(6)] internal float HitPlayChance;
        }

        [ProtoContract]
        public struct LineDefinition
        {
            [ProtoMember(1)] internal TracerBaseDefinition Tracer;
            [ProtoMember(2)] internal string TracerMaterial;
            [ProtoMember(3)] internal Randomize ColorVariance;
            [ProtoMember(4)] internal Randomize WidthVariance;
            [ProtoMember(5)] internal TrailDefinition Trail;
            [ProtoMember(6)] internal OffsetEffect OffsetEffect;
        }

        [ProtoContract]
        public struct OffsetEffect
        {
            [ProtoMember(1)] internal double MaxOffset;
            [ProtoMember(2)] internal double MinLength;
            [ProtoMember(3)] internal double MaxLength;
        }

        [ProtoContract]
        public struct TracerBaseDefinition
        {
            [ProtoMember(1)] internal bool Enable;
            [ProtoMember(2)] internal float Length;
            [ProtoMember(3)] internal float Width;
            [ProtoMember(4)] internal Vector4 Color;
        }

        [ProtoContract]
        public struct TrailDefinition
        {
            [ProtoMember(1)] internal bool Enable;
            [ProtoMember(2)] internal string Material;
            [ProtoMember(3)] internal int DecayTime;
            [ProtoMember(4)] internal Vector4 Color;
            [ProtoMember(5)] internal bool Back;
            [ProtoMember(6)] internal float CustomWidth;
            [ProtoMember(7)] internal bool UseWidthVariance;
            [ProtoMember(8)] internal bool UseColorFade;
        }

        [ProtoContract]
        public struct WeaponEmissive
        {
            [ProtoMember(1)] internal string EmissiveName;
            [ProtoMember(2)] internal string[] EmissivePartNames;
            [ProtoMember(3)] internal bool CycleEmissivesParts;
            [ProtoMember(4)] internal bool LeavePreviousOn;
            [ProtoMember(5)] internal Vector4[] Colors;
            [ProtoMember(6)] internal float[] IntensityRange;
        }

        [ProtoContract]
        public struct Randomize
        {
            [ProtoMember(1)] internal float Start;
            [ProtoMember(2)] internal float End;
        }

        [ProtoContract]
        public struct AudioDefinition
        {
            [ProtoMember(1)] internal AudioHardPointDefinition HardPoint;
            [ProtoMember(2)] internal AudioAmmoDefinition Ammo;
        }

        [ProtoContract]
        public struct AudioAmmoDefinition
        {
            [ProtoMember(1)] internal string TravelSound;
            [ProtoMember(2)] internal string HitSound;
            [ProtoMember(3)] internal float HitPlayChance;
            [ProtoMember(4)] internal bool HitPlayShield;
        }

        [ProtoContract]
        public struct AudioHardPointDefinition
        {
            [ProtoMember(1)] internal string ReloadSound;
            [ProtoMember(2)] internal string NoAmmoSound;
            [ProtoMember(3)] internal string HardPointRotationSound;
            [ProtoMember(4)] internal string BarrelRotationSound;
            [ProtoMember(5)] internal string FiringSound;
            [ProtoMember(6)] internal bool FiringSoundPerShot;
        }

        [ProtoContract]
        public struct DamageScaleDefinition
        {
            [ProtoMember(1)] internal GridSizeDefinition Grids;
            [ProtoMember(2)] internal ArmorDefinition Armor;
            [ProtoMember(3)] internal float MaxIntegrity;
            [ProtoMember(4)] internal bool DamageVoxels;
            [ProtoMember(5)] internal ShieldDefinition Shields;
            [ProtoMember(6)] internal float Characters;
            [ProtoMember(7)] internal CustomScalesDefinition Custom;
            [ProtoMember(8)] internal bool SelfDamage;
        }

        [ProtoContract]
        public struct GridSizeDefinition
        {
            [ProtoMember(1)] internal float Large;
            [ProtoMember(2)] internal float Small;
        }

        [ProtoContract]
        public struct ArmorDefinition
        {
            [ProtoMember(1)] internal float Armor;
            [ProtoMember(2)] internal float Heavy;
            [ProtoMember(3)] internal float Light;
            [ProtoMember(4)] internal float NonArmor;
        }

        [ProtoContract]
        public struct CustomBlocksDefinition
        {
            [ProtoMember(1)] internal string SubTypeId;
            [ProtoMember(2)] internal float Modifier;
        }

        [ProtoContract]
        public struct CustomScalesDefinition
        {
            [ProtoMember(1)] internal CustomBlocksDefinition[] Types;
            [ProtoMember(2)] internal bool IgnoreAllOthers;
        }

        [ProtoContract]
        public struct ShieldDefinition
        {
            internal enum ShieldType
            {
                Heal,
                Bypass,
                Emp,
                Energy,
                Kinetic
            }

            [ProtoMember(1)] internal float Modifier;
            [ProtoMember(2)] internal ShieldType Type;
        }

        [ProtoContract]
        public struct AnimationDefinition
        {
            [ProtoMember(1)] internal PartAnimationSetDef[] WeaponAnimationSets;
            [ProtoMember(2)] internal WeaponEmissive[] Emissives;
        }

        [ProtoContract(IgnoreListHandling = true)]
        public struct PartAnimationSetDef
        {
            [ProtoMember(1)] internal string[] SubpartId;
            [ProtoMember(2)] internal string BarrelId;
            [ProtoMember(3)] internal uint StartupFireDelay;
            [ProtoMember(4)] internal Dictionary<EventTriggers, uint> AnimationDelays;
            [ProtoMember(5)] internal EventTriggers[] Reverse;
            [ProtoMember(6)] internal EventTriggers[] Loop;
            [ProtoMember(7)] internal Dictionary<EventTriggers, RelMove[]> EventMoveSets;
            [ProtoMember(8)] internal EventTriggers[] TriggerOnce;
            [ProtoMember(9)] internal EventTriggers[] ResetEmissives;

        }

        [ProtoContract]
        internal struct RelMove
        {
            public enum MoveType
            {
                Linear,
                ExpoDecay,
                ExpoGrowth,
                Delay,
                Show, //instant or fade
                Hide, //instant or fade
            }

            [ProtoMember(1)] internal MoveType MovementType;
            [ProtoMember(2)] internal XYZ[] LinearPoints;
            [ProtoMember(3)] internal XYZ Rotation;
            [ProtoMember(4)] internal XYZ RotAroundCenter;
            [ProtoMember(5)] internal uint TicksToMove;
            [ProtoMember(6)] internal string CenterEmpty;
            [ProtoMember(7)] internal bool Fade;
            [ProtoMember(8)] internal string EmissiveName;
        }

        [ProtoContract]
        internal struct XYZ
        {
            [ProtoMember(1)] internal double x;
            [ProtoMember(2)] internal double y;
            [ProtoMember(3)] internal double z;
        }

        public class Log
        {
            private static Log _instance = null;
            private TextWriter _file = null;

            private static Log GetInstance()
            {
                return _instance ?? (_instance = new Log());
            }

            public static void Init(string name)
            {
                if (GetInstance()._file == null)
                    GetInstance()._file = MyAPIGateway.Utilities.WriteFileInLocalStorage(name, typeof(Log));
            }

            public static void CleanLine(string text)
            {
                if (GetInstance()._file == null) return;
                GetInstance()._file.WriteLine(text);
                GetInstance()._file.Flush();
            }

            public static void Close()
            {
                if (GetInstance()._file == null) return;
                GetInstance()._file.Flush();
                GetInstance()._file.Close();
            }
        }
    }
}

