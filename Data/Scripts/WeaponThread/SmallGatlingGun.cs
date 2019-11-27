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
        WeaponDefinition SmallGatlingGun => new WeaponDefinition
        {
            Assignments = new ModelAssignments
            {
                MountPoints = new[]
                {
                    MountPoint(subTypeId: "SmallGatlingGun", aimPartId:"MissileTurretBarrels", muzzlePartId: "gunBarrel", azimuthPartId: "", elevationPartId:""),
                },
                Barrels = Names("muzzle_projectile", "muzzle_projectile001", "muzzle_projectile002", "muzzle_projectile003", "muzzle_projectile004", "muzzle_projectile005"),
                EnableSubPartPhysics = false
            },
            HardPoint = new HardPointDefinition
            {
                WeaponId = "SmallGatlingGun", // name of weapon in terminal
                AmmoMagazineId = "NATO_25x184mm",
                Block = AimControl(trackTargets: true, turretAttached: true, turretController: true, primaryTracking: true, rotateRate: 0.00f, elevateRate: 0.00f, minAzimuth: 0, maxAzimuth: 0, minElevation: 0, maxElevation: 0, offset: Vector(x: 0, y: 0, z: 0), fixedOffset: false, inventorySize: 0.34f, debug: false),
                DeviateShotAngle = 0.4f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                EnergyCost = 0.00000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                Hybrid = false, //projectile based weapon with energy cost
                EnergyPriority = 0, //  0 = Lowest shares power with shields, 1 = Medium shares power with thrusters and over powers shields, 2 = Highest Does not share power will use all available power until energy requirements met
                RotateBarrelAxis = 3, // 0 = off, 1 = xAxis, 2 = yAxis, 3 = zAxis
                AimLeadingPrediction = Basic, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                GridWeaponCap = 0,// 0 = unlimited, the smallest weapon cap assigned to a subTypeId takes priority.
                Ui = Display(rateOfFire: false, damageModifier: false, toggleGuidance: false, enableOverload: false),

                Loading = new AmmoLoading
                {
                    RateOfFire = 700,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 240, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1000000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 1000000, //amount of heat lost per second
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
                Shields = Options(modifier: -1f, type: Kinetic), // Types: Kinetic, Energy, Emp or Bypass

                // ignoreOthers will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = SubTypeIds(false),
            },
            Ammo = new AmmoDefinition
            {
                BaseDamage = 150f,
                Mass = 1f, // in kilograms
                Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                BackKickForce = 0f,
                Shape = Options(shape: Line, diameter: 0), //defines the collision shape of projectile, defaults to visual Line Length
                ObjectsHit = Options(maxObjectsHit: 0, countBlocks: false), // 0 = disabled, value determines max objects (and/or blocks) penetrated per hit
                Shrapnel = Options(baseDamage: 1, fragments: 0, maxTrajectory: 100, noAudioVisual: true, noGuidance: true, shape: HalfMoon),

                AreaEffect = new AreaDamage
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive is keens, Radiant is not.
                    AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
                    AreaEffectRadius = 70f,
                    Pulse = Options(interval: 30, pulseChance: 100), // interval measured in game ticks (60 == 1 second)
                    Explosions = Options(noVisuals: false, noSound: false, scale: 2, customParticle: "", customSound: ""),
                    Detonation = Options(detonateOnEnd: false, armOnlyOnHit: false, detonationDamage: 0, detonationRadius: 70),
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
                    AccelPerSec = 0f,
                    DesiredSpeed = 400f,
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
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new ParticleDefinition
                {
                    Ammo = new Particle
                    {
                        Name = "",
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: true, restart: false, distance: 5000, duration: 1, scale: 1)
                    },
                    Hit = new Particle
                    {
                        Name = "MaterialHit_Metal",
                        Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 5000, duration: 1, scale: 1.0f, hitPlayChance: 0.1f),
                    },
                    Barrel1 = new Particle
                    {
                        Name = "Smoke_LargeGunShot", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 1, scale: 1f),
                    },
                    Barrel2 = new Particle
                    {
                        Name = "Muzzle_Flash_Large",//Muzzle_Flash_Large
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = Options(loop: false, restart: false, distance: 50, duration: 1, scale: 1f),
                    },
                },
                Line = new LineDefinition
                {
                    Tracer = Base(enable: true, length: 3f, width: 0.05f, color: Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1)),
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Trail = Options(enable: false, material: "ProjectileTrailLine", decayTime: 600, color: Color(red: 8, green: 8, blue: 64, alpha: 8))
                },
            },
            Audio = new AudioDefinition
            {
                HardPoint = new AudioHardPointDefinition
                {
                    FiringSound = "ArcWepShipGatlingShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },

                Ammo = new AudioAmmoDefinition
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = false,
                }, // Don't edit below this line
            },
        };
    }
}
