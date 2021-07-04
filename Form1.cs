using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JadeInstaller
{
  public partial class mainForm : Form
  {
    public mainForm()
    {
      InitializeComponent();
    }

    private void mainForm_Load(object sender, EventArgs e)
    {
      InstallCreator creator = new InstallCreator();
      creator.Name = "Sample";
      creator.SetupName = "setup.exe";
      creator.Files = new List<string>() {
        "JadeInstaller.exe",
        "JadeInstaller.exe.config",
        "JadeInstaller.pdb",
      };
      creator.InstallLocation = new DirectoryInfo("C:\\Users\\Richard\\Desktop\\TestInstall");
      creator.ExecutableName = "Test.exe";

      string result = creator.Generate();
      if(result == null)
      {
        MessageBox.Show(creator.CompileError);
      }
    }
  }
}
