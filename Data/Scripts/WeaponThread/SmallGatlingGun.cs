﻿using VRageMath;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.ModelAssignmentsDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.Prediction;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.BlockTypes;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.Threat;

namespace WeaponThread
{   // Don't edit above this line
    partial class Weapons
    {
        WeaponDefinition SmallGatlingGun => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingGun",
                        AimPartId = "Barrel",
                        MuzzlePartId = "Barrel",
                        ElevationPartId = "None",
                        AzimuthPartId = "None"
                    },

                },
                Barrels = new []
                {
                    "muzzle_projectile",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatlingGun", // name of weapon in terminal
                DeviateShotAngle = 0.4f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Basic, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = false,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload =  false,
                },
                Ai = new AiDef
                {
                    TrackTargets = false,
                    TurretAttached = false,
                    TurretController = false,
                    PrimaryTracking = false,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.02f,
                    ElevateRate = 0.00f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = false,
                    InventorySize = 0.064f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 3,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 700,
                    BarrelSpinRate = 700, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 240, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, //heat generated per shot
                    MaxHeat = 10000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 9900, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "WepTurretInteriorFire", // subtype name from sbc
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef
                    {
                        Name = "Smoke_LargeGunShot", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 200,
                            MaxDuration = 1,
                            Scale = 1.0f,
                        },
                    },
                    Barrel2 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_Large_Core",//Muzzle_Flash_Large
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100,
                            MaxDuration = 1,
                            Scale = 1f,
                        },
                    },
                },
            },

			Ammos = new [] {
                GatlingAmmoType2
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };
    }
}