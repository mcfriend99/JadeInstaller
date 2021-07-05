using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace JadeInstaller
{
  public class InstallCreator
  {
    /// <summary>
    /// The name of the installer project
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The name of the setup executable to generate
    /// </summary>
    public string SetupName { get; set; }

    /// <summary>
    /// The version string of the installer
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// The descirption given to the installer.
    /// This will be shown whenever the executable is hovered.
    /// </summary>
    public string SetupDescription { get; set; } = "Created by JadeInstaller";

    /// <summary>
    /// The company that owns the application to be installed
    /// </summary>
    public string Company { get; set; } = "JadeInstaller";

    /// <summary>
    /// The copyright string for the installer executable
    /// </summary>
    public string Copyright { get; set; } = $"Copyright ©  {DateTime.Today.Year}";

    /// <summary>
    /// Any trademark owned by the installer
    /// </summary>
    public string Trademark { get; set; } = "";

    /// <summary>
    /// A list of files to add to the installer.
    /// This files will be extracted to the install location at installation time.
    /// </summary>
    public List<string> Files { get; set; } = new List<string>();

    /// <summary>
    /// A list of directories to add to the installer.
    /// This directories will be extracted to the install location at installation time.
    /// </summary>
    public List<string> Folders { get; set; } = new List<string>();

    /// <summary>
    /// Whether to add the installed application to start menu or not.
    /// </summary>
    public bool EnableAddStartMenuEntry { get; set; } = true;

    /// <summary>
    /// Whether to create a desktop icon for the installation or not.
    /// </summary>
    public bool EnableAddDesktopIcon { get; set; } = true;

    /// <summary>
    /// Whether to create a quick launch icon for the installation or not.
    /// </summary>
    public bool EnableAddQuickLuanchIcon { get; set; } = true;

    /// <summary>
    /// A list of commands to run before installation.
    /// </summary>
    public List<string> PreInstallCommands { get; set; } = new List<string>();

    /// <summary>
    /// A list of commands to run after installation.
    /// </summary>
    public List<string> PostInstallCommands { get; set; } = new List<string>();

    public string License { get; set; } = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Arial;}}{\*\generator Msftedit 5.41.15.1515;}\viewkind4\uc1\pard\f0\fs20 asdfasdf\par}";

    /// <summary>
    /// The path the installer should extract to.
    /// </summary>
    public string InstallLocation { get; set; }

    /// <summary>
    /// The name of the executable that will be the main entry point
    /// of the installed app.
    /// This is the executable that will be pointed to by desktop shortcuts
    /// and start menu.
    /// </summary>
    public string ExecutableName { get; set; }

    /// <summary>
    /// Errors that occured during compilation.
    /// </summary>
    public string CompileError { get; set; }

    private long MinStorage = 0;

    public string CreateInstallData()
    {
      string data = "";

      string tmpName = "0________________0";
      @File.Delete(tmpName);

      using (ZipArchive archive = ZipFile.Open(tmpName, ZipArchiveMode.Create))
      {
        foreach(string file in Files)
        {
          ZipArchiveEntry entry = archive.CreateEntryFromFile(new FileInfo(file).FullName, file);

          // add to minimum storage required for installation.
          MinStorage += new FileInfo(file).Length;
        }

        foreach(string dirName in Folders)
        {
          DirectoryInfo dir = new DirectoryInfo(dirName);
          foreach(FileInfo file in dir.EnumerateFiles())
          {
            ZipArchiveEntry entry = archive.CreateEntryFromFile(file.FullName, $"{dirName}/{file.FullName.Replace(dir.FullName, "").Trim('/', '\\')}");

            // add to minimum storage required for installation.
            MinStorage += file.Length;
          }
        }
      }

      if(File.Exists(tmpName))
      {
        data = Convert.ToBase64String(File.ReadAllBytes(tmpName));
        File.Delete(tmpName);
      }

      return data;
    }

    public string ToFileSize(long byteCount)
    {
      string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
      if(byteCount > 0)
      {
        long bytes = Math.Abs(byteCount);
        int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        double num = Math.Round(bytes / Math.Pow(1024, place), 1);
        return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
      }
      return "0 B";
    }

    public string Generate()
    {
      CompileError = null;

      CSharpCodeProvider provider = new CSharpCodeProvider();

      // build the parameters for source compilation
      CompilerParameters cp = new CompilerParameters();

      // add an assembly
      cp.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
      cp.ReferencedAssemblies.Add("System.dll");
      cp.ReferencedAssemblies.Add("System.Core.dll");
      cp.ReferencedAssemblies.Add("System.Data.dll");
      cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
      cp.ReferencedAssemblies.Add("System.Deployment.dll");
      cp.ReferencedAssemblies.Add("System.Drawing.dll");
      cp.ReferencedAssemblies.Add("System.IO.Compression.dll");
      cp.ReferencedAssemblies.Add("System.IO.Compression.FileSystem.dll");
      cp.ReferencedAssemblies.Add("System.Net.Http.dll");
      cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");

      // generate an executable instead of a class library
      cp.GenerateExecutable = true;
      cp.CompilerOptions = "/target:winexe";

      // set assembly file name to generate
      cp.OutputAssembly = SetupName;

      // save the assembly oy a physical file
      cp.GenerateInMemory = false;

      string source = File.ReadAllText("Installer.cs");
      source = source.Replace("{{NAME}}", Name);
      source = source.Replace("{{VERSION}}", Version);
      source = source.Replace("{{DESCRIPTION}}", SetupDescription);
      source = source.Replace("{{COMPANY}}", Company);
      source = source.Replace("{{COPYRIGHT}}", Copyright);
      source = source.Replace("{{TRADEMARK}}", Trademark);
      source = source.Replace("{{SETUP_NAME}}", SetupName);
      source = source.Replace("{{INSTALL_LOCATION}}", InstallLocation);
      source = source.Replace("{{EXECUTABLE_NAME}}", ExecutableName);
      source = source.Replace("{{LICENSE}}", License);

      string preInstall = "", postInstall = "";

      foreach(string str in PreInstallCommands)
      {
        preInstall += $"@\"{str}\",\n";
      }
      foreach (string str in PostInstallCommands)
      {
        postInstall += $"@\"{str}\",\n";
      }

      source = source.Replace("\"{{PRE_INSTALL_COMMANDS}}\"", preInstall);
      source = source.Replace("\"{{POST_INSTALL_COMMANDS}}\"", postInstall);

      source = source.Replace("{{ADD_STARTMENU_ENTRY}}", EnableAddStartMenuEntry ? "true" : "false");
      source = source.Replace("{{ADD_DESKTOP_ICON}}", EnableAddDesktopIcon ? "true" : "false");
      source = source.Replace("{{ADD_QUICK_LAUNCH_ICON}}", EnableAddQuickLuanchIcon ? "true" : "false");

      source = source.Replace("{{INSTALL_DATA}}", CreateInstallData());

      source = source.Replace("{{STORAGE_REQUIRED}}", ToFileSize(MinStorage));

      Console.WriteLine(source);

      //Console.WriteLine(source);

      // invoke compilation
      CompilerResults cr = provider.CompileAssemblyFromSource(cp, source);

      if (cr.Errors.Count > 0)
      {
        CompileError += $"Error building Test into {SetupName}.exe:\n";
        foreach (CompilerError ce in cr.Errors)
        {
          CompileError += $"    {ce.ToString()}\n";
        }
      }
      else
      {
        return cr.PathToAssembly;
      }
      return null;
    }
  }
}
