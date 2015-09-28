using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//added
using Microsoft.Win32;


namespace NFL.App
{
    /// <summary>
    /// Interaction logic for NwPlayerWindow.xaml
    /// </summary>
    public partial class NwPlayerWindow : Window
    {
        public NwPlayerWindow()
        {
            InitializeComponent();
            cmbConference.ItemsSource  = Catalogs.GetConferences();
        }
        private player p = new player();
        private void cmbConference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //verify that a conference was selected
            if (cmbConference.SelectedItem != null)
            {
                //get selected conferences
                Conference c = (Conference)cmbConference.SelectedItem;
                //load divicions 
                cmbDivision.ItemsSource = c.Divisions;

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //read values
           
            p.FirstName = txtFirstName.Text;
            p.LastName = txtLastName.Text;
            p.DateOfBirth =DateTime.Parse(txtDateOfBirt.Text);
            p.HeightInInches = Int32.Parse(txtEight.Text);
            p.WeightInPounds = Int32.Parse(txtWeight.Text);
            //save player
            Team t = (Team)cmbTeams.SelectedItem;
            if (t.Add(t.Id))
            {
                MessageBox.Show("Added!"); 
            }
            p.Team = t.Id;
            p.Add();
            MessageBox.Show("Added!");
            //clean txbox
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtDateOfBirt.Text ="";
            txtEight.Text="";
            txtWeight.Text = "";
            
        }

        private void cmbDivision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //verify that a conference was selected
            if (cmbDivision.SelectedItem != null)
            {
                Division d = (Division)cmbDivision.SelectedItem;
                cmbTeams.ItemsSource = d.Teams;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //load image
            OpenFileDialog dlgLogo = new OpenFileDialog(); //create object
            dlgLogo.Title = "Load Logo"; //title
            dlgLogo.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg"; //allow only images to be selected
            if (dlgLogo.ShowDialog() == true) //show dialog/if user pressed OK
            {
                imgLogo.Source = new BitmapImage(new Uri(dlgLogo.FileName)); //load image into control
                p.LogoUri = dlgLogo.FileName;// pass image to object
            }
        }

       
    }
}
