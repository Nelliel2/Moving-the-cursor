using MovingCursor.MovementModes;
using System;
using System.Configuration;
using System.Dynamic;

namespace MovingCursor
{
    internal enum CursorMovementMode
    {
        Random,
        Circle,
        LockScreen,
        Infinity,
        Spiral,
        Triangle,
        Quadrilateral,
        Polygon,
        RandomPoint
    }

    internal static class ConfigReader
    {
        private static CursorMovementMode GetCursorMovementMode()
        {
            var exceptionMessage = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var modeString = appSettings.Get("mode");

                if (modeString == "Random")
                {
                    var random = new Random();
                    var randomNumber = random.Next(1, Enum.GetNames(typeof(CursorMovementMode)).Length);
                    modeString = Enum.GetName(typeof(CursorMovementMode), randomNumber);
                }

                if (Enum.TryParse<CursorMovementMode>(modeString, true, out CursorMovementMode parsedMode)
                    && Enum.IsDefined(typeof(CursorMovementMode), parsedMode))
                {
                    return parsedMode;
                }

                exceptionMessage = $"Мод {modeString} не существует в перечислении.";

                throw new Exception(exceptionMessage);
            }
            catch (Exception e)
            {
                exceptionMessage = exceptionMessage == "" ? "Ошибка при чтении MovingCursor.dll.config." + e.Message : exceptionMessage;
                throw new Exception(exceptionMessage);
            }
        }

        public static dynamic ReadConfig()
        {
            dynamic config = new ExpandoObject();
            config.mode = GetCursorMovementMode();


            switch (config.mode)
            {
                case CursorMovementMode.Circle:
                    var circleModeSection = ConfigurationManager.GetSection("CircleMode") as CircleModeSection;

                    if (circleModeSection != null)
                    {
                        config.intervalMs = circleModeSection.IntervalMs;
                        config.step = circleModeSection.Step;
                        config.radius = circleModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.LockScreen:
                    var lockScreenModeSection = ConfigurationManager.GetSection("LockScreenMode") as LockScreenModeSection;

                    if (lockScreenModeSection != null)
                    {
                        config.intervalMs = lockScreenModeSection.IntervalMs;
                    }
                    break;
                case CursorMovementMode.Infinity:
                    var infinityModeSection = ConfigurationManager.GetSection("InfinityMode") as InfinityModeSection;

                    if (infinityModeSection != null)
                    {
                        config.intervalMs = infinityModeSection.IntervalMs;
                        config.step = infinityModeSection.Step;
                        config.radius = infinityModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.Spiral:
                    var spiralModeSection = ConfigurationManager.GetSection("SpiralMode") as SpiralModeSection;

                    if (spiralModeSection != null)
                    {
                        config.intervalMs = spiralModeSection.IntervalMs;
                        config.stepRadius = spiralModeSection.StepRadius;
                        config.startRadius = spiralModeSection.StartRadius;
                        config.loopCount = spiralModeSection.LoopCount;
                        config.isLoop = spiralModeSection.IsLoop;
                        config.isReverse = spiralModeSection.IsReverse;
                    }
                    break;
                case CursorMovementMode.Triangle:
                    var triangleModeSection = ConfigurationManager.GetSection("TriangleMode") as TriangleModeSection;

                    if (triangleModeSection != null)
                    {
                        config.intervalMs = triangleModeSection.IntervalMs;
                        config.step = triangleModeSection.Step;
                        config.point1 = triangleModeSection.Point1;
                        config.point2 = triangleModeSection.Point2;
                        config.point3 = triangleModeSection.Point3;
                    }
                    break;
                case CursorMovementMode.Quadrilateral:
                    var quadrilateralModeSection = ConfigurationManager.GetSection("QuadrilateralMode") as QuadrilateralModeSection;

                    if (quadrilateralModeSection != null)
                    {
                        config.intervalMs = quadrilateralModeSection.IntervalMs;
                        config.step = quadrilateralModeSection.Step;
                        config.point1 = quadrilateralModeSection.Point1;
                        config.point2 = quadrilateralModeSection.Point2;
                        config.point3 = quadrilateralModeSection.Point3;
                        config.point4 = quadrilateralModeSection.Point4;
                    }
                    break;
                case CursorMovementMode.Polygon:
                    var polygonModeSection = ConfigurationManager.GetSection("PolygonMode") as PolygonModeSection;

                    if (polygonModeSection != null)
                    {
                        config.intervalMs = polygonModeSection.IntervalMs;
                        config.step = polygonModeSection.Step;
                        config.angleCount = polygonModeSection.AngleCount;
                        config.radius = polygonModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.RandomPoint:
                    var randomPointModeSection = ConfigurationManager.GetSection("RandomPointMode") as RandomPointModeSection;

                    if (randomPointModeSection != null)
                    {
                        config.intervalMs = randomPointModeSection.IntervalMs;
                        config.step = randomPointModeSection.Step;
                    }
                    break;
                default:
                    throw new Exception($"Для мода {config.mode} не реализован Section в app Config");
            }

            return config;

        }

    }

}
