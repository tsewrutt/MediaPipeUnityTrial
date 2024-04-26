using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;

public class PythonInitializer : MonoBehaviour
{
    // Start is called before the first frame update

    public string pythonInterpreterPath = "Assets/BodyCaptureUsingMediaPipe/.venv/Scripts/python.exe";
    public string pythonScriptPath = "Assets/BodyCaptureUsingMediaPipe/HelloWorld.py";
    private Process pythonProcess;

    public void RunPythonScript()
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = pythonInterpreterPath;
            psi.Arguments = pythonScriptPath;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            pythonProcess = Process.Start(psi);

            string output = pythonProcess.StandardOutput.ReadToEnd();
            string error = pythonProcess.StandardError.ReadToEnd();

            UnityEngine.Debug.Log(output);

        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex);
        }

    }
    public void StopPythonProcess()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            pythonProcess.Kill();
            pythonProcess.Dispose();
        }
    }
}

//Async Stuff//

    //public async void RunPythonScript()
    //{
    //    try
    //    {
    //        ProcessStartInfo psi = new ProcessStartInfo();
    //        psi.FileName = pythonInterpreterPath;
    //        psi.Arguments = pythonScriptPath;
    //        psi.UseShellExecute = false;
    //        psi.RedirectStandardOutput = true;
    //        psi.RedirectStandardError = true;

    //        pythonProcess = Process.Start(psi);

    //        string output = pythonProcess.StandardOutput.ReadToEnd();
    //        string error = pythonProcess.StandardError.ReadToEnd();

           
    //        await pythonProcess.WaitForExitAsync();
    //        UnityEngine.Debug.Log(output);

    //    }
    //    catch (Exception ex)
    //    {
    //        UnityEngine.Debug.Log(ex);
    //    }

    //}

//public static class ProcessExtensions
//{
//    public static async Task<bool> WaitForExitAsync(this Process process, int millisecondsTimeout = Timeout.Infinite)
//    {
//        var tcs = new TaskCompletionSource<bool>();
//        process.EnableRaisingEvents = true;
//        process.Exited += (sender, args) => tcs.TrySetResult(true);

//        if (millisecondsTimeout != Timeout.Infinite)
//        {
//            await Task.Delay(millisecondsTimeout);
//            tcs.TrySetResult(false);
//        }

//        return await tcs.Task;
//    }
//}
