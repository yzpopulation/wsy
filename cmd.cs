#region using

using System.Diagnostics;

#endregion

namespace wsy
{
    public partial class cmd
    {
        public static string CmdRun(string cmdtext)
        {
            //string cmdtext = "taskkill /F /IM wps.exe";
            Process p = new Process();
            //设定程序名
            p.StartInfo.FileName = "cmd.exe";
            //关闭shell的使用
            p.StartInfo.UseShellExecute = false;
            //重新定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            //重新定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //设置不显示窗口
            p.StartInfo.CreateNoWindow = true;
            //执行ver命令
            p.Start();
            p.StandardInput.WriteLine(cmdtext);
            p.StandardInput.WriteLine("exit");
            //返还DOS信息
            string strinfo = p.StandardOutput.ReadToEnd();
            return strinfo;
        }
    }

}
