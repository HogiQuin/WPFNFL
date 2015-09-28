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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NFL.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load conferences
            cmbConferences.ItemsSource = Catalogs.GetConferences();
            
        }

        private void cmbConferences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //verify that a conference was selected
            if (cmbConferences.SelectedItem != null)
            { 
                //get selected conferences
                Conference c = (Conference)cmbConferences.SelectedItem;
                //load divicions 
                cmbDivicion.ItemsSource = c.Divisions;
                cmbTeams.ItemsSource = null;
                dgPlayers.ItemsSource = null;
            }
            
        }

        private void cmbDivicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDivicion.SelectedItem != null)
            {
                Division d = (Division)cmbDivicion.SelectedItem;
                cmbTeams.ItemsSource = d.Teams;
            }
        }

        private void cmbTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTeams.SelectedItem != null)
            {
                Team d = (Team)cmbTeams.SelectedItem;
                dgPlayers.ItemsSource = Team.GetPlayers(d);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
                NwPlayerWindow np = new NwPlayerWindow();
                np.Owner = this;
                np.ShowDialog();
            
        }

        private void btnAddTeams_Click(object sender, RoutedEventArgs e)
        {
            NewTeamWindow nt = new NewTeamWindow();
            nt.Owner = this;
            nt.ShowDialog();
        }

        
    }
}
