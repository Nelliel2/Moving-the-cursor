using MovingCursor.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovingCursor
{
    internal class Program
    {
        static async Task Main()
        {
            var cursor = new Cursor();
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
                await cursor.StartModeMoveAsync(token);
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
