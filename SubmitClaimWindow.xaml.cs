using System;
using System.Security.Claims;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ContractClaimSystem
{
    public partial class SubmitClaimWindow : Window
    {
        private string uploadedDocumentPath; // To store the path of the uploaded document

        public SubmitClaimWindow()
        {
            InitializeComponent();
        }

        private void UploadDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|Image Files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedDocumentPath = openFileDialog.FileName;
                UploadedDocumentTextBlock.Text = $"Uploaded: {System.IO.Path.GetFileName(uploadedDocumentPath)}";
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string contract = (ContractComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string claimAmount = ClaimAmountTextBox.Text;
            string claimDate = ClaimDatePicker.SelectedDate?.ToString("d");

            if (string.IsNullOrEmpty(contract) || string.IsNullOrEmpty(claimAmount) || string.IsNullOrEmpty(claimDate) || string.IsNullOrEmpty(uploadedDocumentPath))
            {
                MessageBox.Show("Please fill in all fields and upload a supporting document before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var newClaim = new Claim
                {
                    ClaimID = MainWindow.ClaimsList.Count + 1,
                    Contract = contract,
                    Amount = double.Parse(claimAmount),
                    ClaimDate = DateTime.Parse(claimDate),
                    Status = "Pending",
                    DocumentName = uploadedDocumentPath
                };

                MainWindow.ClaimsList.Add(newClaim); // Add the new claim to the list
                MessageBox.Show("Claim submitted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
