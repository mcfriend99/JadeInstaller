using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/******************* START ASSEMBLY INFO *********************/
// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("{{NAME}}")]
[assembly: AssemblyDescription("{{DESCRIPTION}}")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("{{COMPANY}}")]
[assembly: AssemblyProduct("{{NAME}}")]
[assembly: AssemblyCopyright("{{COPYRIGHT}}")]
[assembly: AssemblyTrademark("{{TRADEMARK}}")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e31a5d92-5be3-49a1-481a-93bfbb00be3a")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("{{VERSION}}")]
[assembly: AssemblyFileVersion("{{VERSION}}")]
/******************* END ASSEMBLY INFO *********************/

namespace JadeInstaller
{
  public class Installer : Form
  {
    /// <summary>
    /// The name of the installer project
    /// </summary>
    public string AppName = @"{{NAME}}";

    /// <summary>
    /// The application version.
    /// </summary>
    public string Version = @"{{VERSION}}";

    /// <summary>
    /// The name of the setup executable to generate
    /// </summary>
    public string SetupName = @"{{SETUP_NAME}}";

    /// <summary>
    /// The path the installer should extract to.
    /// </summary>
    public string InstallLocation = @"{{INSTALL_LOCATION}}";

    /// <summary>
    /// A list of commands to run before installation.
    /// </summary>
    public List<string> PreInstallCommands = new List<string>() {
      "{{PRE_INSTALL_COMMANDS}}"
    };

    /// <summary>
    /// A list of commands to run after installation.
    /// </summary>
    public List<string> PostInstallCommands = new List<string>() {
      "{{POST_INSTALL_COMMANDS}}"
    };

    /// <summary>
    /// The name of the executable that will be the main entry point
    /// of the installed app.
    /// This is the executable that will be pointed to by desktop shortcuts
    /// and start menu.
    /// </summary>
    public string ExecutableName = @"{{EXECUTABLE_NAME}}";

    public string License = @"{{LICENSE}}";

    /// <summary>
    /// Whether to add the installed application to start menu or not.
    /// </summary>
    public bool EnableAddStartMenuEntry = bool.Parse(@"{{ADD_STARTMENU_ENTRY}}");

    /// <summary>
    /// Whether to create a desktop icon for the installation or not.
    /// </summary>
    public bool EnableAddDesktopIcon = bool.Parse(@"{{ADD_DESKTOP_ICON}}");

    private bool AddToStartMenu = true;
    private bool AddToDesktop = true;
    private bool AgreedToLicense = false;

    //
    public string InstallData = @"{{INSTALL_DATA}}";
    private Panel welcomePanel;
    private PictureBox welcomeImg;
    private Label welcomeLabel;
    private Label welcomeDescriptionLabel;
    private Label label1;
    private Button nextBtn;
    private Button cancelBtn;
    private Panel navPanel;
    private Panel bodyPanel;
    private Button backBtn;
    private Panel licensePanel;

    // designer generated
    private bool AppUnusedBoolean = false;

    public void Install()
    {
      byte[] data = Convert.FromBase64String(InstallData);

      using(var stream = new MemoryStream(data))
      using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
      {
        archive.ExtractToDirectory(new DirectoryInfo(InstallLocation).FullName);
      }
    }
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Installer());
    }

    public Installer()
    {
      InitializeComponent();
      //Install();
    }

    private void InitializeComponent()
    {
      this.welcomePanel = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.welcomeDescriptionLabel = new System.Windows.Forms.Label();
      this.welcomeLabel = new System.Windows.Forms.Label();
      this.welcomeImg = new System.Windows.Forms.PictureBox();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.nextBtn = new System.Windows.Forms.Button();
      this.navPanel = new System.Windows.Forms.Panel();
      this.backBtn = new System.Windows.Forms.Button();
      this.bodyPanel = new System.Windows.Forms.Panel();
      this.licensePanel = new System.Windows.Forms.Panel();
      this.welcomePanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.welcomeImg)).BeginInit();
      this.navPanel.SuspendLayout();
      this.bodyPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // welcomePanel
      // 
      this.welcomePanel.Controls.Add(this.label1);
      this.welcomePanel.Controls.Add(this.welcomeDescriptionLabel);
      this.welcomePanel.Controls.Add(this.welcomeLabel);
      this.welcomePanel.Controls.Add(this.welcomeImg);
      this.welcomePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.welcomePanel.Location = new System.Drawing.Point(0, 0);
      this.welcomePanel.Name = "welcomePanel";
      this.welcomePanel.Size = new System.Drawing.Size(544, 342);
      this.welcomePanel.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(201, 170);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(328, 77);
      this.label1.TabIndex = 3;
      this.label1.Text = "It is recommended that you close all other applications before continuing.\n\nClick" +
    " Next to continue, or Cancel to exit the setup.";
      // 
      // welcomeDescriptionLabel
      // 
      this.welcomeDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.welcomeDescriptionLabel.AutoEllipsis = true;
      this.welcomeDescriptionLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.welcomeDescriptionLabel.Location = new System.Drawing.Point(201, 128);
      this.welcomeDescriptionLabel.Name = "welcomeDescriptionLabel";
      this.welcomeDescriptionLabel.Size = new System.Drawing.Size(328, 34);
      this.welcomeDescriptionLabel.TabIndex = 2;
      this.welcomeDescriptionLabel.Text = "This will install JadeInstaller 1.0.0 on your computer.";
      // 
      // welcomeLabel
      // 
      this.welcomeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.welcomeLabel.AutoEllipsis = true;
      this.welcomeLabel.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.welcomeLabel.Location = new System.Drawing.Point(196, 24);
      this.welcomeLabel.Name = "welcomeLabel";
      this.welcomeLabel.Size = new System.Drawing.Size(336, 95);
      this.welcomeLabel.TabIndex = 1;
      this.welcomeLabel.Text = "Welcome to the JadeInstaller setup wizard";
      // 
      // welcomeImg
      // 
      this.welcomeImg.BackColor = System.Drawing.Color.MediumOrchid;
      this.welcomeImg.Dock = System.Windows.Forms.DockStyle.Left;
      this.welcomeImg.Location = new System.Drawing.Point(0, 0);
      this.welcomeImg.Name = "welcomeImg";
      this.welcomeImg.Size = new System.Drawing.Size(180, 342);
      this.welcomeImg.TabIndex = 0;
      this.welcomeImg.TabStop = false;
      // 
      // cancelBtn
      // 
      this.cancelBtn.Location = new System.Drawing.Point(447, 11);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(84, 27);
      this.cancelBtn.TabIndex = 5;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
      // 
      // nextBtn
      // 
      this.nextBtn.Location = new System.Drawing.Point(352, 11);
      this.nextBtn.Name = "nextBtn";
      this.nextBtn.Size = new System.Drawing.Size(84, 27);
      this.nextBtn.TabIndex = 4;
      this.nextBtn.Text = "Next";
      this.nextBtn.UseVisualStyleBackColor = true;
      this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
      // 
      // navPanel
      // 
      this.navPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.navPanel.BackColor = System.Drawing.Color.WhiteSmoke;
      this.navPanel.Controls.Add(this.backBtn);
      this.navPanel.Controls.Add(this.cancelBtn);
      this.navPanel.Controls.Add(this.nextBtn);
      this.navPanel.Location = new System.Drawing.Point(0, 342);
      this.navPanel.Name = "navPanel";
      this.navPanel.Size = new System.Drawing.Size(544, 48);
      this.navPanel.TabIndex = 1;
      // 
      // backBtn
      // 
      this.backBtn.Location = new System.Drawing.Point(264, 11);
      this.backBtn.Name = "backBtn";
      this.backBtn.Size = new System.Drawing.Size(84, 27);
      this.backBtn.TabIndex = 6;
      this.backBtn.Text = "Back";
      this.backBtn.UseVisualStyleBackColor = true;
      this.backBtn.Visible = false;
      this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
      // 
      // bodyPanel
      // 
      this.bodyPanel.Controls.Add(this.licensePanel);
      this.bodyPanel.Controls.Add(this.welcomePanel);
      this.bodyPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.bodyPanel.Location = new System.Drawing.Point(0, 0);
      this.bodyPanel.Name = "bodyPanel";
      this.bodyPanel.Size = new System.Drawing.Size(544, 342);
      this.bodyPanel.TabIndex = 2;
      // 
      // licensePanel
      // 
      this.licensePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.licensePanel.Enabled = false;
      this.licensePanel.Location = new System.Drawing.Point(0, 0);
      this.licensePanel.Name = "licensePanel";
      this.licensePanel.Size = new System.Drawing.Size(544, 342);
      this.licensePanel.TabIndex = 7;
      this.licensePanel.Visible = false;
      // 
      // Installer
      // 
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(544, 390);
      this.Controls.Add(this.navPanel);
      this.Controls.Add(this.bodyPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Installer";
      this.Load += new System.EventHandler(this.Installer_Load);
      this.welcomePanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.welcomeImg)).EndInit();
      this.navPanel.ResumeLayout(false);
      this.bodyPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private void closeApp()
    {
      if(MessageBox.Show("Setup is not yet complete. Are you sure you want to cancel?", string.Format("{0} {1} setup", AppName, Version), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
      {
        Application.Exit();
      }
    }

    private void Installer_Load(object sender, EventArgs e)
    {
      Text = string.Format("{0} {1} setup wizard", AppName, Version);
      welcomeLabel.Text = string.Format("Welcome to the {0} setup wizard", AppName);
      welcomeDescriptionLabel.Text = string.Format("This will install {0} {1} on your computer.", AppName, Version);

      panels.Add(welcomePanel);
    }

    List<Panel> panels = new List<Panel>();

    int currentPage = 0;
    void Next()
    {
      if (currentPage < panels.Count - 1)
      {
        currentPage++;
        if (currentPage == 1 && License.Length == 0)
        {
          currentPage++;
        }

        foreach (Panel p in panels)
        {
          p.Visible = false;
          p.Enabled = false;
        }

        panels[currentPage].Visible = true;
        panels[currentPage].Enabled = true;
      }

      if(currentPage > 0)
      {
        backBtn.Visible = true;
      } else
      {
        backBtn.Visible = false;
      }

      if(currentPage == 1)
      {
        nextBtn.Enabled = AgreedToLicense;
      }
    }

    void Previous()
    {
      if (currentPage > 0)
      {
        currentPage--;
        if (currentPage == 1 && License.Length == 0)
        {
          currentPage--;
        }

        foreach (Panel p in panels)
        {
          p.Visible = false;
          p.Enabled = false;
        }

        panels[currentPage].Visible = true;
        panels[currentPage].Enabled = true;
      }

      if (currentPage > 0)
      {
        backBtn.Visible = true;
      }
      else
      {
        backBtn.Visible = false;
      }

      if (currentPage == 1)
      {
        nextBtn.Enabled = AgreedToLicense;
      }
    }

    private void nextBtn_Click(object sender, EventArgs e)
    {
      Next();
    }

    private void backBtn_Click(object sender, EventArgs e)
    {
      Previous();
    }

    private void cancelBtn_Click(object sender, EventArgs e)
    {
      closeApp();
    }
  }
}
