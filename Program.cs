using System.Drawing;
using System.Runtime.InteropServices;

namespace CursorAutoMovement
{
    class CursorMovement
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point lpPoint);

        public event EventHandler<Point>? MouseMoved = delegate { };

        private Point lastPosition;
        private Point center = new Point((int)(MonitorBounds.Right / 2), (int)(MonitorBounds.Bottom / 2));
        private bool isMonitoring = false;

        public async Task StartAutoCursorAsync(CancellationToken token)
        {
            var config = ConfigReader.ReadConfig();

            switch (config.mode)
            {
                case CursorMovementMode.Circle:
                    await StartCircleAsync(config.intervalMs, config.step, config.radius, token);
                    break;
                case CursorMovementMode.LockScreen:
                    await StartLockScreenAsync(config.intervalMs, token);
                    break;
                case CursorMovementMode.Infinity:
                    await StartInfinityAsync(config.intervalMs, config.step, config.radius, token);
                    break;
                case CursorMovementMode.Spiral:
                    await StartSpiralAsync(
                        intervalMs: config.intervalMs,
                        stepRadius: config.stepRadius,
                        startRadius: config.startRadius,
                        loopCount: config.loopCount,
                        isLoop: config.isLoop,
                        isReverse: config.isReverse,
                        cancellationToken: token);
                    break;
                case CursorMovementMode.Triangle:
                    await StartTriangleAsync(
                        intervalMs: config.intervalMs,
                        step: config.step,
                        point1: config.point1,
                        point2: config.point2,
                        point3: config.point3,
                        cancellationToken: token
                        );
                    break;
                case CursorMovementMode.Quadrilateral:
                    await StartQuadrilateralAsync(
                        intervalMs: config.intervalMs,
                        step: config.step,
                        point1: config.point1,
                        point2: config.point2,
                        point3: config.point3,
                        point4: config.point4,
                        cancellationToken: token
                        );
                    break;
                case CursorMovementMode.Polygon:
                    await StartPolygonAsync(config.intervalMs, config.step, config.angleCount, config.radius, token);
                    break;
                case CursorMovementMode.RandomPoint:
                    await StartRandomPointAsync(config.intervalMs, config.step, token);
                    break;
                default:
                    throw new Exception($"Для мода {config.mode} не реализован метод для перемещения курсора");
            }
        }
        public void StartMonitoring()
        {
            isMonitoring = true;
            GetCursorPos(out lastPosition);
            Thread monitorThread = new Thread(MonitorMouse);
            monitorThread.IsBackground = true;
            monitorThread.Start();
        }

        public void StopMonitoring()
        {
            isMonitoring = false;
        }

        public void Dispose()
        {
            MouseMoved = null;
        }

        protected virtual void OnMouseMoved(Point position)
        {
            MouseMoved?.Invoke(this, position);
        }

        private void MonitorMouse()
        {
            int threshold = 10;

            while (isMonitoring)
            {
                GetCursorPos(out Point currentPosition);

                if (Math.Abs(currentPosition.X - lastPosition.X) > threshold ||
                    Math.Abs(currentPosition.Y - lastPosition.Y) > threshold)
                {
                    OnMouseMoved(currentPosition);
                    lastPosition = currentPosition;
                }

                Thread.Sleep(50);
            }
        }

        private async Task StartLockScreenAsync(int intervalMs, CancellationToken cancellationToken = default)
        {
            MoveTo(center.X, center.Y);
            int stepX = 1;
            int stepY = 1;
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    GetCursorPos(out Point currentPos);

                    int x = currentPos.X;
                    int y = currentPos.Y;

                    if (y - 10 <= MonitorBounds.Top || y + 10 >= MonitorBounds.Bottom)
                    {
                        stepY *= -1;
                    }
                    if (x - 10 <= MonitorBounds.Left || x + 10 >= MonitorBounds.Right)
                    {
                        stepX *= -1;
                    }

                    x = x + stepX;
                    y = y + stepY;

                    MoveTo(x, y);

                    await Task.Delay(intervalMs, cts.Token);
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartCircleAsync(int intervalMs, int step, int radius, CancellationToken cancellationToken = default)
        {
            MoveTo(center.X, center.Y - radius);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    for (int degrees = 0; degrees < 360; degrees += step)
                    {
                        GetCursorPos(out Point currentPos);

                        int x = currentPos.X;
                        int y = currentPos.Y;
                        var radians = degrees * (Math.PI / 180);

                        x = (int)(center.X + radius * Math.Cos(radians));
                        y = (int)(center.Y + radius * Math.Sin(radians));

                        MoveTo(x, y);

                        await Task.Delay(intervalMs, cts.Token);
                    }
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartInfinityAsync(int intervalMs, int step, int radius, CancellationToken cancellationToken = default)
        {
            MoveTo(center.X, center.Y);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Point centerLeftCirc = new Point(center.X - radius, center.Y);
            Point centerRightCirc = new Point(center.X + radius, center.Y);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    //Левый круг.
                    for (int degrees = 0; degrees < 360; degrees += step)
                    {
                        GetCursorPos(out Point currentPos);

                        int x = currentPos.X;
                        int y = currentPos.Y;
                        var radians = degrees * (Math.PI / 180);

                        x = (int)(centerLeftCirc.X + radius * Math.Cos(radians));
                        y = (int)(centerLeftCirc.Y + radius * Math.Sin(radians));

                        MoveTo(x, y);

                        await Task.Delay(intervalMs, cts.Token);
                    }
                    //Правый круг.
                    for (int degrees = 180; degrees > -180; degrees -= step)
                    {
                        GetCursorPos(out Point currentPos);

                        int x = currentPos.X;
                        int y = currentPos.Y;
                        var radians = degrees * (Math.PI / 180);

                        x = (int)(centerRightCirc.X + radius * Math.Cos(radians));
                        y = (int)(centerRightCirc.Y + radius * Math.Sin(radians));

                        MoveTo(x, y);

                        await Task.Delay(intervalMs, cts.Token);
                    }
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartSpiralAsync(int intervalMs, int stepRadius, int startRadius, int loopCount,
            bool isLoop, bool isReverse, CancellationToken cancellationToken = default)
        {
            MoveTo(center.X, center.Y);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            async Task Forward()
            {
                for (float theta = 0; theta < loopCount * Math.PI; theta += 0.1f)
                {
                    float r = startRadius + stepRadius * theta;
                    int x = (int)(center.X + r * (float)Math.Cos(theta));
                    int y = (int)(center.Y + r * (float)Math.Sin(theta));

                    MoveTo(x, y);

                    await Task.Delay(intervalMs, cts.Token);
                }
            }
            async Task Reverse()
            {
                for (float theta = (float)(loopCount * Math.PI); theta > 0; theta -= 0.1f)
                {
                    float r = startRadius + stepRadius * theta;
                    int x = (int)(center.X + r * (float)Math.Cos(theta));
                    int y = (int)(center.Y + r * (float)Math.Sin(theta));

                    MoveTo(x, y);

                    await Task.Delay(intervalMs, cts.Token);
                }
            }
            async Task startLoop(bool isReverse)
            {
                if (isReverse)
                {
                    await Reverse();
                }
                else
                {
                    await Forward();
                }
            }

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await startLoop(isReverse);
                    if (isLoop)
                    {
                        await startLoop(!isReverse);
                    }

                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartTriangleAsync(int intervalMs, int step, Point point1, Point point2, Point point3, CancellationToken cancellationToken = default)
        {
            MoveTo(point1.X, point1.Y);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await MoveToPoint(intervalMs, step, point2, cts);
                    await MoveToPoint(intervalMs, step, point3, cts);
                    await MoveToPoint(intervalMs, step, point1, cts);
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartQuadrilateralAsync(int intervalMs, int step, Point point1, Point point2, Point point3, Point point4, CancellationToken cancellationToken = default)
        {
            MoveTo(point1.X, point1.Y);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await MoveToPoint(intervalMs, step, point2, cts);
                    await MoveToPoint(intervalMs, step, point3, cts);
                    await MoveToPoint(intervalMs, step, point4, cts);
                    await MoveToPoint(intervalMs, step, point1, cts);
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartPolygonAsync(int intervalMs, int step, int angleCount, int radius, CancellationToken cancellationToken = default)
        {
            var points = new Point[angleCount];
            for (int i = 0; i < angleCount; i++)
            {
                int x = (int)(center.X + radius * Math.Cos(2 * Math.PI * i / angleCount));
                int y = (int)(center.Y + radius * Math.Sin(2 * Math.PI * i / angleCount));
                points[i] = new Point(x, y);
            }

            MoveTo(points[angleCount - 1].X, points[angleCount - 1].Y);
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    foreach (Point point in points)
                    {
                        await MoveToPoint(intervalMs, step, point, cts);
                    }
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        private async Task StartRandomPointAsync(int intervalMs, int step, CancellationToken cancellationToken = default)
        {
            Random random = new Random();
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    int x = random.Next(MonitorBounds.Left, MonitorBounds.Right);
                    int y = random.Next(MonitorBounds.Top, MonitorBounds.Bottom);
                    await MoveToPoint(intervalMs, step, new Point(x, y), cts);
                }
            }
            finally
            {
                StopMonitoring();
            }
        }

        async Task MoveToPoint(int intervalMs, int step, Point b, CancellationTokenSource cts)
        {
            GetCursorPos(out Point currentPos);

            while ((Math.Abs(currentPos.X - b.X) >= step || Math.Abs(currentPos.Y - b.Y) >= step) && !cts.Token.IsCancellationRequested)
            {
                GetCursorPos(out currentPos);
                double directionX = b.X - currentPos.X;
                double directionY = b.Y - currentPos.Y;

                double distance = Math.Sqrt(directionX * directionX + directionY * directionY);

                if (distance > 0)
                {
                    directionX /= distance;
                    directionY /= distance;
                }

                int stepX = (int)Math.Round(directionX * step);
                int stepY = (int)Math.Round(directionY * step);
                MoveTo(currentPos.X + stepX, currentPos.Y + stepY);

                await Task.Delay(intervalMs, cts.Token);
            }
        }

        private void MoveTo(int x, int y)
        {
            SetCursorPos(x, y);
            lastPosition.X = x;
            lastPosition.Y = y;
        }

    }

    class MonitorBounds
    {
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        public static int Left = 0;
        public static int Top = 0;
        public static int Right = GetSystemMetrics(SM_CXSCREEN);
        public static int Bottom = GetSystemMetrics(SM_CYSCREEN);
    }

    class NativeMessageBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void ShowError(string message)
        {
            MessageBox(IntPtr.Zero, message, "Ошибка", 0x00000010);
        }
    }

    internal class Program
    {
        static async Task Main()
        {
            var cursor = new CursorMovement();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            cursor.StartMonitoring();
            cursor.MouseMoved += (s, p) =>
            {
                cancelTokenSource.Cancel();
                return;
            };

            try
            {
                await cursor.StartAutoCursorAsync(token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception except)
            {
                NativeMessageBox.ShowError(except.Message);
            }
            finally
            {
                cursor.StopMonitoring();
                cursor.Dispose();
            }
        }

    }
}
