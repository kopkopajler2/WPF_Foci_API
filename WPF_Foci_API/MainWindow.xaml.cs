using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Foci_API.Models;

namespace WPF_Foci_API
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ObservableCollection<Eredmeny> eredmenyek;
		HttpClient client = new HttpClient();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btHozzaad_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Eredmeny eredmeny = new Eredmeny();
				eredmeny.hazai = tbHazai.Text;
				eredmeny.vendeg = tbVendeg.Text;
				eredmeny.hazaipont = int.Parse(tbHazaipont.Text);
				eredmeny.vendegpont = int.Parse(tbVendegpont.Text);
				eredmeny.datum = tbDatum.Text;
				eredmeny.helyszin = tbHelyszin.Text;

				string json = Newtonsoft.Json.JsonConvert.SerializeObject(eredmeny);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
				client.PostAsync("https://65e9a37bc9bf92ae3d39b5e0.mockapi.io/api/eredmeny", content);
				MessageBox.Show("Sikeres hozzáadás!");
				tbHazai.Text = "";
				tbVendeg.Text = "";
				tbHazaipont.Text = "";
				tbVendegpont.Text = "";
				tbDatum.Text = "";
				tbHelyszin.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message+" itt a hiba");
			}

        }

		private void btLeker_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string json = client.GetStringAsync("https://65e9a37bc9bf92ae3d39b5e0.mockapi.io/api/eredmeny").Result;
				eredmenyek = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Eredmeny>>(json);
				listViewMatches.ItemsSource = eredmenyek;
				MessageBox.Show("Sikeres lekérdezés!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message+" hiba!!");
			}

		}
	}
}