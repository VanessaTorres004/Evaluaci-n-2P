using System;
using System.IO;
using Microsoft.Maui.Controls;
namespace Evaluación_2P
{ 
        public partial class MainPage : ContentPage
        {
            public MainPage()
            {
                InitializeComponent();
                LoadLastRecarga();
            }

            private void OnMontoSelected(object sender, EventArgs e)
            {
                if (MontoPicker.SelectedIndex != -1)
                {
                    var monto = MontoPicker.SelectedItem.ToString();
                    RecargaMessage.Text = $"Se ha seleccionado una recarga de {monto} dólares.";
                }
            }

            private async void OnRecargaButtonClicked(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(PhoneEntry.Text) || string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un número telefónico y nombre", "OK");
                    return;
                }

                if (MontoPicker.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Debe seleccionar un monto", "OK");
                    return;
                }

                var confirm = await DisplayAlert(
                    "Confirmación",
                    $"¿Confirma realizar una recarga de {MontoPicker.SelectedItem} dólares a {PhoneEntry.Text}?",
                    "Confirmar",
                    "Cancelar");

                if (confirm)
                {
                    var fileName = $"{NameEntry.Text}.txt";
                    File.WriteAllText(fileName, $"{PhoneEntry.Text},{MontoPicker.SelectedItem}");
                    await DisplayAlert("Éxito", "Recarga realizada con éxito", "OK");
                    LoadLastRecarga();
                }
                else
                {
                    await DisplayAlert("Cancelación", "La recarga ha sido cancelada.", "OK");
                }
            }

            private void LoadLastRecarga()
            {
                // Implementar la lógica para cargar la última recarga desde el archivo y hacer binding
                // Esto es solo un ejemplo, debes ajustar según tu lógica de datos
                string fileName = $"{NameEntry.Text}.txt"; // Asegúrate de que el nombre se haya ingresado
                if (File.Exists(fileName))
                {
                    var lastRecarga = File.ReadAllText(fileName);
                    // Aquí puedes dividir la información y establecer el binding correspondiente
                    var data = lastRecarga.Split(',');
                    // Asumiendo que el primer nombre es el primer elemento, segundo nombre el segundo, etc.
                    BindingContext = new
                    {
                        PrimerNombre = data[0],
                        SegundoNombre = "SegundoNombreEjemplo