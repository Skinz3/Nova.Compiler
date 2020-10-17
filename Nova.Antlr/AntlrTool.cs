using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Antlr
{
    public class AntlrTool
    {
        public const string DLanguage = "CSharp_v4_5";

        public const string AntlrNamespace = "org.antlr.v4.Tool";

        private string AntlrPath
        {
            get;
            set;
        }
        public AntlrTool(string antlrPath)
        {
            this.AntlrPath = antlrPath;
        }
        public bool Generate(string grammarPath, string outputDirectory)
        {
            ProcessStartInfo psi = new ProcessStartInfo("java", string.Format("-cp {0} {1} -Dlanguage={2} {3} -o {4}", AntlrPath, AntlrNamespace, DLanguage, grammarPath, outputDirectory));
            Process process = new Process();
            process.StartInfo = psi;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
                Console.WriteLine(e.Data);
        }

         
    }
}
