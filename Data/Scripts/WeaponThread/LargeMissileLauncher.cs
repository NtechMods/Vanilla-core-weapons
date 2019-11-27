using WeaponCore.Support;
using static WeaponCore.Support.HardPointDefinition.Prediction;
using static WeaponCore.Support.TargetingDefinition.Threat;
using static WeaponCore.Support.TargetingDefinition.BlockTypes;
using static WeaponCore.Support.AmmoTrajectory.GuidanceType;
using static WeaponCore.Support.ShieldDefinition.ShieldType;
using static WeaponCore.Support.Shrapnel.ShrapnelShape;
using static WeaponCore.Support.AreaDamage.AreaEffectType;
using static WeaponCore.Support.ShapeDefinition.Shapes;

namespace WeaponThread
{   // Don't edit above this line
    partial class Weapons
    {
        WeaponDefinition LargeMissileLauncher => new WeaponDefinition
        {
            Assignments = new ModelAssignments
            {
                MountPoints = new[]
                {
                    MountPoint(subTypeId: "LargeMissileLauncher", aimPartId: "MissileTurretBarrels", muzzlePartId: "None", azimuthPartId: "", elevationPartId:""),
                },
                Barrels = Names("muzzle_missile_001", "muzzle_missile_002", "muzzle_missile_003", "muzzle_missile_004", "muzzle_missile_005", "muzzle_missile_006", "muzzle_missile_007",
                        "muzzle_missile_008", "muzzle_missile_009", "muzzle_missile_010", "muzzle_missile_011", "muzzle_missile_012", "muzzle_missile_013", "muzzle_missile_014",
                        "muzzle_missile_015", "muzzle_missile_016", "muzzle_missile_017", "muzzle_missile_018", "muzzle_missile_019"),
                EnableSubPartPhysics = false
            },
            HardPoint = new HardPointDefinition
            {
                WeaponId = "LargeMissileLauncher", // name of weapon in terminal
                AmmoMagazineId = "Missile200mm",
                Block = AimControl(trackTargets: true, turretAttached: true, turretController: true, primaryTracking: true, rotateRate: 0.00f, elevateRate: 0.00f, minAzimuth: 0, maxAzimuth: 0, minElevation: 0, maxElevation: 0, offset: Vector(x: 0, y: 0, z: 0), fixedOffset: false, inventorySize: 0.34f, debug: false),
                DeviateShotAngle = 0.1f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                EnergyCost = 0.00000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                Hybrid = false, //projectile based weapon with energy cost
                EnergyPriority = 0, //  0 = Lowest shares power with shields, 1 = Medium shares power with thrusters and over powers shields, 2 = Highest Does not share power will use all available power until energy requirements met
                RotateBarrelAxis = 0, // 0 = off, 1 = xAxis, 2 = yAxis, 3 = zAxis
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                GridWeaponCap = 0,// 0 = unlimited, the smallest weapon cap assigned to a subTypeId takes priority.
                Ui = Display(rateOfFire: false, damageModifier: false, toggleGuidance: false, enableOverload: false),

                Loading = new AmmoLoading
                {
                    RateOfFire = 120,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 360, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1000000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 1, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                },
            },
            Targeting = new TargetingDefinition
            {
                Threats = Valid(Characters, Projectiles, Grids),
                SubSystems = Priority(Thrust, Utility, Offense, Power, Production, Any), //define block type targeting order
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            DamageScales = new DamageScaleDefinition
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = 0.2f,
                Grids = Options(largeGridModifier: -1f, smallGridModifier: -1f),
                Armor = Options(armor: -1f, light: -1f, heavy: -1f, nonArmor: -1f),
                Shields = Options(modifier: -1f, type: Energy), // Types: Kinetic, Energy, Emp or Bypass

                // ignoreOthers will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = SubTypeIds(false),
            },
            Ammo = new AmmoDefinition
            {
                BaseDamage = 1f,
                Mass = 45f, // in kilograms
                Health = 45f, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                BackKickForce = 0f,
                Shape = Options(shape: Line, diameter: 0), //defines the collision shape of projectile, defaults to visual Line Length
                ObjectsHit = Options(maxObjectsHit: 0, countBlocks: false), // 0 = disabled, value determines max objects (and/or blocks) penetrated per hit
                Shrapnel = Options(baseDamage: 1, fragments: 0, maxTrajectory: 100, noAudioVisual: true, noGuidance: true, shape: HalfMoon),

                AreaEffect = new AreaDamage
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive is keens, Radiant is not.
                    AreaEffectDamage = 500f, // 0 = use spillover from BaseDamage, otherwise use this value.
                    AreaEffectRadius = 4f,
                    Pulse = Options(interval: 30, pulseChance: 0), // interval measured in game ticks (60 == 1 second)
                    Explosions = Options(noVisuals: false, noSound: false, scale: 1, customParticle: "", customSound: ""),
                    Detonation = Options(detonateOnEnd: false, armOnlyOnHit: false, detonationDamage: 0, detonationRadius: 0),
                },
                Beams = new BeamDefinition
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new AmmoTrajectory
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 600f,
                    DesiredSpeed = 200,
                    MaxTrajectory = 800f,
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new Smarts
                    {
                        Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.3f, // controls how sharp the trajectile may turn
                        TrackingDelay = 1200, // Measured in line length units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoints.
                    },
                    Mines = Options(detectRadius: 200, deCloakRadius: 100, fieldTime: 1800, cloak: true, persist: false),
                },
            },



            Graphics = new GraphicDefinition
            {
                ModelName = "\\Models\\Weapons\\Projectile_Missile.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new ParticleDefinition
                {
                    Ammo = new Particle
                    {
                        Name = "MissileSmokeTrail",
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: true, restart: false, distance: 5000, duration: 1, scale: 0.25f)
                    },
                    Hit = new Particle
                    {
                        Name = "",
                        Color = Color(red: 243, green: 190, blue: 51, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 5000, duration: 1, scale: 1.5f, hitPlayChance: 1f),
                        ApplyToShield = true,
                    },
                    Barrel1 = new Particle
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 1, scale: 1f),
                    },
                    Barrel2 = new Particle
                    {
                        Name = "",//Muzzle_Flash_Large
                        Color = Color(red: 255, green: 0, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 1, scale: 1f),
                    },
                },
                Line = new LineDefinition
                {
                    Tracer = Base(enable: false, length: 3f, width: 0.1f, color: Color(red: 0.985f, green: 0.762f, blue: 0.521f, alpha: 1)),
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.05f), // adds random value to default width (negatives shrinks width)
                    Trail = Options(enable: false, material: "ProjectileTrailLine", decayTime: 600, color: Color(red: 8, green: 8, blue: 64, alpha: 8))
                },
            },
            Audio = new AudioDefinition
            {
                HardPoint = new AudioHardPointDefinition
                {
                    FiringSound = "WepShipSmallMissileShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "",
                },

                Ammo = new AudioAmmoDefinition
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 1f,
                    HitPlayShield = true,
                }, // Don't edit below this line
            },
        };
    }
}
