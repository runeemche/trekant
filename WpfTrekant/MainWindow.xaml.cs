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

namespace WpfTrekant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum TriType { 
        NotTri,
        Equi,
        Iso,
        Sca
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.TryParse(txtboxSide1.Text, out var Side1) &&
            Decimal.TryParse(txtboxSide2.Text, out var Side2) &&
            Decimal.TryParse(txtboxSide3.Text, out var Side3))
            {
                switch (TestTriangle(Side1, Side2, Side3))
                {
                    case TriType.NotTri: txtblockRes.Text = "Ikke trekant - kan ikke nå"; break;
                    case TriType.Equi: txtblockRes.Text = "Ligesidet"; break;
                    case TriType.Iso: txtblockRes.Text = "Ligebenet"; break;
                    case TriType.Sca: txtblockRes.Text = "Skalent - alle sider forskellige"; break;
                }

            }
            else
            {
                txtblockRes.Text = "Kunne ikke parse tal";
            }
        }

        private TriType TestTriangle(decimal Side1, decimal Side2, decimal Side3)
        {
            if (Side1 + Side2 <= Side3 || Side2 + Side3 <= Side1 || Side1 + Side3 <= Side2)
                return TriType.NotTri;
            else
            if (Side1 == Side2 && Side2 == Side3)
                 return TriType.Equi;
            else
            if (Side1 == Side2 || Side2 == Side3 || Side1 == Side3)
                return TriType.Iso;

            return TriType.Sca;
        }


        private void RunTestCases()
        {
            try
            {
                Assert(TriType.NotTri, 10, 1, 1);
                Assert(TriType.Iso, 4, 4, 5);
                Assert(TriType.Equi, 4, 4, 4);
                Assert(TriType.Sca, 3, 4, 5);
                txtblockRes.Text = "Test pass";
            }
            catch (Exception e)
            {
                txtblockRes.Text = e.Message;
            }
           
            try
            {
                Assert(TriType.NotTri, 4, 5, 6);
            }
            catch (Exception e)
            {
                txtblockRes.Text = "Test pass2";
            }


        }

        private void Assert(TriType triTyp, decimal v1, decimal v2, decimal v3)
        {
            if (TestTriangle(v1, v2, v3) != triTyp)
                throw new Exception($"Assert failed : {triTyp}{v1}{v2}{v3}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RunTestCases();
        }
    }
}
