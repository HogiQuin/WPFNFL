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
    /// Interaction logic for NewTeamWindow.xaml
    /// </summary>
    public partial class NewTeamWindow : Window
    {
        public NewTeamWindow()
        {
            InitializeComponent();
            cmbConference.ItemsSource = Catalogs.GetConferences();
        }
        
        //object

        private Team t = new Team();
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //load image
            OpenFileDialog dlgLogo = new OpenFileDialog(); //create object
            dlgLogo.Title = "Load Logo"; //title
            dlgLogo.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg"; //allow only images to be selected
            if (dlgLogo.ShowDialog() == true) //show dialog/if user pressed OK
            {
                imgLogo.Source = new BitmapImage(new Uri(dlgLogo.FileName)); //load image into control
                t.LogoUri = dlgLogo.FileName;// pass image to object
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //read values
            t.Id = txtId.Text;
            t.City = txtCity.Text;
            t.Nickname = txtNickname.Text;
            //save team
            Division d =(Division)cmbDivision.SelectedItem;
            if (t.Add(d.Id))
            { MessageBox.Show("Added!"); }
        }

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

       
    }
}
