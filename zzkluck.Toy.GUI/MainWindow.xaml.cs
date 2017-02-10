using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zzkluck.Toy;
using System.Xml;
using System.Collections.ObjectModel;

namespace zzkluck.Toy.GUI
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		static string XmlInfomationFilePath = "../../PersonInfoFile.xml";
		SomeInformation info = new SomeInformation("A1  最后编译于"+DateTime.Now.ToString());

		BirthdayManager bm = new BirthdayManager();
		public MainWindow()
		{
			InitializeComponent();

			this.Loaded += MainWindow_Loaded;
			GridBirthday.DataContext = bm.Persons;
			GridMain.DataContext = info;

			LstBxPerson.Background = new SolidColorBrush(Color.FromArgb(0xC0, 0xF0, 0xF8, 0xFF)); 
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			bm.ReadAllFromFile(XmlInfomationFilePath);
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				PersonInfoPlus newPerson = getInformation();
				bm.Persons.Add(newPerson);
				Search(TxtBxSearch.Text);
			}
			catch
			{
				MessageBox.Show("Something error");
			}

		}

		private void BtnRemove_Click(object sender, RoutedEventArgs e)
		{
			PersonInfoPlus RemovedPerson = LstBxPerson.SelectedItem as PersonInfoPlus;
			if (RemovedPerson != null)
			{
				bm.Persons.Remove(RemovedPerson);
				Search(TxtBxSearch.Text);
			}
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			bm.WriteAllToFile(XmlInfomationFilePath);
			MessageBox.Show("Save Success");
		}

		private void LstBxPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			object SelectItem = ((ListBox)sender).SelectedItem;
			if (SelectItem == null)
			{
				BtnClear_Click(sender,new RoutedEventArgs());
				return;
			}
			SetInformation((PersonInfoPlus)SelectItem); 
			//SetInformation(bm.Persons[SelectIndex]);
		}

		private void BtnClear_Click(object sender, RoutedEventArgs e)
		{
			TxtBxName.Text = string.Empty;
			CoBxSexual.SelectedIndex = -1;
			TxtBxBirthday.Text = string.Empty;
			CoBxImprotance.SelectedIndex = -1;
			TxtBxIconPath.Text = string.Empty;
		}

		private void BtnSelectIcon_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

			openFileDialog.Filter = "图片类型(*.png,*.jpg,*.bmp)|*.png;*.jpg;*.bmp|All Files(*.*)|*.*";

			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				TxtBxIconPath.Text = openFileDialog.FileName;
			}
		}
		private void SetInformation(PersonInfoPlus SelectPerson)
		{
			TxtBxName.Text = SelectPerson.Name;
			CoBxSexual.SelectedIndex = (int)SelectPerson.Sexual;
			TxtBxBirthday.Text = SelectPerson.BirthdayString;
			CoBxImprotance.SelectedIndex = (int)SelectPerson.Improtance;
			TxtBxIconPath.Text = SelectPerson.Icon;
		}

		private PersonInfoPlus getInformation()
		{
			return new PersonInfoPlus(
						bm.NextID++,
						TxtBxName.Text,
						(SexualEnum)CoBxSexual.SelectedIndex,
						DateTime.Parse(TxtBxBirthday.Text),
						(uint)CoBxImprotance.SelectedIndex,
						TxtBxIconPath.Text);
		}

		private void BtnChange_Click(object sender, RoutedEventArgs e)
		{
			int selectedIndex = LstBxPerson.SelectedIndex;
			bm.Persons[selectedIndex] = getInformation();
			LstBxPerson.SelectedIndex = selectedIndex;
		}

		private void TxtBxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			string key = ((TextBox)sender).Text;
			Search(key);
		}

		private void Search(string key)
		{
			if (key == string.Empty)
				GridBirthday.DataContext = bm.Persons;

			ObservableCollection<PersonInfoPlus> result = new ObservableCollection<PersonInfoPlus>();
			foreach (PersonInfoPlus p in bm.Persons)
			{
				if (p.Name.IndexOf(key) != -1)
				{
					result.Add(p);
				}
			}
			bm.SearchResult = result;
			GridBirthday.DataContext = bm.SearchResult;
		}

		private void BtnNavigateBirthday_Click(object sender, RoutedEventArgs e)
		{
			GridMain.Visibility = Visibility.Collapsed;
			GridBirthday.Visibility = Visibility.Visible;
		}
	}
}
