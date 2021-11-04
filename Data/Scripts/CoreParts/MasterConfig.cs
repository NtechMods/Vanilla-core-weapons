namespace Scripts
{
    partial class Parts
    {
        internal Parts()
        {
            // file convention: Name.cs - See Example.cs file for weapon property details.
            //
            // Enable your config files using the follow syntax, don't include the ".cs" extension:
            // ConfigFiles(Your1stConfigFile, Your2ndConfigFile, Your3rdConfigFile);

            PartDefinitions(LargeGatlingTurret,
                        LargeMissileTurret,
                        SmallMissileTurret,
                        LargeMissileLauncher,
                        SmallGatlingGun,
                        LargeInteriorTurret,
                        SmallMissileLauncher,
                        SmallGatlingTurret,
                        SmallRocketLauncherReload);
            ArmorDefinitions();
            SupportDefinitions();
            UpgradeDefinitions();
        }
    }
}

