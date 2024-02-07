using CL_bibliotheque;
using CL_Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace GUI_Bibliotheque
{
    /// <summary>
    /// Logique d'interaction pour EmpruntWindow.xaml
    /// </summary>
    public partial class EmpruntWindow : Window
    {
        public static event EventHandler? BibliothequeMiseAJour;

        private Bibliotheque bibliotheque;
        public EmpruntWindow(Bibliotheque bibliotheque)
        {
            InitializeComponent();

            // Récupérer l'instance de Bibliotheque passée depuis MainWindow
            this.bibliotheque = bibliotheque;

            // Remplir les ComboBox avec les noms des livres
            livresEmpruntComboBox.DisplayMemberPath = "Titre";
            livresRetourComboBox.DisplayMemberPath = "Titre";

            if (bibliotheque.Livres != null)
            {
                foreach (Livre livre in bibliotheque.Livres)
                {
                    livresEmpruntComboBox.Items.Add(livre);
                    livresRetourComboBox.Items.Add(livre);
                }
            }
            
        }

        private void validerEmpruntButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tout le code de validation et d'emprunt

                // Vérifier si l'ID de l'emprunt est saisi
                if (string.IsNullOrEmpty(idEmpruntTextBox.Text)){throw new BibliothequeException("Veuillez sélectionner un livre.");}

                // Vérifier si un livre est sélectionné dans la ComboBox
                if (livresEmpruntComboBox.SelectedItem == null) { throw new BibliothequeException("Veuillez entrer un ID valide."); }

                // Récupérer l'ID de l'emprunt saisi
                int idEmprunt;
                if (!int.TryParse(idEmpruntTextBox.Text, out idEmprunt)){throw new BibliothequeException("Veuillez entrer un ID valide.");}

                // Récupérer l'abonné correspondant à l'ID saisi
                Abonne abonne = bibliotheque.getAbonneParId(idEmprunt);

                // Récupérer le livre sélectionné dans la ComboBox
                Livre livre = (Livre)livresEmpruntComboBox.SelectedItem;

                // Effectuer l'emprunt
                bibliotheque.EmprunterLivre(abonne, livre);

                BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);

                MessageBox.Show("L'emprunt a été enregistré avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            livresEmpruntComboBox.SelectedItem = null;
        }

        private void validerRetourButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Vérifier si l'ID de l'emprunt est saisi
                if (string.IsNullOrEmpty(idRetourTextBox.Text)) { throw new BibliothequeException("Veuillez entrer l'ID de l'abonné."); }

                // Vérifier si un livre est sélectionné dans la ComboBox
                if (livresRetourComboBox.SelectedItem == null) { throw new BibliothequeException("Veuillez sélectionner un livre."); }

                // Récupérer l'ID de l'emprunt saisi
                int idretourEmprunt;
                if (!int.TryParse(idRetourTextBox.Text, out idretourEmprunt)) { throw new BibliothequeException("Veuillez entrer un ID valide."); }

                // Récupérer l'abonné correspondant à l'ID saisi
                Abonne abonne = bibliotheque.getAbonneParId(idretourEmprunt);

                // Récupérer le livre sélectionné dans la ComboBox
                Livre livre = (Livre)livresRetourComboBox.SelectedItem;

                // Effectuer le retour
                bibliotheque.RetournerLivre(abonne, livre);

                BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);

                MessageBox.Show("Le retour a été enregistré avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BibliothequeException ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            livresRetourComboBox.SelectedItem = null;
        }

        private void livresButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }

        private void EmpruntWindows_Closing(object sender, CancelEventArgs e)
        {
            // Appel à l'événement BibliothequeMiseAJour
            BibliothequeMiseAJour?.Invoke(bibliotheque, EventArgs.Empty);
        }
    }
}
