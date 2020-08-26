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
using System.Data.SqlClient;

namespace WpfStarterLearning
{
    /// <summary>MainWindow.xamlの相互作用ロジック</summary>
    /// <remarks>NuGetパッケージ: Install-Package System.Data.SqlClient -Version 4.8.1</remarks>
    public partial class MainWindow : Window
    {
        private System.Windows.Data.Binding bind;
        private System.Data.DataSet Dset;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = "WPFアプリ、スターター学習";
            RichTextBox1.AppendText("WPFアプリ、スターター学習..." + "\r\n");
        }

        private void ButtonDbRead_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            var datat = new System.Data.DataTable();


            sb.DataSource = "(local)\\SQLEXPRESS";
            sb.InitialCatalog = "AdoNetSample";
            sb.IntegratedSecurity = true;
            sb.ConnectTimeout = 3;

            cn.ConnectionString = sb.ConnectionString;
            cn.Open();

            cmd.Connection = cn;
            cmd.CommandText = "select * from ShohinDataDesk";
            dr = cmd.ExecuteReader();

            datat.Load(dr, System.Data.LoadOption.PreserveChanges);
            dr.Close();
            DataGrid1.ItemsSource = datat.DefaultView;

            cn.Close();
            RichTextBox1.AppendText("テーブルの内容を読み込みました。" + "\r\n");
        }

        private void ButtonTimer_Click(object sender, RoutedEventArgs e)
        {
            WindowTimer subw = new WindowTimer();
            subw.ShowDialog();
        }

        private void ButtonXmlRead_Click(object sender, RoutedEventArgs e)
        {
            bind = new System.Windows.Data.Binding();
            //DataGrid1.ItemsSource = bind1;
            Dset = new System.Data.DataSet();

            Dset.ReadXml("MsSqlServer.xml", System.Data.XmlReadMode.ReadSchema);
            DataGrid1.ItemsSource = Dset.Tables;// (0).DefaultView;
            //bind.Source = dataset1.Tables(0).DefaultView;
        }

        private void ButtonXmlWrite_Click(object sender, RoutedEventArgs e)
        {
            Dset.WriteXml("MsSqlServer.xml", System.Data.XmlWriteMode.WriteSchema);
            Dset.AcceptChanges();
        }
    }
}