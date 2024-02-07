using CL_bibliotheque;
using CL_Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;


namespace GUI_Bibliotheque
{
    /// <summary>
    /// Logique d'interaction pour RequeteWindow.xaml
    /// </summary>
    public partial class RequeteWindow : Window
    {
        public static event EventHandler? BibliothequeMiseAJour;

        private Bibliotheque bibliotheque;
        public RequeteWindow(Bibliotheque bibli)
        {
            InitializeComponent();

            this.bibliotheque = bibli;

            // Remplir les ComboBox avec les noms des livres
            numeroAbonneComboBox.DisplayMemberPath = "NumeroAbonne";
            livreComboBox.DisplayMemberPath = "Titre";

            if (bibliotheque.Livres != null)
            {
                foreach (Livre livre in bibliotheque.Livres)
                {
                    livreComboBox.Items.Add(livre);
                }
            }

            if (bibliotheque.Abonnes != null)
            {
                foreach (Abonne abo in bibliotheque.Abonnes)
                {
                    numeroAbonneComboBox.Items.Add(abo);
                }
            }

            requetesDataGrid.ItemsSource = null;
            requetesDataGrid.Items.Clear();
        }

        private void RechercherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                requetesDataGrid.ItemsSource = null;
                requetesDataGrid.Items.Clear();

                if (numeroAbonneComboBox.SelectedItem == null && livreComboBox.SelectedItem == null) { throw new BibliothequeException("Veuillez selectionner un numero d'abonne ou un livre."); }

                if (numeroAbonneComboBox.SelectedItem != null)
                {
                    // Recherche des livres empruntés par un abonné spécifique
                    Abonne abonne = (Abonne)numeroAbonneComboBox.SelectedItem;

                    List<Emprunt> emprunts = bibliotheque.RequeteLivresEmpruntesParAbonne(abonne.NumeroAbonne);
                    requetesDataGrid.Items.Add(emprunts);
                }
                else if (livreComboBox.SelectedItem != null)
                {
                    // Recherche des abonnés ayant emprunté un livre spécifique
                    Livre livre = (Livre)livreComboBox.SelectedItem;

                    List<Emprunt> emprunts = bibliotheque.RequeteAbonnesEmpruntParLivre(livre.Titre);
                    requetesDataGrid.ItemsSource = emprunts;
                }
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            numeroAbonneComboBox.SelectedItem = null;
            livreComboBox.SelectedItem = null;
        }
        private void livresButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }

        private void RequeteWindows_Closing(object sender, CancelEventArgs e)
        {
            // Appel à l'événement BibliothequeMiseAJour
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }
    }
}
