using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace RobloxAltGen
{
	// Token: 0x02000003 RID: 3
	public partial class MainWindow : Window
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000207E File Offset: 0x0000027E
		public MainWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000208C File Offset: 0x0000028C
		private void button_Click(object sender, RoutedEventArgs e)
		{
			this.textBlock.Text = "Cancel";
			this.button5.Content = "YES";
			((Storyboard)base.TryFindResource("uninstallpopup")).Begin();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000002C4
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if (this.Agree.IsChecked.GetValueOrDefault())
			{
				((Storyboard)base.TryFindResource("transition")).Begin();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		private void button2_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.youtube.com/channel/UCRLlL51rR_LXNIOZmgS5GjQ");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		private void awa_Click(object sender, RoutedEventArgs e)
		{
			using (WebClient webClient = new WebClient())
			{
				string[] array = webClient.DownloadString("https://raw.githubusercontent.com/robloxaltgen/official/main/update.txt").Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				Process.Start("https://discord.gg/" + array[2]);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002168 File Offset: 0x00000368
		private void installbtn_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<>c__DisplayClass6_0 CS$<>8__locals1 = new MainWindow.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			this.installbtn.Visibility = Visibility.Hidden;
			CS$<>8__locals1.MainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RobloxAltGen";
			CS$<>8__locals1.ExePath = CS$<>8__locals1.MainDirectory + "\\AltGenLauncher.exe";
			if (!Directory.Exists(CS$<>8__locals1.MainDirectory))
			{
				Directory.CreateDirectory(CS$<>8__locals1.MainDirectory);
			}
			CS$<>8__locals1.download = new WebClient();
			string[] array = CS$<>8__locals1.download.DownloadString("https://raw.githubusercontent.com/robloxaltgen/official/main/update.txt").Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			File.WriteAllText(System.IO.Path.Combine(CS$<>8__locals1.MainDirectory, "update.txt"), array[0]);
			CS$<>8__locals1.download.DownloadProgressChanged += delegate(object s, DownloadProgressChangedEventArgs ee)
			{
				if (CS$<>8__locals1.<>4__this.Intervel == 1 && ee.ProgressPercentage > 0 && ee.ProgressPercentage % 25 == 0)
				{
					((Storyboard)CS$<>8__locals1.<>4__this.TryFindResource(ee.ProgressPercentage.ToString() + "%")).Begin();
				}
			};
			CS$<>8__locals1.download.DownloadFileCompleted += delegate(object s, AsyncCompletedEventArgs ee)
			{
				MainWindow.<>c__DisplayClass6_0.<<installbtn_Click>b__1>d <<installbtn_Click>b__1>d;
				<<installbtn_Click>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<installbtn_Click>b__1>d.<>4__this = CS$<>8__locals1;
				<<installbtn_Click>b__1>d.<>1__state = -1;
				<<installbtn_Click>b__1>d.<>t__builder.Start<MainWindow.<>c__DisplayClass6_0.<<installbtn_Click>b__1>d>(ref <<installbtn_Click>b__1>d);
			};
			CS$<>8__locals1.download.Headers.Set("Accept-Encoding", "gzip, deflate, br");
			CS$<>8__locals1.download.Headers.Set("Accept", "*/*");
			CS$<>8__locals1.download.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/robloxaltgen/official/main/Updater.exe"), CS$<>8__locals1.MainDirectory + "\\Updater.exe");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A0 File Offset: 0x000004A0
		private void button5_Click(object sender, RoutedEventArgs e)
		{
			if (this.textBlock.Text == "Cancel")
			{
				Storyboard storyboard = (Storyboard)base.TryFindResource("close");
				storyboard.Completed += delegate(object s, EventArgs eee)
				{
					base.Close();
				};
				storyboard.Begin();
				return;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", true))
			{
				string text = "RobloxAltGen";
				RegistryKey registryKey2 = registryKey.OpenSubKey(text);
				if (registryKey2 != null)
				{
					registryKey2.Close();
					registryKey.DeleteSubKey(text);
				}
			}
			using (RegistryKey classesRoot = Registry.ClassesRoot)
			{
				RegistryKey registryKey3 = classesRoot.OpenSubKey("altlauncher", true);
				if (registryKey3 != null)
				{
					registryKey3.Close();
					classesRoot.DeleteSubKeyTree("altlauncher");
				}
			}
			Storyboard storyboard2 = (Storyboard)base.TryFindResource("uninstallcancel");
			storyboard2.Completed += delegate(object s, EventArgs ee)
			{
				Storyboard storyboard3 = (Storyboard)base.TryFindResource("close");
				storyboard3.Completed += delegate(object ss, EventArgs eee)
				{
					string text2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RobloxAltGen";
					if (Directory.Exists(text2))
					{
						new Process
						{
							StartInfo = new ProcessStartInfo
							{
								WindowStyle = ProcessWindowStyle.Hidden,
								FileName = "cmd.exe",
								UseShellExecute = true,
								Arguments = string.Format("/C timeout /t 3 & echo Y | rmdir /s \"{0}\"", text2)
							}
						}.Start();
					}
					base.Close();
				};
				storyboard3.Begin();
			};
			storyboard2.Begin();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023A0 File Offset: 0x000005A0
		private void button4_Click(object sender, RoutedEventArgs e)
		{
			if (this.textBlock.Text == "Cancel")
			{
				((Storyboard)base.TryFindResource("uninstallcancel")).Begin();
				return;
			}
			Storyboard storyboard = (Storyboard)base.TryFindResource("uninstallcancel");
			storyboard.Completed += delegate(object s, EventArgs ee)
			{
				Storyboard storyboard2 = (Storyboard)base.TryFindResource("close");
				storyboard2.Completed += delegate(object ss, EventArgs eee)
				{
					base.Close();
				};
				storyboard2.Begin();
			};
			storyboard.Begin();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002404 File Offset: 0x00000604
		private void Storyboard_Completed(object sender, EventArgs e)
		{
			try
			{
				List<string> list = Environment.GetCommandLineArgs().ToList<string>();
				if (list.Count > 1 && list[1].Contains("uninstall"))
				{
					this.sections.Visibility = Visibility.Hidden;
					this.button.Visibility = Visibility.Hidden;
					((Storyboard)base.TryFindResource("uninstallpopup")).Begin();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000247C File Offset: 0x0000067C
		private async void window_Loaded(object sender, RoutedEventArgs e)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage httpResponseMessage = await client.GetAsync("https://robloxaltgen.com/tos.txt");
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					TextBox textBox = this.tosbox;
					textBox.Text = await httpResponseMessage.Content.ReadAsStringAsync();
					textBox = null;
				}
			}
			HttpClient client = null;
		}

		// Token: 0x04000001 RID: 1
		private int Intervel;
	}
}
