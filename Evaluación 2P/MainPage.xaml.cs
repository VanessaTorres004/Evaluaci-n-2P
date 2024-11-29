
namespace Evaluación_2P
{ 
        public partial class MainPage : ContentPage
        {
            public MainPage()
            {
                InitializeComponent();
                
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
           
                string fileName = $"{NameEntry.Text}.txt"; 
                if (File.Exists(fileName))
                {
                    var lastRecarga = File.ReadAllText(fileName);
                   
                    var data = lastRecarga.Split(',');
                
                BindingContext = new
                {
                    PrimerNombre = data[0],
                    SegundoNombre = data[0],
                    PhoneNumber = data[0], 
                    MontoRecarga = data[1] 
                    };

               
                RecargaMessage.Text = $"Última recarga: {data[1]} dólares a {data[0]}";
            }
            else
            {
                RecargaMessage.Text = "No hay recargas anteriores registradas.";
            }
        }
    }
}