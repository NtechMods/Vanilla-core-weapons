﻿using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;

namespace Scripts
{
    partial class Parts
    {
        // Don't edit above this line
        WeaponDefinition SmallMissileTurret => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallMissileTurret",
                        SpinPartId = "",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "",
                        ElevationPartId = "",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Muzzles = new []
                {
                    "muzzle_missile_001",
					"muzzle_missile_002",
                },
                Ejector = "",
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
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 0, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                PartName = "SmallMissileTurret", // name of weapon in terminal
                DeviateShotAngle = 0.1f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,

                Ui = new UiDef
                {
                    RateOfFire = false,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload =  false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                    SuppressFire = false, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                },
                HardWare = new HardwareDef
                {

                    RotateRate = 0.03f,
                    ElevateRate = 0.03f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -8,
                    MaxElevation = 90,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    FixedOffset = false,
                    InventorySize = 0.360f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = true,
                        TerminalControls = true,
                        AmmoRound = "", // Optional. If specified, the warhead will always use this ammo on detonation rather than the currently selected ammo.
                    },
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                    RestrictionRadius = 0, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 90,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 30, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 100, //heat generated per shot
                    MaxHeat = 1000000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 10, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 2,
                    DelayAfterBurst = 330, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFull = false,
                    GiveUpAfter = false,
                    BarrelSpinRate = 700, // Visual only, 0 disables and uses RateOfFire.
                    MagsToLoad = 2, // Number of physical magazines to consume on reload.
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                    SpinFree = false, // Spin barrel while not firing.
                    StayCharged = false, // Will start recharging whenever power cap is not full.
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "WepShipSmallMissileShot", // subtype name from sbc
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 200,
                            MaxDuration = 1,
                            Scale = 1.0f,
                        },
                    },
                    Effect2 = new ParticleDef
                    {
                        Name = "",//Muzzle_Flash_Large
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
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
                MissileAmmos
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };
    }
}