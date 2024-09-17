using System.Collections.Generic;
using System.Security.Claims;
using System.Windows;

namespace ContractClaimSystem
{
    public partial class MainWindow : Window
    {
        public static List<Claim> ClaimsList = new List<Claim>();

        public MainWindow()
        {
            InitializeComponent();
            ClaimsDataGrid.ItemsSource = ClaimsList; // Bind the claims list to the data grid
        }

        private void SubmitNewClaimButton_Click(object sender, RoutedEventArgs e)
        {
            var submitClaimWindow = new SubmitClaimWindow();
            submitClaimWindow.ShowDialog(); // Open the Submit Claim window
            ClaimsDataGrid.ItemsSource = null;
            ClaimsDataGrid.ItemsSource = ClaimsList; // Refresh the data grid after submitting a claim
        }

        private void ClaimsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ClaimsDataGrid.SelectedItem is Claim selectedClaim)
            {
                var verifyClaimWindow = new VerifyClaimWindow(selectedClaim);
                verifyClaimWindow.ShowDialog();
                ClaimsDataGrid.ItemsSource = null;
                ClaimsDataGrid.ItemsSource = ClaimsList; // Refresh the data grid after verification
            }
        }
    }
}
