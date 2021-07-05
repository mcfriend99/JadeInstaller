using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
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
    /// The minimum storage required to install the application.
    /// </summary>
    public string StorageRequired = @"{{STORAGE_REQUIRED}}";

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

    /// <summary>
    /// Whether to create a quick launch icon for the installation or not.
    /// </summary>
    public bool EnableAddQuickLaunchIcon = bool.Parse(@"{{ADD_QUICK_LAUNCH_ICON}}");

    private bool AddToStartMenu = true;
    private bool AddToDesktop = true;
    private bool AddToQuickLaunch = true;
    private bool AgreedToLicense = true;

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
        private Panel panel1;
        private Label label3;
        private Label label2;
        private CheckBox licenstAgreeCheck;
    private RichTextBox licenseBox;
    private Panel installPathPanel;
    private Panel panel2;
    private Label installLocationDecription;
    private Label label4;
    private Label label6;
    private Label installLocationHint;
    private TextBox installLocationBox;
    private Button installPathSelector;
    private Label installSpaceInfo;
    private Panel shortcutsSelectPanel;
    private Panel panel3;
    private Label label7;
    private Label label5;
    private Label label8;
    private Label label9;
    private CheckBox quickLaunchCheckBox;
    private CheckBox desktopCheckBox;
    private CheckBox startMenuCheckBox;

    // designer generated
    private bool AppUnusedBoolean = false;

    public void Install()
    {
      byte[] data = Convert.FromBase64String(InstallData);

      using(var stream = new MemoryStream(data))
      using(ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
      {
        archive.ExtractToDirectory(new DirectoryInfo(Environment.ExpandEnvironmentVariables(InstallLocation)).FullName);
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
      this.licenstAgreeCheck = new System.Windows.Forms.CheckBox();
      this.backBtn = new System.Windows.Forms.Button();
      this.bodyPanel = new System.Windows.Forms.Panel();
      this.shortcutsSelectPanel = new System.Windows.Forms.Panel();
      this.quickLaunchCheckBox = new System.Windows.Forms.CheckBox();
      this.desktopCheckBox = new System.Windows.Forms.CheckBox();
      this.startMenuCheckBox = new System.Windows.Forms.CheckBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label7 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.installPathPanel = new System.Windows.Forms.Panel();
      this.installSpaceInfo = new System.Windows.Forms.Label();
      this.installPathSelector = new System.Windows.Forms.Button();
      this.installLocationBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.installLocationHint = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.installLocationDecription = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.licensePanel = new System.Windows.Forms.Panel();
      this.licenseBox = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.welcomePanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.welcomeImg)).BeginInit();
      this.navPanel.SuspendLayout();
      this.bodyPanel.SuspendLayout();
      this.shortcutsSelectPanel.SuspendLayout();
      this.panel3.SuspendLayout();
      this.installPathPanel.SuspendLayout();
      this.panel2.SuspendLayout();
      this.licensePanel.SuspendLayout();
      this.panel1.SuspendLayout();
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
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.welcomeDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.nextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.navPanel.Controls.Add(this.licenstAgreeCheck);
      this.navPanel.Controls.Add(this.backBtn);
      this.navPanel.Controls.Add(this.cancelBtn);
      this.navPanel.Controls.Add(this.nextBtn);
      this.navPanel.Location = new System.Drawing.Point(0, 342);
      this.navPanel.Name = "navPanel";
      this.navPanel.Size = new System.Drawing.Size(544, 48);
      this.navPanel.TabIndex = 1;
      // 
      // licenstAgreeCheck
      // 
      this.licenstAgreeCheck.AutoSize = true;
      this.licenstAgreeCheck.Checked = true;
      this.licenstAgreeCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.licenstAgreeCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.licenstAgreeCheck.Location = new System.Drawing.Point(12, 17);
      this.licenstAgreeCheck.Name = "licenstAgreeCheck";
      this.licenstAgreeCheck.Size = new System.Drawing.Size(136, 17);
      this.licenstAgreeCheck.TabIndex = 0;
      this.licenstAgreeCheck.Text = "I accept the agreement";
      this.licenstAgreeCheck.UseVisualStyleBackColor = true;
      this.licenstAgreeCheck.Visible = false;
      this.licenstAgreeCheck.CheckedChanged += new System.EventHandler(this.licenstAgreeCheck_CheckedChanged);
      // 
      // backBtn
      // 
      this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
      this.bodyPanel.Controls.Add(this.shortcutsSelectPanel);
      this.bodyPanel.Controls.Add(this.installPathPanel);
      this.bodyPanel.Controls.Add(this.licensePanel);
      this.bodyPanel.Controls.Add(this.welcomePanel);
      this.bodyPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.bodyPanel.Location = new System.Drawing.Point(0, 0);
      this.bodyPanel.Name = "bodyPanel";
      this.bodyPanel.Size = new System.Drawing.Size(544, 342);
      this.bodyPanel.TabIndex = 2;
      // 
      // shortcutsSelectPanel
      // 
      this.shortcutsSelectPanel.Controls.Add(this.quickLaunchCheckBox);
      this.shortcutsSelectPanel.Controls.Add(this.desktopCheckBox);
      this.shortcutsSelectPanel.Controls.Add(this.startMenuCheckBox);
      this.shortcutsSelectPanel.Controls.Add(this.label9);
      this.shortcutsSelectPanel.Controls.Add(this.label8);
      this.shortcutsSelectPanel.Controls.Add(this.panel3);
      this.shortcutsSelectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.shortcutsSelectPanel.Location = new System.Drawing.Point(0, 0);
      this.shortcutsSelectPanel.Name = "shortcutsSelectPanel";
      this.shortcutsSelectPanel.Size = new System.Drawing.Size(544, 342);
      this.shortcutsSelectPanel.TabIndex = 6;
      this.shortcutsSelectPanel.Visible = false;
      // 
      // quickLaunchCheckBox
      // 
      this.quickLaunchCheckBox.AutoSize = true;
      this.quickLaunchCheckBox.Location = new System.Drawing.Point(54, 223);
      this.quickLaunchCheckBox.Name = "quickLaunchCheckBox";
      this.quickLaunchCheckBox.Size = new System.Drawing.Size(150, 17);
      this.quickLaunchCheckBox.TabIndex = 5;
      this.quickLaunchCheckBox.Text = "Create Quick Launch icon";
      this.quickLaunchCheckBox.UseVisualStyleBackColor = true;
      this.quickLaunchCheckBox.CheckedChanged += new System.EventHandler(this.quickLaunchCheckBox_CheckedChanged);
      // 
      // desktopCheckBox
      // 
      this.desktopCheckBox.AutoSize = true;
      this.desktopCheckBox.Location = new System.Drawing.Point(54, 199);
      this.desktopCheckBox.Name = "desktopCheckBox";
      this.desktopCheckBox.Size = new System.Drawing.Size(121, 17);
      this.desktopCheckBox.TabIndex = 4;
      this.desktopCheckBox.Text = "Create desktop icon";
      this.desktopCheckBox.UseVisualStyleBackColor = true;
      this.desktopCheckBox.CheckedChanged += new System.EventHandler(this.desktopCheckBox_CheckedChanged);
      // 
      // startMenuCheckBox
      // 
      this.startMenuCheckBox.AutoSize = true;
      this.startMenuCheckBox.Location = new System.Drawing.Point(54, 175);
      this.startMenuCheckBox.Name = "startMenuCheckBox";
      this.startMenuCheckBox.Size = new System.Drawing.Size(150, 17);
      this.startMenuCheckBox.TabIndex = 3;
      this.startMenuCheckBox.Text = "Create start menu shortcut";
      this.startMenuCheckBox.UseVisualStyleBackColor = true;
      this.startMenuCheckBox.CheckedChanged += new System.EventHandler(this.startMenuCheckBox_CheckedChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(37, 149);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(103, 13);
      this.label9.TabIndex = 2;
      this.label9.Text = "Icons and shortcuts:";
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(37, 84);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(467, 47);
      this.label8.TabIndex = 1;
      this.label8.Text = "Select the icons and shortcuts you will like to configure while we are installing" +
    " JadeInstaller 1.0.0.\nClick Next when you are ready to proceed.";
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel3.Controls.Add(this.label7);
      this.panel3.Controls.Add(this.label5);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(544, 65);
      this.panel3.TabIndex = 0;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(34, 36);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(362, 13);
      this.label7.TabIndex = 1;
      this.label7.Text = "Which icons and shortcuts should be configured along with the installation?";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(34, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(143, 16);
      this.label5.TabIndex = 0;
      this.label5.Text = "Icons and Shortcuts";
      // 
      // installPathPanel
      // 
      this.installPathPanel.Controls.Add(this.installSpaceInfo);
      this.installPathPanel.Controls.Add(this.installPathSelector);
      this.installPathPanel.Controls.Add(this.installLocationBox);
      this.installPathPanel.Controls.Add(this.label6);
      this.installPathPanel.Controls.Add(this.installLocationHint);
      this.installPathPanel.Controls.Add(this.panel2);
      this.installPathPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.installPathPanel.Enabled = false;
      this.installPathPanel.Location = new System.Drawing.Point(0, 0);
      this.installPathPanel.Name = "installPathPanel";
      this.installPathPanel.Size = new System.Drawing.Size(544, 342);
      this.installPathPanel.TabIndex = 2;
      this.installPathPanel.Visible = false;
      // 
      // installSpaceInfo
      // 
      this.installSpaceInfo.AutoSize = true;
      this.installSpaceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.installSpaceInfo.Location = new System.Drawing.Point(34, 311);
      this.installSpaceInfo.Name = "installSpaceInfo";
      this.installSpaceInfo.Size = new System.Drawing.Size(282, 13);
      this.installSpaceInfo.TabIndex = 5;
      this.installSpaceInfo.Text = "At least 10 MB of disk space is required for this installation.";
      // 
      // installPathSelector
      // 
      this.installPathSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.installPathSelector.Location = new System.Drawing.Point(429, 156);
      this.installPathSelector.Name = "installPathSelector";
      this.installPathSelector.Size = new System.Drawing.Size(75, 23);
      this.installPathSelector.TabIndex = 4;
      this.installPathSelector.Text = "Browse";
      this.installPathSelector.UseVisualStyleBackColor = true;
      this.installPathSelector.Click += new System.EventHandler(this.installPathSelector_Click);
      // 
      // installLocationBox
      // 
      this.installLocationBox.Location = new System.Drawing.Point(34, 157);
      this.installLocationBox.Name = "installLocationBox";
      this.installLocationBox.Size = new System.Drawing.Size(388, 20);
      this.installLocationBox.TabIndex = 3;
      this.installLocationBox.TextChanged += new System.EventHandler(this.installLocationBox_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(34, 131);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(390, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "To continue, click Next. If you would like to select a different folder, click Br" +
    "owse.";
      // 
      // installLocationHint
      // 
      this.installLocationHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.installLocationHint.Location = new System.Drawing.Point(34, 84);
      this.installLocationHint.Name = "installLocationHint";
      this.installLocationHint.Size = new System.Drawing.Size(470, 35);
      this.installLocationHint.TabIndex = 1;
      this.installLocationHint.Text = "JadeInstaller will be installed to the following location.";
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel2.Controls.Add(this.installLocationDecription);
      this.panel2.Controls.Add(this.label4);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(544, 62);
      this.panel2.TabIndex = 0;
      // 
      // installLocationDecription
      // 
      this.installLocationDecription.AutoSize = true;
      this.installLocationDecription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.installLocationDecription.Location = new System.Drawing.Point(31, 34);
      this.installLocationDecription.Name = "installLocationDecription";
      this.installLocationDecription.Size = new System.Drawing.Size(191, 13);
      this.installLocationDecription.TabIndex = 1;
      this.installLocationDecription.Text = "Where should JadeInstaller be installed";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(31, 13);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(197, 16);
      this.label4.TabIndex = 0;
      this.label4.Text = "Select Destination Location";
      // 
      // licensePanel
      // 
      this.licensePanel.Controls.Add(this.licenseBox);
      this.licensePanel.Controls.Add(this.panel1);
      this.licensePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.licensePanel.Enabled = false;
      this.licensePanel.Location = new System.Drawing.Point(0, 0);
      this.licensePanel.Name = "licensePanel";
      this.licensePanel.Size = new System.Drawing.Size(544, 342);
      this.licensePanel.TabIndex = 7;
      this.licensePanel.Visible = false;
      // 
      // licenseBox
      // 
      this.licenseBox.BackColor = System.Drawing.Color.White;
      this.licenseBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.licenseBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.licenseBox.Location = new System.Drawing.Point(0, 65);
      this.licenseBox.Margin = new System.Windows.Forms.Padding(16);
      this.licenseBox.Name = "licenseBox";
      this.licenseBox.ReadOnly = true;
      this.licenseBox.ShortcutsEnabled = false;
      this.licenseBox.ShowSelectionMargin = true;
      this.licenseBox.Size = new System.Drawing.Size(544, 277);
      this.licenseBox.TabIndex = 1;
      this.licenseBox.Text = "";
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(544, 65);
      this.panel1.TabIndex = 0;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(26, 27);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(503, 36);
      this.label3.TabIndex = 1;
      this.label3.Text = "Please read the following license agreement. You must accept the terms of this ag" +
    "reement before continuing with the installation.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(26, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(141, 16);
      this.label2.TabIndex = 0;
      this.label2.Text = "License Agreement";
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
      this.navPanel.PerformLayout();
      this.bodyPanel.ResumeLayout(false);
      this.shortcutsSelectPanel.ResumeLayout(false);
      this.shortcutsSelectPanel.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.installPathPanel.ResumeLayout(false);
      this.installPathPanel.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.licensePanel.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
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

      InstallLocation = Environment.ExpandEnvironmentVariables(InstallLocation);

      installLocationDecription.Text = string.Format("Where should {0} {1} be installed?", AppName, Version);
      installLocationHint.Text = string.Format("{0} will be installed to the following location.", AppName);
      installLocationBox.Text = InstallLocation;
      installSpaceInfo.Text = string.Format("At least {0} of disk space is required for this installation.", StorageRequired);

      licenseBox.Rtf = License;

      desktopCheckBox.Enabled = desktopCheckBox.Checked = AddToDesktop = EnableAddDesktopIcon;
      startMenuCheckBox.Enabled = startMenuCheckBox.Checked = AddToStartMenu = EnableAddStartMenuEntry;
      quickLaunchCheckBox.Enabled = quickLaunchCheckBox.Checked = AddToQuickLaunch = EnableAddQuickLaunchIcon;

      panels.Add(welcomePanel);
      panels.Add(licensePanel);
      panels.Add(installPathPanel);
      panels.Add(shortcutsSelectPanel);
    }

    List<Panel> panels = new List<Panel>();

    int currentPage = 0;
    void Next()
    {
      if(currentPage == 2)
      {
        if(Directory.Exists(InstallLocation))
        {
          if(MessageBox.Show(string.Format("The folder {0} already exists.\nWould you like to install to the folder anyway?", InstallLocation), string.Format("{0} {1} setup", AppName, Version), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
          {
            return;
          }
        }
      }

      if (currentPage < panels.Count - 1)
      {
        currentPage++;
        if (currentPage == 1 && License.Length == 0)
        {
          currentPage++;
        }

        if(currentPage > panels.Count - 1)
        {
          currentPage = 0;
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
        licenstAgreeCheck.Visible = true;
      } else
      {
        licenstAgreeCheck.Visible = false;
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

        if(currentPage < 0)
        {
          currentPage = 0;
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
        licenstAgreeCheck.Visible = true;
      }
      else if (currentPage == 0)
      {
        nextBtn.Enabled = true;
        licenstAgreeCheck.Visible = false;
      }
      else if (currentPage == 2) {
        nextBtn.Enabled = installPathUriIsValid;
        licenstAgreeCheck.Visible = false;
      }
      else
      {
        licenstAgreeCheck.Visible = false;
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

    private void licenstAgreeCheck_CheckedChanged(object sender, EventArgs e)
    {
      AgreedToLicense = licenstAgreeCheck.Checked;
      nextBtn.Enabled = AgreedToLicense;
    }

    private void installPathSelector_Click(object sender, EventArgs e)
    {
      FolderBrowserDialogEx dialog = new FolderBrowserDialogEx();
      dialog.ShowNewFolderButton = false;
      dialog.ShowEditBox = true;
      dialog.ShowFullPathInEditBox = false;
      dialog.NewStyle = true;
      dialog.Description = "Select a folder in the list below, then click OK.";

      if(dialog.ShowDialog() == DialogResult.OK)
      {
        InstallLocation = string.Format("{0}\\{1}", dialog.SelectedPath, AppName);
        installLocationBox.Text = InstallLocation;
      }
    }

    private bool installPathUriIsValid = true;
    private void installLocationBox_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (Path.IsPathRooted(installLocationBox.Text))
        {
          InstallLocation = Path.GetFullPath(installLocationBox.Text);
          installPathUriIsValid = true;
        } else
        {
          installPathUriIsValid = false;
        }
      } catch(Exception)
      {
        installPathUriIsValid = false;
      }
      nextBtn.Enabled = installPathUriIsValid;
    }

    private void startMenuCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      AddToStartMenu = startMenuCheckBox.Checked;
    }

    private void desktopCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      AddToDesktop = desktopCheckBox.Checked;
    }

    private void quickLaunchCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      AddToQuickLaunch = quickLaunchCheckBox.Checked;
    }
  }



  ///// FOLDER DIALG
  #region FOLDER DIALOG

  public class FolderBrowserDialogEx : System.Windows.Forms.CommonDialog
  {
    private static readonly int MAX_PATH = 260;

    // Fields
    private PInvoke.BrowseFolderCallbackProc _callback;
    private string _descriptionText;
    private Environment.SpecialFolder _rootFolder;
    private string _selectedPath;
    private bool _selectedPathNeedsCheck;
    private bool _showNewFolderButton;
    private bool _showEditBox;
    private bool _showBothFilesAndFolders;
    private bool _newStyle = true;
    private bool _showFullPathInEditBox = true;
    private bool _dontIncludeNetworkFoldersBelowDomainLevel;
    private int _uiFlags;
    private IntPtr _hwndEdit;
    private IntPtr _rootFolderLocation;

    // Events
    //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler HelpRequest
    {
      add
      {
        base.HelpRequest += value;
      }
      remove
      {
        base.HelpRequest -= value;
      }
    }

    // ctor
    public FolderBrowserDialogEx()
    {
      this.Reset();
    }

    // Factory Methods
    public static FolderBrowserDialogEx PrinterBrowser()
    {
      FolderBrowserDialogEx x = new FolderBrowserDialogEx();
      // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
      x.BecomePrinterBrowser();
      return x;
    }

    public static FolderBrowserDialogEx ComputerBrowser()
    {
      FolderBrowserDialogEx x = new FolderBrowserDialogEx();
      // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
      x.BecomeComputerBrowser();
      return x;
    }


    // Helpers
    private void BecomePrinterBrowser()
    {
      _uiFlags += BrowseFlags.BIF_BROWSEFORPRINTER;
      Description = "Select a printer:";
      PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CSIDL.PRINTERS, ref this._rootFolderLocation);
      ShowNewFolderButton = false;
      ShowEditBox = false;
    }

    private void BecomeComputerBrowser()
    {
      _uiFlags += BrowseFlags.BIF_BROWSEFORCOMPUTER;
      Description = "Select a computer:";
      PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CSIDL.NETWORK, ref this._rootFolderLocation);
      ShowNewFolderButton = false;
      ShowEditBox = false;
    }


    private class CSIDL
    {
      public const int PRINTERS = 4;
      public const int NETWORK = 0x12;
    }

    private class BrowseFlags
    {
      public const int BIF_DEFAULT = 0x0000;
      public const int BIF_BROWSEFORCOMPUTER = 0x1000;
      public const int BIF_BROWSEFORPRINTER = 0x2000;
      public const int BIF_BROWSEINCLUDEFILES = 0x4000;
      public const int BIF_BROWSEINCLUDEURLS = 0x0080;
      public const int BIF_DONTGOBELOWDOMAIN = 0x0002;
      public const int BIF_EDITBOX = 0x0010;
      public const int BIF_NEWDIALOGSTYLE = 0x0040;
      public const int BIF_NONEWFOLDERBUTTON = 0x0200;
      public const int BIF_RETURNFSANCESTORS = 0x0008;
      public const int BIF_RETURNONLYFSDIRS = 0x0001;
      public const int BIF_SHAREABLE = 0x8000;
      public const int BIF_STATUSTEXT = 0x0004;
      public const int BIF_UAHINT = 0x0100;
      public const int BIF_VALIDATE = 0x0020;
      public const int BIF_NOTRANSLATETARGETS = 0x0400;
    }

    private static class BrowseForFolderMessages
    {
      // messages FROM the folder browser
      public const int BFFM_INITIALIZED = 1;
      public const int BFFM_SELCHANGED = 2;
      public const int BFFM_VALIDATEFAILEDA = 3;
      public const int BFFM_VALIDATEFAILEDW = 4;
      public const int BFFM_IUNKNOWN = 5;

      // messages TO the folder browser
      public const int BFFM_SETSTATUSTEXT = 0x464;
      public const int BFFM_ENABLEOK = 0x465;
      public const int BFFM_SETSELECTIONA = 0x466;
      public const int BFFM_SETSELECTIONW = 0x467;
    }

    private int FolderBrowserCallback(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
    {
      switch (msg)
      {
        case BrowseForFolderMessages.BFFM_INITIALIZED:
          if (this._selectedPath.Length != 0)
          {
            PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_SETSELECTIONW, 1, this._selectedPath);
            if (this._showEditBox && this._showFullPathInEditBox)
            {
              // get handle to the Edit box inside the Folder Browser Dialog
              _hwndEdit = PInvoke.User32.FindWindowEx(new HandleRef(null, hwnd), IntPtr.Zero, "Edit", null);
              PInvoke.User32.SetWindowText(_hwndEdit, this._selectedPath);
            }
          }
          break;

        case BrowseForFolderMessages.BFFM_SELCHANGED:
          IntPtr pidl = lParam;
          if (pidl != IntPtr.Zero)
          {
            if (((_uiFlags & BrowseFlags.BIF_BROWSEFORPRINTER) == BrowseFlags.BIF_BROWSEFORPRINTER) ||
                ((_uiFlags & BrowseFlags.BIF_BROWSEFORCOMPUTER) == BrowseFlags.BIF_BROWSEFORCOMPUTER))
            {
              // we're browsing for a printer or computer, enable the OK button unconditionally.
              PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_ENABLEOK, 0, 1);
            }
            else
            {
              IntPtr pszPath = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
              bool haveValidPath = PInvoke.Shell32.SHGetPathFromIDList(pidl, pszPath);
              String displayedPath = Marshal.PtrToStringAuto(pszPath);
              Marshal.FreeHGlobal(pszPath);
              // whether to enable the OK button or not. (if file is valid)
              PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_ENABLEOK, 0, haveValidPath ? 1 : 0);

              // Maybe set the Edit Box text to the Full Folder path
              if (haveValidPath && !String.IsNullOrEmpty(displayedPath))
              {
                if (_showEditBox && _showFullPathInEditBox)
                {
                  if (_hwndEdit != IntPtr.Zero)
                    PInvoke.User32.SetWindowText(_hwndEdit, displayedPath);
                }

                if ((_uiFlags & BrowseFlags.BIF_STATUSTEXT) == BrowseFlags.BIF_STATUSTEXT)
                  PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_SETSTATUSTEXT, 0, displayedPath);
              }
            }
          }
          break;
      }
      return 0;
    }

    private static PInvoke.IMalloc GetSHMalloc()
    {
      PInvoke.IMalloc[] ppMalloc = new PInvoke.IMalloc[1];
      PInvoke.Shell32.SHGetMalloc(ppMalloc);
      return ppMalloc[0];
    }

    public override void Reset()
    {
      this._rootFolder = (Environment.SpecialFolder)0;
      this._descriptionText = string.Empty;
      this._selectedPath = string.Empty;
      this._selectedPathNeedsCheck = false;
      this._showNewFolderButton = true;
      this._showEditBox = true;
      this._newStyle = true;
      this._dontIncludeNetworkFoldersBelowDomainLevel = false;
      this._hwndEdit = IntPtr.Zero;
      this._rootFolderLocation = IntPtr.Zero;
    }

    protected override bool RunDialog(IntPtr hWndOwner)
    {
      bool result = false;
      if (_rootFolderLocation == IntPtr.Zero)
      {
        PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)this._rootFolder, ref _rootFolderLocation);
        if (_rootFolderLocation == IntPtr.Zero)
        {
          PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref _rootFolderLocation);
          if (_rootFolderLocation == IntPtr.Zero)
          {
            throw new InvalidOperationException("FolderBrowserDialogNoRootFolder");
          }
        }
      }
      _hwndEdit = IntPtr.Zero;
      //_uiFlags = 0;
      if (_dontIncludeNetworkFoldersBelowDomainLevel)
        _uiFlags += BrowseFlags.BIF_DONTGOBELOWDOMAIN;
      if (this._newStyle)
        _uiFlags += BrowseFlags.BIF_NEWDIALOGSTYLE;
      if (!this._showNewFolderButton)
        _uiFlags += BrowseFlags.BIF_NONEWFOLDERBUTTON;
      if (this._showEditBox)
        _uiFlags += BrowseFlags.BIF_EDITBOX;
      if (this._showBothFilesAndFolders)
        _uiFlags += BrowseFlags.BIF_BROWSEINCLUDEFILES;


      if (Control.CheckForIllegalCrossThreadCalls && (Application.OleRequired() != ApartmentState.STA))
      {
        throw new ThreadStateException("DebuggingException: ThreadMustBeSTA");
      }
      IntPtr pidl = IntPtr.Zero;
      IntPtr hglobal = IntPtr.Zero;
      IntPtr pszPath = IntPtr.Zero;
      try
      {
        PInvoke.BROWSEINFO browseInfo = new PInvoke.BROWSEINFO();
        hglobal = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
        pszPath = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
        this._callback = new PInvoke.BrowseFolderCallbackProc(this.FolderBrowserCallback);
        browseInfo.pidlRoot = _rootFolderLocation;
        browseInfo.Owner = hWndOwner;
        browseInfo.pszDisplayName = hglobal;
        browseInfo.Title = this._descriptionText;
        browseInfo.Flags = _uiFlags;
        browseInfo.callback = this._callback;
        browseInfo.lParam = IntPtr.Zero;
        browseInfo.iImage = 0;
        pidl = PInvoke.Shell32.SHBrowseForFolder(browseInfo);
        if (((_uiFlags & BrowseFlags.BIF_BROWSEFORPRINTER) == BrowseFlags.BIF_BROWSEFORPRINTER) ||
        ((_uiFlags & BrowseFlags.BIF_BROWSEFORCOMPUTER) == BrowseFlags.BIF_BROWSEFORCOMPUTER))
        {
          this._selectedPath = Marshal.PtrToStringAuto(browseInfo.pszDisplayName);
          result = true;
        }
        else
        {
          if (pidl != IntPtr.Zero)
          {
            PInvoke.Shell32.SHGetPathFromIDList(pidl, pszPath);
            this._selectedPathNeedsCheck = true;
            this._selectedPath = Marshal.PtrToStringAuto(pszPath);
            result = true;
          }
        }
      }
      finally
      {
        PInvoke.IMalloc sHMalloc = GetSHMalloc();
        sHMalloc.Free(_rootFolderLocation);
        _rootFolderLocation = IntPtr.Zero;
        if (pidl != IntPtr.Zero)
        {
          sHMalloc.Free(pidl);
        }
        if (pszPath != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(pszPath);
        }
        if (hglobal != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(hglobal);
        }
        this._callback = null;
      }
      return result;
    }

    // Properties
    //[SRDescription("FolderBrowserDialogDescription"), SRCategory("CatFolderBrowsing"), Browsable(true), DefaultValue(""), Localizable(true)]

    /// <summary>
    /// This description appears near the top of the dialog box, providing direction to the user.
    /// </summary>
    public string Description
    {
      get
      {
        return this._descriptionText;
      }
      set
      {
        this._descriptionText = (value == null) ? string.Empty : value;
      }
    }

    //[Localizable(false), SRCategory("CatFolderBrowsing"), SRDescription("FolderBrowserDialogRootFolder"), TypeConverter(typeof(SpecialFolderEnumConverter)), Browsable(true), DefaultValue(0)]
    public Environment.SpecialFolder RootFolder
    {
      get
      {
        return this._rootFolder;
      }
      set
      {
        if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
        {
          throw new InvalidEnumArgumentException("value", (int)value, typeof(Environment.SpecialFolder));
        }
        this._rootFolder = value;
      }
    }

    //[Browsable(true), SRDescription("FolderBrowserDialogSelectedPath"), SRCategory("CatFolderBrowsing"), DefaultValue(""), Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]

    /// <summary>
    /// Set or get the selected path.  
    /// </summary>
    public string SelectedPath
    {
      get
      {
        if (((this._selectedPath != null) && (this._selectedPath.Length != 0)) && this._selectedPathNeedsCheck)
        {
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._selectedPath).Demand();
          this._selectedPathNeedsCheck = false;
        }
        return this._selectedPath;
      }
      set
      {
        this._selectedPath = (value == null) ? string.Empty : value;
        this._selectedPathNeedsCheck = true;
      }
    }

    //[SRDescription("FolderBrowserDialogShowNewFolderButton"), Localizable(false), Browsable(true), DefaultValue(true), SRCategory("CatFolderBrowsing")]

    /// <summary>
    /// Enable or disable the "New Folder" button in the browser dialog.
    /// </summary>
    public bool ShowNewFolderButton
    {
      get
      {
        return this._showNewFolderButton;
      }
      set
      {
        this._showNewFolderButton = value;
      }
    }

    /// <summary>
    /// Show an "edit box" in the folder browser.
    /// </summary>
    /// <remarks>
    /// The "edit box" normally shows the name of the selected folder.  
    /// The user may also type a pathname directly into the edit box.  
    /// </remarks>
    /// <seealso cref="ShowFullPathInEditBox"/>
    public bool ShowEditBox
    {
      get
      {
        return this._showEditBox;
      }
      set
      {
        this._showEditBox = value;
      }
    }

    /// <summary>
    /// Set whether to use the New Folder Browser dialog style.
    /// </summary>
    /// <remarks>
    /// The new style is resizable and includes a "New Folder" button.
    /// </remarks>
    public bool NewStyle
    {
      get
      {
        return this._newStyle;
      }
      set
      {
        this._newStyle = value;
      }
    }


    public bool DontIncludeNetworkFoldersBelowDomainLevel
    {
      get { return _dontIncludeNetworkFoldersBelowDomainLevel; }
      set { _dontIncludeNetworkFoldersBelowDomainLevel = value; }
    }

    /// <summary>
    /// Show the full path in the edit box as the user selects it. 
    /// </summary>
    /// <remarks>
    /// This works only if ShowEditBox is also set to true. 
    /// </remarks>
    public bool ShowFullPathInEditBox
    {
      get { return _showFullPathInEditBox; }
      set { _showFullPathInEditBox = value; }
    }

    public bool ShowBothFilesAndFolders
    {
      get { return _showBothFilesAndFolders; }
      set { _showBothFilesAndFolders = value; }
    }
  }



  internal static class PInvoke
  {
    static PInvoke() { }

    public delegate int BrowseFolderCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

    internal static class User32
    {
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

      [DllImport("user32.dll", SetLastError = true)]
      //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
      //public static extern IntPtr FindWindowEx(HandleRef hwndParent, HandleRef hwndChildAfter, string lpszClass, string lpszWindow);
      public static extern IntPtr FindWindowEx(HandleRef hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

      [DllImport("user32.dll", SetLastError = true)]
      public static extern Boolean SetWindowText(IntPtr hWnd, String text);
    }

    [ComImport, Guid("00000002-0000-0000-c000-000000000046"), SuppressUnmanagedCodeSecurity, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMalloc
    {
      [PreserveSig]
      IntPtr Alloc(int cb);
      [PreserveSig]
      IntPtr Realloc(IntPtr pv, int cb);
      [PreserveSig]
      void Free(IntPtr pv);
      [PreserveSig]
      int GetSize(IntPtr pv);
      [PreserveSig]
      int DidAlloc(IntPtr pv);
      [PreserveSig]
      void HeapMinimize();
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class BROWSEINFO
    {
      public IntPtr Owner;
      public IntPtr pidlRoot;
      public IntPtr pszDisplayName;
      public string Title;
      public int Flags;
      public BrowseFolderCallbackProc callback;
      public IntPtr lParam;
      public int iImage;
    }



    [SuppressUnmanagedCodeSecurity]
    internal static class Shell32
    {
      // Methods
      [DllImport("shell32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SHBrowseForFolder([In] PInvoke.BROWSEINFO lpbi);
      [DllImport("shell32.dll")]
      public static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] PInvoke.IMalloc[] ppMalloc);
      [DllImport("shell32.dll", CharSet = CharSet.Auto)]
      public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
      [DllImport("shell32.dll")]
      public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
    }

  }

  #endregion
}
